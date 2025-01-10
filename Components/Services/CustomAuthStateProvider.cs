using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorageService;
    private readonly HttpClient http;
    private bool isInitialized = false;
    private bool isPrerendering = true; // Flag to handle prerendering state

    public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
    {
        this.localStorageService = localStorageService;
        this.http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Nếu chưa khởi tạo hoặc đang prerendering, trả về một AuthenticationState mặc định
        if (!isInitialized || isPrerendering)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Lấy token từ LocalStorage
        string authToken = await localStorageService.GetItemAsStringAsync("authToken");
        var identity = new ClaimsIdentity();
        http.DefaultRequestHeaders.Authorization = null;

        // Nếu token tồn tại, thêm thông tin xác thực vào headers HTTP
        if (!string.IsNullOrEmpty(authToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
            }
            catch (Exception)
            {
                // Xử lý lỗi giải mã token nếu có
                identity = new ClaimsIdentity();
            }
        }

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    // Đánh dấu kết thúc quá trình prerendering
    public void NotifyPrerenderCompleted()
    {
        isPrerendering = false;
    }

    // Khởi tạo lại trạng thái xác thực khi đã sẵn sàng
    public void InitializeAuthenticationStateAsync()
    {
        isInitialized = true;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    // Hàm giải mã JWT
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        // Thêm padding vào Base64 nếu cần
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    // Phân tích claims từ JWT token
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

    }
}
