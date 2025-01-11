using BanSach.Components.Data;
using BanSach.Components.IService;
using BanSach.Components.Model;
using BanSach.Components.Model.ViewModel;
using BanSach.Components.Services.PaymentServices;
using Microsoft.EntityFrameworkCore;

namespace BanSach.Components.Services
{
    public class BillService : IBillService
    {

        private readonly AppDbContext db;
        private readonly IPaymentOS _icommon;

        public BillService(AppDbContext _db, IPaymentOS icommon)
        {
            db = _db;
            _icommon = icommon;

        }
        public async Task<List<Product_bill>> GetAllProduct_bill()
        {
            return await db.Product_bills.ToListAsync();
        }
        public async Task<List<BillVanDonDTO>> GetBill()
        {
            var query = from bill in db.Bill
                        join user in db.Users on bill.UserID equals user.UserId
                        select new BillVanDonDTO
                        {
                            BillId = bill.BillId,
                            UserID = bill.UserID,
                            AddressId = bill.AddressId,
                            DeliveryId = bill.DeliveryId,
                            PayStatus = bill.PayStatus,
                            TotalPrice = bill.TotalPrice,
                            Note = bill.Note,
                            Created_at = bill.Created_at,
                            Updated_at = bill.Updated_at,
                            Status = bill.Status,
                            ApproveBill = bill.ApproveBill,
                            UserName = user.Username,
                        };
            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(b => b.Created_at)
                .ToListAsync();
            return items;
        }
        public async Task<List<BillVanDonDTO>> GetBillBySatus(string status )
        {
            var query = from bill in db.Bill
                        join user in db.Users on bill.UserID equals user.UserId
                        where bill.Status == status  // Lọc theo trạng thái
                        select new BillVanDonDTO
                        {
                            BillId = bill.BillId,
                            UserID = bill.UserID,
                            AddressId = bill.AddressId,
                            DeliveryId = bill.DeliveryId,
                            PayStatus = bill.PayStatus,
                            TotalPrice = bill.TotalPrice,
                            Note = bill.Note,
                            Created_at = bill.Created_at,
                            Updated_at = bill.Updated_at,
                            Status = bill.Status,
                            ApproveBill = bill.ApproveBill,
                            UserName = user.Username,
                        };
            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(b => b.Created_at)
                .ToListAsync();
            return items;
        }

        public async Task DeleteProductFromBill(int billId, int productId)
        {
            // Tìm sản phẩm trong hóa đơn bằng BillId và ProductId
            var productBill = await db.Product_bills
                                       .FirstOrDefaultAsync(pb => pb.BillId == billId && pb.ProductId == productId);

            // Nếu tìm thấy sản phẩm, xóa nó
            if (productBill != null)
            {
                db.Product_bills.Remove(productBill);
                await db.SaveChangesAsync();
            }
            else
            {
                // Nếu không tìm thấy sản phẩm, có thể thông báo lỗi hoặc làm gì đó
                // Ví dụ: throw new Exception("Sản phẩm không có trong hóa đơn.");
            }
        }

        public async Task DeleteAllProductsFromBill(int billId)
        {
            // Tìm tất cả các sản phẩm liên quan đến hóa đơn
            var productBills = await db.Product_bills
                                       .Where(pb => pb.BillId == billId)
                                       .ToListAsync();

            // Nếu có sản phẩm, xóa tất cả
            if (productBills.Any())
            {
                db.Product_bills.RemoveRange(productBills);
                await db.SaveChangesAsync();
            }
            else
            {
                // Nếu không có sản phẩm, có thể thông báo hoặc xử lý
                // Ví dụ: throw new Exception("Không có sản phẩm nào trong hóa đơn này.");
            }
        }


        public async Task EditProduct_bill(Product_bill Product_bill)
        {
            db.Product_bills.Update(Product_bill);
            await db.SaveChangesAsync();
        }
        public async Task EditBill(Bill bill)
        {
            db.Bill.Update(bill);
            await db.SaveChangesAsync();
        }
        public async Task Deletebill(Bill bill)
        {
            db.Bill.Remove(bill);
            await db.SaveChangesAsync();
        }

        public async Task<List<Bill>> GetAllbill()
        {
            return await db.Bill.ToListAsync();
        }
        public async Task<List<Delivery>> GetAllDelivery()
        {
            return await db.Deliveries.ToListAsync();
        }
        public async Task<Delivery?> GetDeById(int DeId)
        {
            var d = await db.Deliveries.FirstOrDefaultAsync(p => p.DeliveryId == DeId);
            if (d == null)
            {
                return null;
            }

            return d;
        }
        public async Task<Bill> GetBillById(int BillId)
        {
            var d = await db.Bill.FirstOrDefaultAsync(p => p.BillId == BillId);
            if (d == null)
            {
                return null;
            }

            return d;
        }


