
/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 1.0
 * Ngay thuc hien: 27/6/2026
 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Data.Entities
{
    public class CustomerAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string ReceiverName { get; set; } = string.Empty;

        [Required]
        public string ReceiverPhone { get; set; } = string.Empty;

        [Required]
        public string AddressLine { get; set; } = string.Empty;

        public bool IsDefault { get; set; } = false;

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
