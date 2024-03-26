using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.JavaScript;

namespace OTPResearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<IEnumerable<WeatherForecast>> SendMailAsync()
        {

            #region Send mail
            // Tạo nội dung Email
            MailMessage message = new MailMessage(
            from: "tester@otpresearch.iam.gserviceaccount.com",
                to: "thanhduy.nolimits@gmail.com",
                subject: "Test SMTP",
                body: "You are welcome"
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress("test@cornmail.com"));
            message.Sender = new MailAddress("test@cornmail.com");

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                // Tạo xác thực bằng địa chỉ gmail và password
                client.Credentials = new NetworkCredential("tester.otpresearch@gmail.com", "rggg thav cmsu lxbn");
                client.EnableSsl = true;

                await client.SendMailAsync(message);
            }
            #endregion

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
