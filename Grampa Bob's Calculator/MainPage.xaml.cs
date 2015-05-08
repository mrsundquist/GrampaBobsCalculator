using Grampa_Bob_s_Calculator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grampa_Bob_s_Calculator
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
              
        User theUser;

        private void aboutPageClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null) { this.Frame.Navigate(typeof(AboutPage)); }
        }

        private void AddVehicle_Click(object sender, TappedRoutedEventArgs e)
        {
            new VehicleWrapper(VehicleUIStack, theUser, (Button)sender);
        }

        private void AddUser_Click(object sender, TappedRoutedEventArgs e)
        {
            pageTitle.Visibility = Visibility.Collapsed;
            NewUser.Visibility = Visibility.Collapsed;
            NewVehicle.Visibility = Visibility.Visible;
            theUser = new User(UserUIStack);
        }

        private void clearAllData(object sender, TappedRoutedEventArgs e)
        {
            VehicleWrapper.DeleteAllVehicles();
            theUser.ClearDisplay();
        }
    }
}
