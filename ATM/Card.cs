using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Card
    {
        private string fullName;
        private int cardNumber;
        private int amount;

        public string FullName { get => fullName; set { fullName = value; OnPropertyChanged(); } }
        public int CardNumber { get => cardNumber; set { cardNumber = value; OnPropertyChanged(); } }
        public int Amount { get => amount; set { amount = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Card() { }

        public Card(string fullName, int cardNumber, int amount)
        {
            FullName = fullName;
            CardNumber = cardNumber;
            Amount = amount;
        }
    }
}
