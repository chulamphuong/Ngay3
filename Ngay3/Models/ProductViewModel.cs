
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ngay3.ViewModels
{
    public class ProductViewModel
    {
        // Thông tin sản phẩm
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(1, 1000000000, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        // Mã Category được chọn
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int? CategoryId { get; set; }

        // Danh sách Category lấy từ CategoryController
        public List<SelectListItem> Categories { get; set; }
            = new List<SelectListItem>();
    }
}