        public async Task<List<DoanhThuViewModel>> GetAllBillDoanhThu(DateTime? fromDate, DateTime? toDate)
        {
            var fromDateOnly = fromDate?.Date ?? DateTime.MinValue;
            var toDateOnly = toDate?.Date ?? DateTime.MaxValue;

            var result = await (from b in db.Bill
                                join u in db.Users on b.UserID equals u.UserId
                                where b.Created_at.HasValue && b.Created_at.Value.Date >= fromDateOnly && b.Created_at.Value.Date <= toDateOnly
                                orderby b.Created_at descending
                                select new DoanhThuViewModel
                                {
                                    BillId = b.BillId,
                                    BillCreatedAt = b.Created_at,
                                    UserName = u.Username,
                                    TotalPrice = b.TotalPrice,
                                    OrderStatus = b.Status,
                                    PayStatus = b.PayStatus
                                }).ToListAsync();

            return result.ToList();
        }
        public async Task<List<TopProductViewModel>> GetTopProducts(DateTime? fromDate, DateTime? toDate)
        {
            var fromDateOnly = fromDate?.Date ?? DateTime.MinValue;
            var toDateOnly = toDate?.Date ?? DateTime.MaxValue;

            var bills = await (from b in db.Bill
                               join pb in db.Product_bills on b.BillId equals pb.BillId
                               join p in db.Products on pb.ProductId equals p.ProductId
                               where b.Created_at.HasValue && b.Created_at.Value.Date >= fromDateOnly && b.Created_at.Value.Date <= toDateOnly

                               select new
                               {
                                   ProductId = pb.ProductId,
                                   ProductName = p.ProductName,
                                   Quantity = pb.Quantity,
                                   TotalPrice = b.TotalPrice,
                                   Price = p.SellPrice
                               }).ToListAsync();

            // Lấy các sản phẩm bán chạy nhất
            var topProducts = bills
                              .GroupBy(b => b.ProductId)
                              .Select(g => new TopProductViewModel
                              {
                                  ProductId = g.Key,
                                  ProductName = g.FirstOrDefault().ProductName,
                                  TotalQuantity = g.Sum(b => b.Quantity),
                                  TotalPrice = g.Sum(b => b.TotalPrice),
                                  Price = g.FirstOrDefault().Price
                              })
                              .OrderByDescending(g => g.TotalQuantity)
                              .Take(5)
                              .ToList();

            return topProducts;
        }
        //public async Task<(decimal doanhThu, int doanhSo, int soDon)> GetDoanhThuTheoTrangThai(DateTime currentDate, string? orderStatus = null)
        //{
        //    // Xác định ngày đầu và ngày cuối của tháng hiện tại
        //    var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        //    var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        //    var query = from b in db.Bill
        //                join pb in db.Product_bills on b.BillId equals pb.BillId
        //                where b.Created_at >= startOfMonth
        //                      && b.Created_at <= endOfMonth
        //                      && b.ApproveBill == true
        //                select new
        //                {
        //                    TotalPrice = b.TotalPrice,
        //                    Quantity = pb.Quantity,
        //                    BillId = b.BillId,
        //                    Status = b.Status // Bao gồm Status trong kiểu ẩn danh
        //                };

        //    // Thêm điều kiện lọc trạng thái nếu có
        //    if (!string.IsNullOrEmpty(orderStatus))
        //    {
        //        query = query.Where(b => b.Status == orderStatus);
        //    }

        //    var bills = await query.ToListAsync();

        //    // Tính tổng doanh thu, doanh số và số đơn hàng
        //    decimal doanhThu = bills.Sum(b => b.TotalPrice);
        //    int doanhSo = bills.Sum(b => b.Quantity);
        //    int soDon = bills.Select(b => b.BillId).Distinct().Count();

        //    return (doanhThu, doanhSo, soDon);
        //}

