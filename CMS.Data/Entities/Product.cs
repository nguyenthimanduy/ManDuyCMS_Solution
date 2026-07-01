
/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 1.0
 * Ngay thuc hien: 21/6/2026
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        public string Name { get; set; } = string.Empty;



        public string? Description { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }



        public int StockQuantity { get; set; }



        public string? ImageUrl { get; set; }



        public int CategoryProductId { get; set; }



        [ForeignKey("CategoryProductId")]
        public CategoriesProduct? Category { get; set; }



        // QUAN HỆ ORDER
        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();

    }
}