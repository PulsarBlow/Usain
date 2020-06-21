namespace Usain.EventListener.Infrastructure.Security
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IRequestAuthenticator
    {
        Task<bool> IsAuthenticAsync(
            HttpRequest request,
            CancellationToken cancellationToken);
    }
}
