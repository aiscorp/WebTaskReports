using System.Threading.Tasks;




namespace WebTaskReports.Interfaces.Services
{
    public interface IEmailService 
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }

}
