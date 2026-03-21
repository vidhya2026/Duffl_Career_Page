namespace Duffl_career.Models
{
    // This class maps to the "EmailSettings" section in appsettings.json
    public class EmailSettings
    {
        public string SmtpServer { get; set; }    // Gmail SMTP server address
        public int Port { get; set; }              // Port 587 for TLS
        public string SenderEmail { get; set; }    // Your Gmail address
        public string SenderPassword { get; set; } // Your Gmail App Password
        public string SenderName { get; set; }     // Display name in email
    }
}
