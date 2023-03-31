using System;
using System.Threading.Tasks;
using BookBird.Application.DTOs.Emails;
using BookBird.Application.Helpers;
using BookBird.Application.Options;
using BookBird.Application.Providers;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookBird.Infrastructure.Providers
{
    internal sealed class EmailProvider : IEmailProvider
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly SendGridOptions _sendGridOptions;

        public EmailProvider(ISendGridClient sendGridClient, IConfiguration configuration)
        {
            _sendGridClient = sendGridClient;
            _sendGridOptions = configuration.GetOptions<SendGridOptions>(nameof(SendGridOptions));
        }

        public async Task SendAsync(EmailDto email)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_sendGridOptions.EmailFrom, _sendGridOptions.EmailName),
                Subject = email.Subject,
                PlainTextContent = email.PlainContent
            };

            var attachment = CreateAttachment(email);

            if (attachment is not null)
            {
                message.Attachments.Add(attachment);
            }

            message.AddTo(new EmailAddress(email.ToEmail, email.ToName));
            
            await _sendGridClient
                .SendEmailAsync(message)
                .ConfigureAwait(false);
        }

        private static Attachment CreateAttachment(EmailDto email)
        {
            if (email.Attachment is null)
            {
                return null;
            }
            
            var content = Convert.ToBase64String(email.Attachment.Content);

            return new Attachment
            {
                Content = content,
                Type = email.Attachment.Type,
                Filename = email.Attachment.FileName,
                Disposition = email.Attachment.Disposition,
                ContentId = email.Attachment.ContentId
            };
        }
    }
}