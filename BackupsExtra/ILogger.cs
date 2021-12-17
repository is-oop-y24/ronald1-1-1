namespace BackupsExtra
{
    public interface ILogger
    {
        public void SendMessage(string message);

        public void Warning(string message)
        {
            SendMessage("[Warning] : " + message);
        }

        public void Info(string message)
        {
            SendMessage("[Info] : " + message);
        }

        public void Error(string message)
        {
            SendMessage("[Error] : " + message);
        }
    }
}