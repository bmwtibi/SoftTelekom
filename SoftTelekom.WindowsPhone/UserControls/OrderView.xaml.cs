using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SoftTelekom.WindowsPhone.UserControls
{
    public partial class OrderView : MvxPhonePage
    {
        public OrderView()
        {
            InitializeComponent();
        }

        private void CityListPicker_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MbListPicker_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}