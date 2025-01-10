namespace BanSach.Components.Model.ViewModel
{
    public class ShippingInfoDTO
    {
        public int UserID { get; set; }
        public string? ReceiverName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SelectedCity { get; set; }
        public string? SelectedDistrict { get; set; }
        public string? ReceiveAddress { get; set; }
    }
}
