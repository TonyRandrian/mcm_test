using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Mcm.Shared.Application.Services
{
    public class SmtpSettings
    {
        public required string Host { get; set; }
        public int Port { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool EnableSsl { get; set; }
        public required string DisplayName { get; set; }
        public required string FrontUrl { get; set; }
    }
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtp;

        public MailService(IOptions<SmtpSettings> smtpOptions)
        {
            _smtp = smtpOptions.Value;
        }

        public async Task SendInvitationAsync(MailInvitationRequest request)
        {
            try
            {
                string body = htmlTemplate
                        .Replace("{{firstName}}", request.FirstName)
                        .Replace("{{lastName}}", request.LastName)
                        .Replace("{{email}}", request.Email)
                        .Replace("{{companyName}}", request.CompanyName)
                        .Replace("{{token}}", request.Token)
                        .Replace("{{url}}", _smtp.FrontUrl)
                        .Replace("{{expirationDate}}",  DateTime.UtcNow.AddDays(2).ToString("dd/MM/yyyy"))
                        .Replace("{{year}}", DateTime.UtcNow.Year.ToString());

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtp.Email, _smtp.DisplayName),
                    Subject = "Mcm - Email d'Invitation",
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(new MailAddress(request.Email));

                var smtp = new SmtpClient(_smtp.Host, _smtp.Port)
                {
                  EnableSsl = _smtp.EnableSsl,
                  Credentials = new NetworkCredential(_smtp.Email, _smtp.Password)
                };

                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
      private string htmlTemplate = @"
<!DOCTYPE html>
<html lang=""fr"">
<head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
    <title>Invitation M.C.M CRM</title>
</head>

<body style=""margin:0;padding:0;background-color:#f4f6f9;font-family:Arial,sans-serif;"">

<table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f6f9;padding:40px 0;"">
<tr>
<td align=""center"">

<table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 4px 12px rgba(0,0,0,0.08);"">

<!-- HEADER -->
<tr>
    <td style=""background:#1e293b;padding:30px;text-align:center;"">
        <h1 style=""color:#ffffff;margin:0;font-size:26px;"">M.C.M CRM</h1>
        <p style=""color:#cbd5e1;margin-top:8px;font-size:14px;"">
            Invitation à rejoindre votre espace collaboratif
        </p>
    </td>
</tr>

<!-- BODY -->
<tr>
<td style=""padding:40px 35px;"">

    <h2 style=""margin:0 0 15px 0;color:#1e293b;"">
        Bonjour {{firstName}} {{lastName}},
    </h2>

    <p style=""font-size:15px;line-height:1.6;color:#475569;"">
        Vous avez été invité(e) à rejoindre la plateforme
        <strong>M.C.M CRM</strong> au sein de l’entreprise
        <strong>{{companyName}}</strong>.
    </p>

    <!-- INFO BOX -->
    <table width=""100%"" cellpadding=""0"" cellspacing=""0""
           style=""margin:30px 0;background:#f8fafc;border:1px solid #e2e8f0;border-radius:10px;"">
        <tr>
            <td style=""padding:20px;"">

                <p style=""margin:0 0 10px 0;color:#334155;"">
                    <strong>Nom :</strong> {{firstName}} {{lastName}}
                </p>

                <p style=""margin:0 0 10px 0;color:#334155;"">
                    <strong>Email :</strong> {{email}}
                </p>

                <p style=""margin:0 0 10px 0;color:#334155;"">
                    <strong>Entreprise :</strong> {{companyName}}
                </p>

            </td>
        </tr>
    </table>

    <!-- Accepter -->
    <div style=""text-align:center;margin-top:30px;"">
        <a href=""{{url}}/invitation?token={{token}}""
           style=""display:inline-block;
                  background-color:#2563eb;
                  color:#ffffff;
                  text-decoration:none;
                  padding:14px 32px;
                  border-radius:8px;
                  font-size:16px;
                  font-weight:bold;"">
            Accepter l'invitation
        </a>
    </div>

    <!-- Refuser -->
    <div style=""text-align:center;margin-top:15px;"">
        <a href=""{{url}}/decline?token={{token}}""
           style=""color:#dc2626;
                  text-decoration:none;
                  font-size:14px;"">
            Refuser l'invitation
        </a>
    </div>

    <p style=""margin-top:30px;font-size:13px;color:#64748b;"">
        Si le bouton ne fonctionne pas, copiez ce lien :
    </p>

    <p style=""font-size:13px;word-break:break-all;color:#334155;"">
        {{url}}/accept?token={{token}}
    </p>

    <!-- EXPIRATION -->
    <p style=""margin-top:30px;font-size:12px;color:#94a3b8;line-height:1.6;"">
        Cette invitation est personnelle et sécurisée.<br/>
        Elle expirera le <strong>{{expirationDate}}</strong>.
    </p>

</td>
</tr>

<!-- FOOTER -->
<tr>
    <td style=""background:#f8fafc;padding:20px;text-align:center;border-top:1px solid #e2e8f0;"">
        <p style=""margin:0;font-size:12px;color:#64748b;"">
            © {{year}} M.C.M CRM — Tous droits réservés
        </p>
    </td>
</tr>

</table>

</td>
</tr>
</table>

</body>
</html>
";

    }
}
