namespace VendingMachine
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RegionInfo _currentRegion = new RegionInfo("RU");
        private ISingleCurrencyWallet CustomerWallet { get; set; }
        private ISingleCurrencyWallet VendingMachineWallet { get; set; }
        private ISingleCurrencyWallet ReceivedMoney { get; set; }
        private ICollection<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ReceivedMoney = new SingleCurrencyWallet(_currentRegion);
            CustomerWallet = InitCustomerWallet();
            VendingMachineWallet = InitVendingMachineWallet();
            RefreshData();
        }

        private void RefreshData()
        {
            var coins = CustomerWallet.GetCoinsAmount();
            customerWallet.ItemsSource = coins.Select(c => new { Name = c.Key.ToString(), Amount = c.Value, Value = c.Key.Denomination }).ToList();

            coins = VendingMachineWallet.GetCoinsAmount();
            vendingMachineWallet.ItemsSource = coins.Select(c => new { Name = c.Key.ToString(), Amount = c.Value, Value = c.Key.Denomination }).ToList();
            receivedMoneyButton.Text = ReceivedMoney.CalculateTotalSum().ToString();
        }

        private ISingleCurrencyWallet InitCustomerWallet()
        {
            var wallet = new SingleCurrencyWallet(_currentRegion);

            for (int i = 0; i < 10; i++)
            {
                wallet.Push(new Coin(1m, _currentRegion));
            }

            for (int i = 0; i < 30; i++)
            {
                wallet.Push(new Coin(2m, _currentRegion));
            }

            for (int i = 0; i < 20; i++)
            {
                wallet.Push(new Coin(5m, _currentRegion));
            }

            for (int i = 0; i < 15; i++)
            {
                wallet.Push(new Coin(10m, _currentRegion));
            }

            return wallet;
        }

        private ISingleCurrencyWallet InitVendingMachineWallet()
        {
            var wallet = new SingleCurrencyWallet(_currentRegion);

            foreach (var denomination in new[] { 1m, 2m, 5m, 10m })
            {
                for (int i = 0; i < 100; i++)
                {
                    wallet.Push(new Coin(denomination, _currentRegion));
                }
            }

            return wallet;
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var denomination = (decimal)button.DataContext;

            var coin = new Coin(denomination, _currentRegion);
            if (CustomerWallet.GetAmount(coin) > 0)
            {
                CustomerWallet.Pop(coin);
                ReceivedMoney.Push(coin);
                RefreshData();
            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            var changeSum = ReceivedMoney.CalculateTotalSum();

            // Сливаем все монетки в кошелек автомата и выдаем сдачу
            // ToDo надо механизм транзакций запилить
            var coins = ReceivedMoney.GetCoinsAmount();
            foreach (var pair in coins)
            {
                var coin = pair.Key;
                var amount = pair.Value;
                for (int i = 0; i < amount; i++)
                {
                    ReceivedMoney.Pop(coin);
                    VendingMachineWallet.Push(coin);
                }
            }

            var change = VendingMachineWallet.Withdraw(changeSum);
            foreach (var coin in change)
            {
                CustomerWallet.Push(coin);
            }

            changeTextBox.Text = String.Join(",", change.Select(c => c.ToString()));
            RefreshData();
        }
    }
}
