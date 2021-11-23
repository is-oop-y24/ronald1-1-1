using System;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTests
    {
        private CentralBank _centralBank;
        
        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void DebitAccount()
        {
            Bank bank = _centralBank.AddBank("1", 3.65f, 3.65f, 1000f, 2000);
            var client1 = new Client("Maria", "Bubkina");
            _centralBank.AddClient(client1);
            _centralBank.AddClientToBank(client1, bank);
            DebitAccount account1 = _centralBank.AddDebitAccount(client1);
            var client2 = new Client("Kolya", "Shishkin");
            _centralBank.AddClient(client2);
            _centralBank.AddClientToBank(client2, bank);
            DebitAccount account2 = _centralBank.AddDebitAccount(client1);
            account2.Replenishment(1000);
            account1.Replenishment(1000);
            _centralBank.SkipDays(30);
            Assert.True(Math.Abs(account1.Money - 1003) < 0.01);
            account1.Withdrawal(103);
            Assert.True(Math.Abs(account1.Money - 900) < 0.01);
            account2.Transaction(account1, 103);
            Assert.True(Math.Abs(account1.Money - 1003) < 0.01);
            Assert.True(Math.Abs(account2.Money - 900) < 0.01);
        }
        
        [Test]
        public void DepositAccount()
        {
            Bank bank = _centralBank.AddBank("1", 3.65f, 3.65f, 1000f, 2000);
            var client1 = new Client("Maria", "Bubkina");
            _centralBank.AddClient(client1);
            _centralBank.AddClientToBank(client1, bank);
            DepositAccount account1 = _centralBank.AddDepositAccount(client1, 30);
            var client2 = new Client("Kolya", "Shishkin");
            _centralBank.AddClient(client2);
            _centralBank.AddClientToBank(client2, bank);
            DepositAccount account2 = _centralBank.AddDepositAccount(client1, 40);
            account2.Replenishment(1000);
            account1.Replenishment(1000);
            _centralBank.SkipDays(35);
            Console.WriteLine(account1.Money);
            Assert.True(Math.Abs(account1.Money - 1003) < 0.01);
            account1.Withdrawal(103);
            Assert.True(Math.Abs(account1.Money - 900) < 0.01);
            Assert.False(account2.Transaction(account1, 103));
            Assert.False(account2.Withdrawal( 103));
            _centralBank.SkipDays(5);
            Assert.True(Math.Abs(account2.Money - 1004) < 0.01);
        }
        
        [Test]
        public void CreditAccount()
        {
            Bank bank = _centralBank.AddBank("1", 1f, 3.65f, 2000f, 2000);
            var client1 = new Client("Maria", "Bubkina");
            _centralBank.AddClient(client1);
            _centralBank.AddClientToBank(client1, bank);
            CreditAccount account1 = _centralBank.AddCreditAccount(client1);
            account1.Replenishment(1000);
            _centralBank.SkipDays(30);
            Assert.True(Math.Abs(account1.Money - 1000) < 0.01);
            account1.Withdrawal(2000);
            _centralBank.SkipDays(30);
            Assert.True(Math.Abs(account1.Money - (-1030)) < 0.01);
            Assert.False(account1.Withdrawal(2000f));
        }

        [Test]
        public void ConfirmClient()
        {
            Bank bank = _centralBank.AddBank("1", 1f, 3.65f, 2000f, 2000);
            var client1 = new Client("Maria", "Bubkina");
            _centralBank.AddClient(client1);
            _centralBank.AddClientToBank(client1, bank);
            IAccount account1 = _centralBank.AddDebitAccount(client1);
            account1.Replenishment(3000);
            Assert.False(account1.Withdrawal(2500));
            _centralBank.AddPassportToClient(client1, "1111111111");
            client1 = _centralBank.FindClient(client1.Id);
            account1 = client1.Accounts[0];
            Assert.True(account1.Withdrawal(2500));
        }

        [Test]
        public void Transactions()
        {
            Bank bank = _centralBank.AddBank("1", 3.65f, 3.65f, 1000f, 2000);
            var client1 = new Client("Maria", "Bubkina");
            _centralBank.AddClient(client1);
            _centralBank.AddClientToBank(client1, bank);
            DebitAccount account1 = _centralBank.AddDebitAccount(client1);
            var client2 = new Client("Kolya", "Shishkin");
            _centralBank.AddClient(client2);
            _centralBank.AddClientToBank(client2, bank);
            DebitAccount account2 = _centralBank.AddDebitAccount(client2);
            account2.Replenishment(1000);
            account1.Replenishment(1000);
            account2.Transaction(account1, 500);
            Assert.True(Math.Abs(account1.Money - 1500) < 0.01);
            Assert.True(Math.Abs(account2.Money - 500) < 0.01);
            _centralBank.CancelTransaction(_centralBank.Transactions[0].Id);
            Assert.True(Math.Abs(account1.Money - 1000) < 0.01);
            Assert.True(Math.Abs(account2.Money - 1000) < 0.01);
        }
    }
}