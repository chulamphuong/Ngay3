using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngay3.Models
{
    public class Category
    {
        // tạo id sinh tu động bằng cách sử dụng thuộc tính [Key] và [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] //báo trình biên dịch biết Id là khóa chính
        [DataType(DataType.Text)] //báo trình biên dịch biết Id là kiểu dữ liệu Text
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //báo trình biên dịch biết Id là kiểu dữ liệu tự động tăng
        [Required(ErrorMessage = "Id is required")] //báo trình biên dịch biết Id là bắt buộc phải có
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")] //báo trình biên dịch biết Id phải lớn hơn 0
        [Display(Name = "Category Id")] //báo trình biên dịch biết Id là tên hiển thị của Category
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")] //báo trình biên dịch biết Name là bắt buộc phải có
        [StringLength(100, MinimumLength =2, ErrorMessage = "Name cannot be longer than 100 characters")] //báo trình biên dịch biết Name không được dài hơn 100 ký tự từ 2 ký tự trở lên
        public string Name { get; set; } = string.Empty; //báo trình biên dịch biết khởi tạo giá trị mặc định cho Name là chuỗi rỗng
         
         public List<Product>? Products { get; set; } = new List<Product>(); //báo trình biên dịch biết Products là kiểu dữ liệu List<Product> và khởi tạo giá trị mặc định là danh sách rỗng

    }
}
