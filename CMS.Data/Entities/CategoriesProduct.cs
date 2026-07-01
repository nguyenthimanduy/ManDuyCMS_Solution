/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 1.0
 * Ngay thuc hien: 21/5/2026
 */
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Entities
{
    public class CategoriesProduct
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;


        public string? Description { get; set; }


        public string? ImageUrl { get; set; }



        public ICollection<Product> Products { get; set; }
            = new List<Product>();

    }
}