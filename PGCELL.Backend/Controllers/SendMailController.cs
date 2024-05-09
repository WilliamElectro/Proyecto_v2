using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;

namespace PGCELL.Backend.Controllers
{
    [ApiController]
    [Route("api/sendmail")] // Ruta base para las operaciones relacionadas con el envío de correos
    public class SendMailController : ControllerBase
    {
        [HttpPost("send")] // Ruta para el endpoint de envío de correo
        public IActionResult SendEmail(EmailData data)
        {
            try
            {
                var mail = new MailMessage();
                mail.To.Add(data.RecipientEmail);
                mail.Subject = data.Subject;
                mail.Body = data.Message;

                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Credentials = new System.Net.NetworkCredential("pgceel22@gmail.com", "ceij ylhz pnpp vqtg");
                smtpClient.Send(mail);

                return Ok(); // Se envió el correo exitosamente
            }
            catch (Exception ex)
            {
                // Manejar el error apropiadamente
                return StatusCode(500, ex.Message); // Error interno del servidor
            }
        }
    }

    public class EmailData
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
