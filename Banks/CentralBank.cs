using System.Collections.Generic;
using System.Data;
using Banks.Tools;

namespace Banks
{
    public class CentralBank
    {
        private List<Bank> _banks;
        private List<Transaction> _transactions;
        private List<Client> _clients;

        public CentralBank()
        {
            _banks = new List<Bank>();
            _transactions = new List<Transaction>();
            _clients = new List<Client>();
        }

        private delegate void DayPassed();
        private event DayPassed NewDay;

        public List<Bank> Banks => new List<Bank>(_banks);
        public List<Transaction> Transactions => new List<Transaction>(_transactions);
        public Bank AddBank(string name, float commissionPercent, float payPercent, float creditLimit, float notConfirmedLimit)
        {
            IBuilder builder = new BankBuilder();
            builder.SetName(name);
            builder.SetCommission(commissionPercent);
            builder.SetPayPercent(payPercent);
            builder.SetCreditLimit(creditLimit);
            builder.SetNotConfirmedLimit(notConfirmedLimit);
            Bank bank = builder.GetResult();
            _banks.Add(bank);
            return bank;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void AddClientToBank(Client client, Bank bank)
        {
            bank.AddClient(client);
        }

        public DebitAccount AddDebitAccount(Client client)
        {
            var debitAccount = new DebitAccount(client.Id, this, client.Bank);
            client.AddAccount(debitAccount);
            NewDay += debitAccount.DayPassed;
            return debitAccount;
        }

        public DepositAccount AddDepositAccount(Client client, int timeLimit)
        {
            var depositAccount = new DepositAccount(client.Id, this, client.Bank, timeLimit);
            client.AddAccount(depositAccount);
            NewDay += depositAccount.DayPassed;
            return depositAccount;
        }

        public CreditAccount AddCreditAccount(Client client)
        {
            var creditAccount = new CreditAccount(client.Id, this, client.Bank);
            client.AddAccount(creditAccount);
            NewDay += creditAccount.DayPassed;
            return creditAccount;
        }

        public void SkipDays(int count)
        {
            for (int i = 0; i < count; i++)
            {
                NewDay?.Invoke();
            }
        }

        public Client FindClient(int id)
        {
            foreach (Client client in _clients)
            {
                if (client.Id == id)
                {
                    return client;
                }
            }

            throw new BanksException("Client not found: " + id);
        }

        public Bank FindBank(string name)
        {
            foreach (Bank bank in _banks)
            {
                if (bank.Name == name)
                {
                    return bank;
                }
            }

            throw new BanksException("Bank not found: " + name);
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void CancelTransaction(int id)
        {
            Transaction canceledTransaction = null;
            foreach (Transaction transaction in _transactions)
            {
                if (transaction.Id == id)
                {
                    canceledTransaction = transaction;
                }
            }

            if (canceledTransaction == null)
            {
                throw new BanksException("Transaction not found:" + id);
            }

            canceledTransaction.Cancel();
            _transactions.Remove(canceledTransaction);
        }

        public void AddPassportToClient(Client client, string passport)
        {
            if (passport.Length != 10)
            {
                throw new BanksException("Invalid passport: " + passport);
            }

            Bank bank = client.Bank;
            _clients.Remove(client);
            client = new ConfirmedClient(client, passport);
            _clients.Add(client);
            bank.ConfirmClient(client);
        }
    }
}