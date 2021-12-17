using System.Collections.Generic;

namespace Banks
{
    public class Client
    {
        private static int id = 0;
        private List<IAccount> _accounts;
        private string _name;
        private string _surname;
        private int _id;

        public Client(string name, string surname)
        {
            _name = name;
            _id = ++id;
            _surname = surname;
            _accounts = new List<IAccount>();
        }

        public virtual Bank Bank { get; set; }
        public virtual int Id { get; set; }
        public string Name { get; }
        public string Surname { get; }
        public virtual List<IAccount> Accounts => new List<IAccount>(_accounts);
        public virtual void AddAccount(IAccount account)
        {
            _accounts.Add(account);
        }

        public virtual bool IsConfirmed()
        {
            return false;
        }
    }
}