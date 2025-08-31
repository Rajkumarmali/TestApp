using System.Threading.Tasks;

namespace TestApp.Manager.interfaces;

public interface ITestEmail
{
    Task<string> SendEmail();
}