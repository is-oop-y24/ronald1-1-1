using System.Collections.Generic;

namespace Banks
{
    public class ConfirmedClient : ClientDecorator
    {
        private string _passport;
        public ConfirmedClient(Client client, string passport)
            : base(client)
        {
            _passport = passport;
        }

        public override Bank Bank
        {
            get
            {
                return Client.Bank;
            }
            set
            {
                Client.Bank = value;
            }
        }

        public override int Id
        {
            get
            {
                return Client.Id;
            }
            set
            {
                Client.Id = value;
            }
        }

        public override List<IAccount> Accounts
        {
            get
            {
                return new List<IAccount>(Client.Accounts);
            }
        }

        public override void AddAccount(IAccount account)
        {
            Client.AddAccount(account);
        }

        public override bool IsConfirmed()
        {
            return true;
        }
    }
}