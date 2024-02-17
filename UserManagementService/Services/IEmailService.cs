using BiletVarmi.Models;

namespace BiletVarmi.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
