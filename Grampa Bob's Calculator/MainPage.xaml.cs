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

        private void updateAnnualMiles(object sender, RangeBaseValueChangedEventArgs e)
        {
            AnnualMilesDisplay.Text = string.Format("{0:n0}", numMiles.Value).ToString();
            updateMileage(sender, e);
        }

        private void updateCityPercentage(object sender, RangeBaseValueChangedEventArgs e)
        {
            CityPercentageDisplay.Text = pctCityMiles.Value.ToString() + "%";
            updateCentsPerMile();
        }

        private void updateGasPrice(object sender, RangeBaseValueChangedEventArgs e)
        {
            GasCostDisplay.Text = "$" + Math.Round(gasCost.Value, 2).ToString("F");
            updateCentsPerMile();
        }

        private void updateInterestRate(object sender, RangeBaseValueChangedEventArgs e)
        {
            InterestRateDisplay.Text = Math.Round(interestRate.Value, 2).ToString("F") + "%";
            updateCentsPerMile();
        }

        private void updateTaxRate(object sender, RangeBaseValueChangedEventArgs e)
        {
            TaxRateDisplay.Text = Math.Round(taxRate.Value, 2).ToString("F") + "%";
            updateInitialCost(sender, e);
            updateCentsPerMile();
        }

        private void updateVehicleDisplay(object sender, TextChangedEventArgs e)
        {
            VehicleDisplay.Text = VehicleYear.Text + " " + VehicleMake.Text + " "
                + VehicleModel.Text;
            if (VehicleSource.Text != "")
            {
                VehicleDisplay.Text += " (" + VehicleSource.Text + ")";
            }
        }

        private void updateInitialCost(object sender, RangeBaseValueChangedEventArgs e)
        {
            InitialCostDisplay.Text = "$" + string.Format("{0:n0}", calculateInitialCost()).ToString() + ".00";
            updateCentsPerMile();
        }

        private double calculateInitialCost()
        {
            return ((vehiclePrice.Value + vehicleRepairCost.Value) * (1 + (taxRate.Value * .01)));
        }

        private void updateMileage(object sender, RangeBaseValueChangedEventArgs e)
        {
            MinimumFinalMileage.Text = (vehicleInitialMileage.Value/1000).ToString() + "k";
            vehicleFinalMileage.Minimum = vehicleInitialMileage.Value;
            LifeSpanDisplay.Text = calculateLifeSpan().ToString("F") + " years";
            updateCentsPerMile();
        }

        private double calculateLifeSpan()
        {
            double milesRemaining = vehicleFinalMileage.Value - vehicleInitialMileage.Value;
            double milesPerYear = numMiles.Value;
            if (milesRemaining > 0)
            {
                return milesRemaining / milesPerYear;
            }
            else
            {
                return 0;
            }
        }

        private void updateCentsPerMile()
        {
            try
            {
                double interest = interestRate.Value / 100;
                double lifespan = calculateLifeSpan();
                double initialCost = calculateInitialCost();
                double milesPerYear = numMiles.Value;
                double fuelCost = gasCost.Value;
                double cityMilesPerGallon = cityMPG.Value;
                double highwayMilesPerGallon = highwayMPG.Value;
                double percentCityMiles = pctCityMiles.Value / 100;
                double initialMileage = vehicleInitialMileage.Value;
                double finalMileage = vehicleFinalMileage.Value;

                double price = initialCost *
                    Math.Exp(interest * ((finalMileage - initialMileage) / milesPerYear));
                double priceRate = price / (finalMileage-initialMileage);

                double fuelRate = fuelCost / ((cityMilesPerGallon * percentCityMiles)
                    + (highwayMilesPerGallon * (1 - percentCityMiles)));

                double maintenanceRate = (2775000 + initialMileage + finalMileage) / 50000000;

                double centsPerMile = (priceRate + fuelRate + maintenanceRate) * 100;

                
                centsPerMileDisplay.Text = centsPerMile.ToString("F");
                centsPerMileDisplay.Text += "¢ per mile";
            }
            catch (Exception ex)
            {
            }
        }

        private void updateCentsPerMile(object sender, RangeBaseValueChangedEventArgs e)
        {
            updateCentsPerMile();
        }
    }
}
