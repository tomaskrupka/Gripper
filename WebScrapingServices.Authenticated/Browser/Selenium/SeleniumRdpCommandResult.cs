namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumRdpCommandResult : IRdpCommandResult
    {
        private string _message;
        public SeleniumRdpCommandResult(string message)
        {
            _message = message;
        }
        public string Message => _message;

        internal static SeleniumRdpCommandResult Success { get; private set; } = new SeleniumRdpCommandResult("Rdp command success.");

    }
}
