namespace Usain.EventListener.Infrastructure.Logging
{
    public static class EventIds
    {
        public static class RequestAuthenticationMiddleware
        {
            public const int AuthenticationFailed
                = 10;

            public const int UnhandledException
                = 11;
        }

        public static class RequestAuthenticator
        {
            public const int RequestAuthenticationIsDisabled
                = 20;

            public const int InvokingSignatureVerification = 21;
        }

        public static class UsainServerMiddleware
        {
            public const int InvokingEndpointHandler = 30;
            public const int InvokingEndpointResult = 31;
            public const int UnhandledException = 32;
        }

        public static class CommandHandler
        {
            public const int HandlingCommand = 40;
            public const int CancellingCommand = 41;
            public const int CommandHandled = 42;
        }

        public static class EventsEndpointHandler
        {
            public const int ProcessingEvent = 50;
            public const int MethodNotAllowed = 51;
            public const int JsonDeserializationReturnNull = 52;
        }
    }
}
