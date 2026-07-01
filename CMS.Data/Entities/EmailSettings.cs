
/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 1.0
 * Ngay thuc hien: 27/6/2026
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsAPI.Settings
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string SenderName { get; set; }
    }
}