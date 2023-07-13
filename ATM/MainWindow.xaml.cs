using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ATM
{
    public partial class MainWindow : Window
    {
        private List<Card> Cards { get; set; } = new List<Card>();
        private Card currentlyCard; 
        private bool loggedin = false;

        public MainWindow()
        {
            Cards.Add(new Card("Farid Salimov", 123123123, 10000));
            Cards.Add(new Card("Emin Resulov", 111111111, 5000));
            InitializeComponent();
        }

        private void InsertCardButton_Click(object sender, RoutedEventArgs e)
        {
            int enteredCardNumber;
            bool found = false;

            if (int.TryParse(CardNumberTextBox.Text, out enteredCardNumber))
            {
                foreach (var card in Cards)
                {
                    if (enteredCardNumber == card.CardNumber)
                    {
                        found = true;
                        loggedin = true;
                        currentlyCard = card;
                        CardNameTextBox.Text = card.FullName;
                        CardAmountTextBox.Text = card.Amount.ToString();
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Invalid card number.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                MessageBox.Show("Invalid card number format, please enter a numeric value.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (loggedin)
            {
                int withdrawAmount;

                if (int.TryParse(WithdrawAmountTextBox.Text, out withdrawAmount))
                {
                    if (withdrawAmount <= currentlyCard.Amount)
                    {
                        WithdrawAmountTextBox.IsEnabled = false;
                        TransferButton.IsEnabled = false;
                        InsertCardButton.IsEnabled = false;
                        LoadDataButton.IsEnabled = false;
                        CardNumberTextBox.IsEnabled = false;

                        int newAmount = currentlyCard.Amount - withdrawAmount;
                        CardAmountTextBox.Text = newAmount.ToString();

                        for (int i = 1; i <= 10; i++)
                        {
                            ComingAmountTextBox.Text = $"{i * withdrawAmount / 10:C2}";
                            await Task.Delay(500);
                        }

                        currentlyCard.Amount = newAmount;

                        WithdrawAmountTextBox.IsEnabled = true;
                        TransferButton.IsEnabled = true;
                        InsertCardButton.IsEnabled = true;
                        LoadDataButton.IsEnabled = true;
                        CardNumberTextBox.IsEnabled = true;
                        MessageBox.Show($"{ComingAmountTextBox.Text}$ has been successfully withdrawn.", "Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    else
                    {
                        MessageBox.Show("You do not have this amount of money on your card.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Invalid withdraw amount format, please enter a numeric value.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                MessageBox.Show("First, log in to your card account.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (loggedin)
            {
                CardNameTextBox.Text = currentlyCard.FullName;
                CardAmountTextBox.Text = currentlyCard.Amount.ToString();
            }

            else
            {
                MessageBox.Show("First, log in to your card account.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}