namespace WebSocketTrisServer
{
    public class Message
    {
        public enum Code
        {
            StartGame
        }

        public Code MessageCode { get; set; }
        public string Body { get; set; }
    }
}
