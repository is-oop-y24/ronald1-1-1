using System.Collections.Generic;

namespace Banks
{
    public class Bank
    {
        private string _name;
        private List<Client> _clients;

        public Bank(string name, float commission, float payPercent, float creditLimit, float notConfirmedLimit)
        {
            _clients = new List<Client>();
            _name = name;
            Commission = commission;
            PayPercent = payPercent;
            CreditLimit = creditLimit;
            NotConfirmedLimit = notConfirmedLimit;
        }

        public float Commission { get; }

        public float PayPercent { get; }
        public float CreditLimit { get; }

        public float NotConfirmedLimit { get; }

        public List<Client> Clients => new List<Client>(_clients);

        public string Name { get; }

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