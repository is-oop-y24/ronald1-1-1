using System.Collections.Generic;

namespace Banks
{
    public class Bank
    {
        private List<Client> _clients;

        public Bank()
        {
            _clients = new List<Client>();
        }

        public float Commission { get; set; }

        public float PayPercent { get; set; }
        public float CreditLimit { get; set; }

        public float NotConfirmedLimit { get; set; }
        public string Name { get; set; }

        public List<Client> Clients => new List<Client>(_clients);

        public void AddAccountToClient(Client client, IAccount account)
        {
            client.AddAccount(account);
        }

        public void AddClient(Client client)
        {
            client.Bank = this;
            _clients.Add(client);
        }

        public void ConfirmClient(Client confirmedClient)
        {
            Client oldClient = null;
            foreach (Client client in _clients)
            {
                if (client.Id == confirmedClient.Id)
                {
                    oldClient = client;
                }
            }

            _clients.Remove(oldClient);
            _clients.Add(confirmedClient);
        }
    }
}