using BanSach.Components.Model;
using BanSach.Components.Model.ViewModel;
using Net.payOS.Types;

namespace BanSach.Components.Services.PaymentServices
{
    public interface IPaymentOS
    {
        Task<CreatePaymentResult> CreatePaymentLink(List<ProductBillDetailDto> model);
        string GenerateRandomString(int lenght);
        Task<Bill> PaymentSucsses(string orderCode);
    }
}
