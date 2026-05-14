/*
 * Sinh vien:Nguyen Thi Man Duy
 * Ma sv:2123110470
 * Version 1.0
 * 
 */






using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Entities
{
    //thuc the danh muc bai viet
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // Tên danh mục (vd: Tin Giáo Dục)
        public string Description { get; set; }

        // Quan hệ: Một danh mục có nhiều bài viết
        public virtual ICollection<Post> Post { get; set; }
    }
}
