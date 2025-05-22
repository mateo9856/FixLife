using System.Net.Mail;

namespace FixLife.Admin.Db.Tools
{
    public class EmailSender
    {
        private string _smtpServer;

        public EmailSender()
        {
            _smtpServer = "your_smtp_server";
        }

        public void SendEmail(string from, string to)
        {
            using var smtpClient = new SmtpClient(_smtpServer);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("your_username", "your_password");
            smtpClient.Port = 587;

            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = "FixLife.Admin AuthenticationCode",
                Body = GeneateAuthenticationCodeHtml(),
                IsBodyHtml = true
            };
        }

        private string GeneateAuthenticationCodeHtml()
        {
            return $@"
                <html>
                    <body>
                        <h1>Authentication Code</h1>
                        <p>Your authentication code is: <strong>{CodeGenerator.GenerateAuthCode()}</strong></p>
                    </body>
                </html>";
        }
    }
}
