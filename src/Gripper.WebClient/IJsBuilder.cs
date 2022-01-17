namespace Gripper.WebClient
{
    public interface IJsBuilder
    {
        public string ClickFirstByCssSelector(string selector);
        public string DocumentQuerySelectorAll(string selector);
        public string DocumentQuerySelector(string selector);
    }
}