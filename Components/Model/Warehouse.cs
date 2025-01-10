using System.ComponentModel.DataAnnotations;

namespace BanSach.Components.Model
{
    public class Warehouse
    {
        [Key]
        public int WarehouseID { get; set; }
        public string NameWarehouse { get; set; }
    }
}
