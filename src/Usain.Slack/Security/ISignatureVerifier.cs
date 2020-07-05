namespace Usain.Slack.Security
{
    public interface ISignatureVerifier
    {
        bool Verify(
            string signature,
            long timestamp,
            string message);
    }
}
