using GalaSoft.MvvmLight;
using KioskClient.DTO;
using System;
using System.Xml;

namespace KioskClient.ViewModel
{
    public partial class MenuProductViewModel : ViewModelBase
    {
        public MenuProductDTO Product { get; }
        public bool IsInCart => Quantity > 0;
        public event EventHandler? QuantityChanged;

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if(Set(ref quantity, value))
                {
                    RaisePropertyChanged(nameof(IsInCart));
                    QuantityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public MenuProductViewModel(MenuProductDTO product)
        {
            Product = product;
        }

        public void IncrementQuantity()
        {
            Quantity++;
        }

        public void DecrementQuantity()
        {
            if (Quantity > 0)Quantity--;
        }
    }
}
