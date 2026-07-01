
using System.Net;
using System.Net.Mail;

namespace CMS.Backend.Helpers
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SmtpClient CreateSmtpClient()
        {
            var client = new SmtpClient(_configuration["EmailSettings:SmtpHost"] ?? "smtp.gmail.com")
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587"),
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SenderEmail"] ?? "",
                    _configuration["EmailSettings:SenderPassword"] ?? ""),
                EnableSsl = true
            };
            return client;
        }

        private MailMessage CreateMailMessage(string toEmail, string subject, string htmlBody)
        {
            var message = new MailMessage
            {
                From = new MailAddress(
                    _configuration["EmailSettings:SenderEmail"] ?? "",
                    _configuration["EmailSettings:SenderName"] ?? "Nàng Quýt Cosmetics"),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(toEmail);
            return message;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string code)
        {
            var subject = "🔐 Mã xác thực đặt lại mật khẩu - Nàng Quýt";
            var htmlBody = $@"<html><body style='font-family:Segoe UI;background:#f8f8f8'>
<div style='max-width:600px;margin:auto;background:#fff;border-radius:12px;padding:30px'>
<h2 style='color:#EC4899'>🌸 Nàng Quýt Cosmetics</h2>
<p>Xin chào,</p>
<p>Bạn đã yêu cầu đặt lại mật khẩu. Vui lòng sử dụng mã OTP bên dưới:</p>
<div style='font-size:34px;font-weight:bold;color:#F97316;text-align:center;padding:20px;border:2px dashed #F97316'>{code}</div>
<p>Mã có hiệu lực trong <b>15 phút</b>.</p>
<p>Nếu bạn không thực hiện yêu cầu này, hãy bỏ qua email.</p>
<hr/>
<p style='color:#888;font-size:12px'>© 2026 Nàng Quýt Cosmetics</p>
</div></body></html>";
            using var c = CreateSmtpClient();
            using var m = CreateMailMessage(toEmail, subject, htmlBody);
            await c.SendMailAsync(m);
        }

        public async Task SendOrderConfirmationEmailAsync(
            string toEmail, string customerName, int orderId, DateTime orderDate,
            string shippingName, string shippingPhone, string shippingAddress,
            string paymentMethod, List<OrderItemInfo> items, decimal totalAmount)
        {
            var rows = string.Join("", items.Select(i => $"<tr><td>{i.ProductName}</td><td>{i.Quantity}</td><td>{i.UnitPrice:N0}₫</td><td>{i.Quantity * i.UnitPrice:N0}₫</td></tr>"));
            var pay = paymentMethod == "VNPay" ? "Thanh toán trực tuyến (VNPay)" : "Thanh toán khi nhận hàng (COD)";
            var subject = $"💄 Xác nhận đơn hàng #{orderId} - Nàng Quýt";
            var html = $@"<html><body style='font-family:Segoe UI;background:#f8f8f8'>
<div style='max-width:700px;margin:auto;background:#fff;padding:30px;border-radius:12px'>
<h2 style='color:#EC4899'>🌸 Nàng Quýt Cosmetics</h2>
<p>Xin chào <b>{customerName}</b>, cảm ơn bạn đã mua sắm tại Nàng Quýt.</p>
<p><b>Mã đơn:</b> #{orderId}<br/>
<b>Ngày đặt:</b> {orderDate:dd/MM/yyyy HH:mm}</p>
<p><b>Người nhận:</b> {shippingName}<br/>
<b>Điện thoại:</b> {shippingPhone}<br/>
<b>Địa chỉ:</b> {shippingAddress}<br/>
<b>Thanh toán:</b> {pay}</p>
<table border='1' cellspacing='0' cellpadding='8' width='100%'>
<tr><th>Sản phẩm</th><th>SL</th><th>Đơn giá</th><th>Thành tiền</th></tr>
{rows}
<tr><td colspan='3' align='right'><b>Tổng cộng</b></td><td><b>{totalAmount:N0}₫</b></td></tr>
</table>
<p>Cảm ơn bạn đã lựa chọn Nàng Quýt. Chúc bạn luôn xinh đẹp! 🌸</p>
</div></body></html>";
            using var c = CreateSmtpClient();
            using var m = CreateMailMessage(toEmail, subject, html);
            await c.SendMailAsync(m);
        }
    }

    public class OrderItemInfo
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
