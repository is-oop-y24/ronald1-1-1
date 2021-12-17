using System;
using System.Collections.Generic;

namespace Banks
{
    public class ConsoleInterface
    {
        private CentralBank _centralBank;

        public ConsoleInterface(CentralBank centralBank)
        {
            _centralBank = centralBank;
        }

        public void NewLine(string line)
        {
            string[] words = line.Split(' ');
            string command = words[0];
            if (command.Equals("addBank"))
            {
                AddBank(words);
            }

            if (command.Equals("skipDays"))
            {
                SkipDays(words);
            }

            if (command.Equals("addDebitAccount"))
            {
                AddDebitAccount(words);
            }

            if (command.Equals("addDepositAccount"))
            {
                AddDepositAccount(words);
            }

            if (command.Equals("addCreditAccount"))
            {
                AddCreditAccount(words);
            }

            if (command.Equals("addClient"))
            {
                AddClient(words);
            }

            if (command.Equals("cancelTransaction"))
            {
                CancelTransaction(words);
            }

            if (command.Equals("addPassportToClient"))
            {
                AddPassportToClient(words);
            }

            if (command.Equals("banks"))
            {
                Banks();
            }

            if (command.Equals("bankClients"))
            {
                BankClients(words);
            }

            if (command.Equals("clientAccounts"))
            {
                ClientAccounts(words);
            }
        }

        private void AddClient(string[] words)
        {
            if (words.Length > 4 || words.Length < 3)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            if (words.Length == 4)
            {
                var client = new Client(words[1], words[2]);
                client = new ConfirmedClient(client, words[3]);
                _centralBank.AddClient(client);
            }
            else
            {
                var client = new Client(words[1], words[2]);
                _centralBank.AddClient(client);
            }
        }

        private void AddCreditAccount(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int id;
            int.TryParse(words[1], out id);
            try
            {
                _centralBank.AddCreditAccount(_centralBank.FindClient(id));
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void AddDepositAccount(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int id;
            int timeLimit;
            int.TryParse(words[1], out id);
            int.TryParse(words[2], out timeLimit);
            try
            {
                _centralBank.AddDepositAccount(_centralBank.FindClient(id), timeLimit);
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void AddDebitAccount(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int id;
            int.TryParse(words[1], out id);
            try
            {
                _centralBank.AddDebitAccount(_centralBank.FindClient(id));
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void SkipDays(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int days;
            int.TryParse(words[1], out days);
            try
            {
                _centralBank.SkipDays(days);
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void AddBank(string[] words)
        {
            if (words.Length != 6)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            float commissionPercent, payPercent, creditLimit, notConfirmedLimit;
            float.TryParse(words[2], out commissionPercent);
            float.TryParse(words[2], out payPercent);
            float.TryParse(words[2], out creditLimit);
            float.TryParse(words[2], out notConfirmedLimit);
            try
            {
                _centralBank.AddBank(words[1], commissionPercent, payPercent, creditLimit, notConfirmedLimit);
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void CancelTransaction(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int id;
            int.TryParse(words[1], out id);
            try
            {
                _centralBank.CancelTransaction(id);
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void AddPassportToClient(string[] words)
        {
            if (words.Length != 3)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int id;
            int.TryParse(words[1], out id);
            string passport = words[2];
            try
            {
                _centralBank.AddPassportToClient(_centralBank.FindClient(id), passport);
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void Banks()
        {
            foreach (Bank bank in _centralBank.Banks)
            {
                Console.WriteLine(bank.Name);
            }
        }

        private void BankClients(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            try
            {
                List<Client> clients = _centralBank.FindBank(words[1]).Clients;
                foreach (Client client in clients)
                {
                    Console.WriteLine(client.Name + " " + client.Surname + " " + client.Id);
                }
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }

        private void ClientAccounts(string[] words)
        {
            if (words.Length != 2)
            {
                Console.WriteLine("Invalid line!");
                return;
            }

            int id;
            int.TryParse(words[1], out id);
            try
            {
                Client client = _centralBank.FindClient(id);
                foreach (IAccount account in client.Accounts)
                {
                    Console.WriteLine(account.Id);
                }
            }
            catch
            {
                Console.WriteLine("Invalid line!");
            }
        }
    }
}