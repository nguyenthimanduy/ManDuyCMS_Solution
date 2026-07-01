
/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 1.0
 * Ngay thuc hien: 21/5/2026
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Entities
{
    // Khách hàng
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Mật khẩu đã được băm bằng BCrypt

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<CustomerAddress>? Addresses { get; set; }
    }
}
