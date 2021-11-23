namespace Banks
{
    public class ClientDecorator : Client
    {
        public ClientDecorator(Client client)
            : base(client.Name, client.Surname)
        {
            Client = client;
        }

        protected Client Client { get; }
    }
}