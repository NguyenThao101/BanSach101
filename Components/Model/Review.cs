using BanSach.Components.Model;

public class Review
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int ProductBillId { get; set; }
    public Product_bill ProductBill { get; set; }
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}

