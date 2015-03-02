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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Grampa_Bob_s_Calculator
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void aboutPageClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AboutPage));
            }
        }

        private void updateDisplay(object sender, RangeBaseValueChangedEventArgs e) { updateDisplay(); }
        private void updateDisplay(object sender, TextChangedEventArgs e) { updateDisplay(); }
        private void updateDisplay()
        {
            try
            {
                double interest = interestRate.Value / 100;
                double tax = taxRate.Value / 100;
                double purchaseCost = vehiclePrice.Value;
                double repairCost = vehicleRepairCost.Value;
                double initialCost = (purchaseCost + repairCost) * (1 + tax);
                double milesPerYear = numMiles.Value;
                double fuelCost = gasCost.Value;
                double cityMilesPerGallon = cityMPG.Value;
                double highwayMilesPerGallon = highwayMPG.Value;
                double percentCityMiles = pctCityMiles.Value / 100;
                double initialMileage = vehicleInitialMileage.Value;
                double finalMileage = (vehicleFinalMileage.Value < initialMileage) ? (initialMileage) : (vehicleFinalMileage.Value);
                double totalMileage = finalMileage - initialMileage;
                double totalYears = (totalMileage > 0) ? (totalMileage / milesPerYear) : (0);

                //update annual miles
                AnnualMilesDisplay.Text = string.Format("{0:n0}", milesPerYear).ToString();

                //update city percentage
                CityPercentageDisplay.Text = (percentCityMiles * 100).ToString() + "%";

                //update gas price
                GasCostDisplay.Text = "$" + Math.Round(fuelCost, 2).ToString("F");

                //update interest rate
                InterestRateDisplay.Text = Math.Round(interest, 2).ToString("F") + "%";

                //update tax rate
                TaxRateDisplay.Text = Math.Round((tax * 100), 2).ToString("F") + "%";

                //update vehicle info display
                VehicleDisplay.Text = VehicleYear.Text + " " + VehicleMake.Text + " " + VehicleModel.Text;
                if (VehicleSource.Text != "") VehicleDisplay.Text += " (" + VehicleSource.Text + ")";

                //update initial cost
                InitialCostDisplay.Text = "$" + string.Format("{0:n0}", initialCost).ToString() + ".00";

                //update mileage
                MinimumFinalMileage.Text = (initialMileage / 1000).ToString() + "k";
                vehicleFinalMileage.Minimum = initialMileage;
                LifeSpanDisplay.Text = totalYears.ToString("F") + " years";

                //calculate cents per mile
                double price = initialCost * Math.Exp(interest * totalYears);
                double priceRate = price / totalMileage;

                double fuelRate = fuelCost / ((cityMilesPerGallon * percentCityMiles)
                    + (highwayMilesPerGallon * (1 - percentCityMiles)));

                double maintenanceRate = (initialMileage + finalMileage) / 4444000;

                double centsPerMile = (priceRate + fuelRate + maintenanceRate) * 100;
                if (Double.IsNaN(centsPerMile) || Double.IsInfinity(centsPerMile))
                    centsPerMile = 0;

                centsPerMileDisplay.Text = centsPerMile.ToString("F");
                centsPerMileDisplay.Text += "¢ per mile";
            } catch (Exception ex) { }
        }
    }
}
