using MimeKit;

namespace PGCELL.Backend.Helpers
{
    public interface ISmtpClient
    {
        void Connect(string host, int port, bool useSsl);

        void Authenticate(string username, string password);

        void Send(MimeMessage message);

        void Disconnect(bool quit);
    }
}