        public async Task<(decimal doanhThu, int doanhSo, int soDon)> GetDoanhThuThangHienTai(DateTime currentDate)
        {
            // Xác định ngày đầu và ngày cuối của tháng hiện tại
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var bills = await (from b in db.Bill
                               join pb in db.Product_bills on b.BillId equals pb.BillId
                               where b.Created_at >= startOfMonth
                                     && b.Created_at <= endOfMonth
                                     && b.Status == "Hoàn thành"
                                     && b.ApproveBill == true
                               select new
                               {
                                   TotalPrice = b.TotalPrice,
                                   Quantity = pb.Quantity,
                                   BillId = b.BillId // Thêm BillId để đếm số đơn
                               }).ToListAsync();

            // Tính tổng doanh thu, doanh số và số đơn hàng
            decimal doanhThu = bills.Sum(b => b.TotalPrice);
            int doanhSo = bills.Sum(b => b.Quantity);
            int soDon = bills.Select(b => b.BillId).Count(); // Đếm số lượng các hóa đơn duy nhất

            return (doanhThu, doanhSo, soDon);
        }
        public async Task<PagedResult<BillVanDonDTO>> GetBillCho(int page, int pageSize)
        {
            var query = from bill in db.Bill
                        join user in db.Users on bill.UserID equals user.UserId
                        where bill.Status == "Chờ"
                        select new BillVanDonDTO
                        {
                            BillId = bill.BillId,
                            UserID = bill.UserID,
                            AddressId = bill.AddressId,
                            DeliveryId = bill.DeliveryId,
                            PayStatus = bill.PayStatus,
                            TotalPrice = bill.TotalPrice,
                            Note = bill.Note,
                            Created_at = bill.Created_at,
                            Updated_at = bill.Updated_at,
                            Status = bill.Status,
                            ApproveBill = bill.ApproveBill,
                            UserName = user.Username,
                        };

            var totalCount = await query.CountAsync();

            var items = await query
         .OrderByDescending(b => b.Created_at)
         .Skip(page * pageSize)
         .Take(pageSize)
         .ToListAsync();
            return new PagedResult<BillVanDonDTO>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        public async Task<PagedResult<BillVanDonDTO>> GetBillLayhang(int page, int pageSize)
        {
            var query = from bill in db.Bill
                        join user in db.Users on bill.UserID equals user.UserId
                        where bill.Status == "Đang chờ lấy hàng"
                        select new BillVanDonDTO
                        {
                            BillId = bill.BillId,
                            UserID = bill.UserID,
                            AddressId = bill.AddressId,
                            DeliveryId = bill.DeliveryId,
                            PayStatus = bill.PayStatus,
                            TotalPrice = bill.TotalPrice,
                            Note = bill.Note,
                            Created_at = bill.Created_at,
                            Updated_at = bill.Updated_at,
                            Status = bill.Status,
                            ApproveBill = bill.ApproveBill,
                            UserName = user.Username,
                        };

            var totalCount = await query.CountAsync();

            var items = await query
         .OrderByDescending(b => b.Created_at)
         .Skip(page * pageSize)
         .Take(pageSize)
         .ToListAsync();
            return new PagedResult<BillVanDonDTO>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        public async Task<PagedResult<BillVanDonDTO>> GetBillDanggiao(int page, int pageSize)
        {
            var query = from bill in db.Bill
                        join user in db.Users on bill.UserID equals user.UserId
                        where bill.Status == "Đang giao"
                        select new BillVanDonDTO
                        {
                            BillId = bill.BillId,
                            UserID = bill.UserID,
                            AddressId = bill.AddressId,
                            DeliveryId = bill.DeliveryId,
                            PayStatus = bill.PayStatus,
                            TotalPrice = bill.TotalPrice,
                            Note = bill.Note,
                            Created_at = bill.Created_at,
                            Updated_at = bill.Updated_at,
                            Status = bill.Status,
                            ApproveBill = bill.ApproveBill,
                            UserName = user.Username,
                        };

            var totalCount = await query.CountAsync();

            var items = await query
         .OrderByDescending(b => b.Created_at)
         .Skip(page * pageSize)
         .Take(pageSize)
         .ToListAsync();
            return new PagedResult<BillVanDonDTO>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        public async Task<PagedResult<BillVanDonDTO>> GetBillHoanthanh(int page, int pageSize)
        {
            var query = from bill in db.Bill
                        join user in db.Users on bill.UserID equals user.UserId
                        where bill.Status == "Hoàn thành"
                        select new BillVanDonDTO
                        {
                            BillId = bill.BillId,
                            UserID = bill.UserID,
                            AddressId = bill.AddressId,
                            DeliveryId = bill.DeliveryId,
                            PayStatus = bill.PayStatus,
                            TotalPrice = bill.TotalPrice,
                            Note = bill.Note,
                            Created_at = bill.Created_at,
                            Updated_at = bill.Updated_at,
                            Status = bill.Status,
                            ApproveBill = bill.ApproveBill,
                            UserName = user.Username,
                        };

            var totalCount = await query.CountAsync();

            var items = await query
         .OrderByDescending(b => b.Created_at)
         .Skip(page * pageSize)
         .Take(pageSize)
         .ToListAsync();
            return new PagedResult<BillVanDonDTO>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
        public async Task<bool> ApproveProductBillAsync(BillVanDonDTO bill)
        {
            var checkBill = await db.Bill.FirstOrDefaultAsync(x => x.BillId == bill.BillId);
            if (checkBill == null)
            {
                return false;
            }
            if (checkBill.ApproveBill == true)
            {
                return false;
            }
            checkBill.Status ="Đang xử lí";
            checkBill.Note = "Đã duyệt đơn";
            checkBill.ApproveBill = true;
            checkBill.Status = "Đang chờ lấy hàng";
            checkBill.Updated_at = DateTime.Now;
            await db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteProductBillAsync(BillVanDonDTO bill)
        {
            var checkBill = await db.Bill.FirstOrDefaultAsync(x => x.BillId == bill.BillId);
            if (checkBill == null)
            {
                return false;
            }
            checkBill.Status = "Đã Hủy"; // Chuyển trạng thái hóa đơn thành "Đã hủy"
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CompleteProductBillAsync(BillVanDonDTO bill)
        {
            var checkBill = await db.Bill.FirstOrDefaultAsync(x => x.BillId == bill.BillId);
            if (checkBill == null)
            {
                return false;
            }
            checkBill.Updated_at = DateTime.Now;
            checkBill.Status = "Hoàn thành"; // Chuyển trạng thái hóa đơn thành "hoàn thành"
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductBillDetailDto>> GetProductsByBillId(int billId)
        {
            DateTime today = DateTime.Today;

            // Truy vấn sản phẩm và áp dụng khuyến mãi nếu có
            var products = await (
                from pb in db.Product_bills
                join bill in db.Bill on pb.BillId equals bill.BillId
                join p in db.Products on pb.ProductId equals p.ProductId
                join d in db.Discount
                    .Where(d => d.StartDate <= today && d.EndDate >= today)
                    on p.ProductId equals d.ProductId into discountGroup
                from discount in discountGroup.DefaultIfEmpty()
                where pb.BillId == billId // Lọc theo billId
                select new ProductBillDetailDto
                {
                    ProductBillId = pb.ProductBillId,
                    Quantity = pb.Quantity,
                    Price = discount != null ? discount.SellPrice : pb.Price, // Nếu có khuyến mãi, dùng giá khuyến mãi
                    Bills = bill,
                    Product = p,
                }
            ).OrderByDescending(p => p.ProductBillId).ToListAsync();

            return products;
        }

        public async Task<List<ProductBillDetailDto>> GetProductsByBill()
        {
            var products = await (from pb in db.Product_bills
                                  join bill in db.Bill on pb.BillId equals bill.BillId
                                  join p in db.Products on pb.ProductId equals p.ProductId

                                  select new ProductBillDetailDto
                                  {
                                      ProductBillId = pb.ProductBillId,
                                      Quantity = pb.Quantity,
                                      Price = pb.Price,
                                      Bills = bill,
                                      Product = p,
                                  }).OrderByDescending(p => p.ProductBillId).ToListAsync();

            return products;
        }

        public async Task<Model.Address> GetAddressUser(int UserID)
        {
            var addressUser = await db.Address.FirstOrDefaultAsync(x => x.UserId == UserID);
            if (addressUser == null)
            {
                return new Model.Address();
            }
            return addressUser;
        }

        public async Task<(Delivery, int code, string message)> SetDeliveryBill(int? BillID, int DeliveryID, int WarehouseID)
        {
            var bill = await db.Bill.FirstOrDefaultAsync(x => x.BillId == BillID);
            if (bill == null)
            {
                return (new Delivery(), 404, "Không có hóa đơn");
            }
            var deleveryCheck = await db.Deliveries.FirstOrDefaultAsync(x => x.DeliveryId == DeliveryID);
            if (deleveryCheck == null)
            {
                return (new Delivery(), 404, "Không có đơn vị vận chuyển");

            }

            bill.DeliveryId = deleveryCheck.DeliveryId;
            bill.Status = "Đang giao";
          //  bill.WarehouseID = WarehouseID;
            // Cập nhật số lượng sản phẩm trong kho
            // Lọc chỉ các sản phẩm thuộc hóa đơn cụ thể
            var productBills = await db.Product_bills
                .Where(pb => pb.BillId == BillID) // Thêm điều kiện để chỉ lấy các sản phẩm trong hóa đơn hiện tại
                .ToListAsync();

            foreach (var detail in productBills)
            {
                var warehouseProduct = await db.Products
                    .FirstOrDefaultAsync(wp => wp.ProductId == detail.ProductId);

                //if (warehouseProduct == null)
                //{
                //    return (new Delivery(), 404, $"Không tìm thấy sản phẩm {detail.ProductId}");
                //}

                //if (warehouseProduct.Quantity < detail.Quantity)
                //{
                //    return (new Delivery(), 400, $"Số lượng sản phẩm {detail.ProductId} không đủ trong kho");
                //}

                //// Trừ số lượng trong kho
                //warehouseProduct.Quantity -= detail.Quantity;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await db.SaveChangesAsync();


            return (deleveryCheck, 200, "Thành công");
        }

        public async Task<(Bill, int code, string message)> ConfirmOrder(Bill model, string type, ShippingInfoDTO address)
        {
            var checkBill = await db.Bill.FirstOrDefaultAsync(x => x.BillId == model.BillId);
            if (checkBill == null)
            {
                return (model, 400, "Không tìm thấy hóa đơn");

            }
            if (checkBill.Status != "Chờ")
            {
                return (model, 400, "Hóa đơn đang được đợi duyệt");

            }
            Model.Address newAddress = new Model.Address();
            newAddress.UserId = address.UserID;
            newAddress.DetailAddress = address.SelectedDistrict + " , " + address.SelectedCity +","+address.ReceiveAddress;
            newAddress.Phone = address.PhoneNumber;
            newAddress.Name = address.ReceiverName;
            newAddress.CreatedAt = DateTime.Now;
            newAddress.UpdatedAt = DateTime.Now;
            await db.Address.AddAsync(newAddress);
            await db.SaveChangesAsync();

            if (type == "Tiền mặt")
            {
                checkBill.PayStatus = type;
                checkBill.Note = "Thanh toán khi nhận hàng";
                checkBill.Status ="Đang xử lí";
                checkBill.Updated_at = DateTime.Now;
                checkBill.AddressId = newAddress.AddressId;
                await db.SaveChangesAsync();
                return (model, 200, "Đã xác nhận đơn hàng, chờ duyệt");
            }
            else
            {
                DateTime today = DateTime.Today;

                // Truy vấn sản phẩm và áp dụng khuyến mãi nếu có
                var products = await (
                    from pb in db.Product_bills
                    join bill in db.Bill on pb.BillId equals bill.BillId
                    join p in db.Products on pb.ProductId equals p.ProductId
                    join d in db.Discount
                        .Where(d => d.StartDate <= today && d.EndDate >= today)
                        on p.ProductId equals d.ProductId into discountGroup
                    from discount in discountGroup.DefaultIfEmpty()
                    where  pb.BillId == checkBill.BillId // Lọc theo billId
                    select new ProductBillDetailDto
                    {
                        ProductBillId = pb.ProductBillId,
                        Quantity = pb.Quantity,
                        Price = discount != null ? discount.SellPrice : pb.Price, // Nếu có khuyến mãi, dùng giá khuyến mãi
                        Bills = bill,
                        Product = p,
                    }
                ).ToListAsync();


                var link = await _icommon.CreatePaymentLink(products);
                if (link != null)
                {
                    return (checkBill, 202, link.checkoutUrl);

                }
            }

            return (model, 400, "Đã xảy ra lỗi");
        }
        public async Task<List<Warehouse>> GetAllWarehouse()
        {
            return await db.Warehouse.ToListAsync();
        }

        public async Task<TopProductViewModel> GetTotalQuantityP(int productId)
        {
            // Lấy thông tin hóa đơn và sản phẩm theo productId
            var bills = await (from b in db.Bill
                               join pb in db.Product_bills on b.BillId equals pb.BillId
                               join p in db.Products on pb.ProductId equals p.ProductId
                               where pb.ProductId == productId  // Lọc theo ProductId
                               select new
                               {
                                   ProductId = pb.ProductId,
                                   ProductName = p.ProductName,
                                   Quantity = pb.Quantity,
                                   TotalPrice = b.TotalPrice,
                                   Price = p.SellPrice
                               }).ToListAsync();

            // Tính tổng số lượng và tổng giá trị bán được cho sản phẩm theo productId
            var product = bills
                            .GroupBy(b => b.ProductId)
                            .Select(g => new TopProductViewModel
                            {
                                ProductId = g.Key,
                                ProductName = g.FirstOrDefault().ProductName,
                                TotalQuantity = g.Sum(b => b.Quantity),
                                TotalPrice = g.Sum(b => b.TotalPrice),
                                Price = g.FirstOrDefault().Price
                            })
                            .FirstOrDefault();

            return product;
        }

    }
}
