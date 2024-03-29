using ClosedXML.Excel;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;

namespace BOXCricket.CF
{
    public class CommonFunctions
    {
        #region Export Excel
        public static IActionResult ExportExcel(DataTable dt)
        {
            //Name of File  
            string fileName = "BookingList.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add dt in worksheet  
                wb.Worksheets.Add(dt, fileName);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }
        #endregion

        #region Generate Token
        public static string GenerateToken(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }
        #endregion

        #region Method TO Send Email
        public static void SendEmail(string Email, string Subject, string Message)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("blind.basic@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(Email));
            emailToSend.Subject = Subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = Message };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("blind.basic@gmail.com", "your authentication token");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
        }
        #endregion
    }
}
