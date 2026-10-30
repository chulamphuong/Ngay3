using System.ComponentModel.DataAnnotations;

namespace Ngay3.Models
{
    public class Product
    {
        public int Id { get; set; } // Mã hàng hóa

        [Required]
        public string Name { get; set; } // Tên hàng hóa

        public decimal Price { get; set; } // Giá

        public string Description { get; set; } // Mô tả

        public int CategoryId { get; set; } // Mã danh mục
    }
}