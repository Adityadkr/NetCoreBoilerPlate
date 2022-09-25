using CommonEntities.Models.Email;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.Services.IRepository
{
    public interface IEmailService
    {
        //Task SendEmail(Message message);
       
       // Task Send(MimeMessage mailMessage);

        //HOW TO CALL EMAIL
        //Message message = new Message(new string[] { user.EmailId }, EmailService.ReInvitaionSubject, body);
        //await _EmailService.SendEmail(message);
    }
}
