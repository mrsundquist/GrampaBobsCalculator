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
        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }
        
        List<Vehicle> vehicles = new List<Vehicle>();

#region Navigation Helper and Dictionary
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
            // Restore values stored in session state.
            if (e.PageState != null)
            {
                if (e.PageState.ContainsKey("numMiles")) numMiles.Value = Convert.ToInt32(e.PageState["numMiles"].ToString());
                if (e.PageState.ContainsKey("pctCityMiles")) pctCityMiles.Value = Convert.ToInt32(e.PageState["pctCityMiles"].ToString());
                if (e.PageState.ContainsKey("gasCost")) gasCost.Value = Convert.ToDouble(e.PageState["gasCost"].ToString());
                if (e.PageState.ContainsKey("interestRate")) interestRate.Value = Convert.ToDouble(e.PageState["interestRate"].ToString());
                if (e.PageState.ContainsKey("taxRate")) taxRate.Value = Convert.ToDouble(e.PageState["taxRate"].ToString());
                if (e.PageState.ContainsKey("annualMiles")) AnnualMilesDisplay.Text = e.PageState["annualMiles"].ToString();
                if (e.PageState.ContainsKey("cityPctg")) CityPercentageDisplay.Text = e.PageState["cityPctg"].ToString();
                if (e.PageState.ContainsKey("gasCostDisplay")) GasCostDisplay.Text = e.PageState["gasCostDisplay"].ToString();
                if (e.PageState.ContainsKey("interestRateDisplay")) InterestRateDisplay.Text = e.PageState["interestRateDisplay"].ToString();
                if (e.PageState.ContainsKey("taxRateDisplay")) TaxRateDisplay.Text = e.PageState["taxRateDisplay"].ToString();
            }

            // Restore values stored in app data.
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("numMiles")) numMiles.Value = Convert.ToInt32(localSettings.Values["numMiles"].ToString());
            if (localSettings.Values.ContainsKey("pctCityMiles")) pctCityMiles.Value = Convert.ToInt32(localSettings.Values["pctCityMiles"].ToString());
            if (localSettings.Values.ContainsKey("gasCost")) gasCost.Value = Convert.ToDouble(localSettings.Values["gasCost"].ToString());
            if (localSettings.Values.ContainsKey("interestRate")) interestRate.Value = Convert.ToDouble(localSettings.Values["interestRate"].ToString());
            if (localSettings.Values.ContainsKey("taxRate")) taxRate.Value = Convert.ToDouble(localSettings.Values["taxRate"].ToString());
            if (localSettings.Values.ContainsKey("annualMiles")) AnnualMilesDisplay.Text = localSettings.Values["annualMiles"].ToString();
            if (localSettings.Values.ContainsKey("cityPctg")) CityPercentageDisplay.Text = localSettings.Values["cityPctg"].ToString();
            if (localSettings.Values.ContainsKey("gasCostDisplay")) GasCostDisplay.Text = localSettings.Values["gasCostDisplay"].ToString();
            if (localSettings.Values.ContainsKey("interestRateDisplay")) InterestRateDisplay.Text = localSettings.Values["interestRateDisplay"].ToString();
            if (localSettings.Values.ContainsKey("taxRateDisplay")) TaxRateDisplay.Text = localSettings.Values["TaxRateDisplay"].ToString();
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
            // this is saved only for this session
            // put page specifics here - e.g. numMiles on slider AND in display
            e.PageState["numMiles"] = numMiles.Value;
            e.PageState["pctCityMiles"] = pctCityMiles.Value;
            e.PageState["gasCost"] = gasCost.Value;
            e.PageState["interestRate"] = interestRate.Value;
            e.PageState["taxRate"] = taxRate.Value;
            e.PageState["annualMiles"] = AnnualMilesDisplay.Text;
            e.PageState["cityPctg"] = CityPercentageDisplay.Text;
            e.PageState["gasCostDisplay"] = GasCostDisplay.Text;
            e.PageState["interestRateDisplay"] = InterestRateDisplay.Text;
            e.PageState["taxRateDisplay"] = TaxRateDisplay.Text;
        }

        private void saveAppData(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings =
              Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["numMiles"] = numMiles.Value;
            localSettings.Values["pctCityMiles"] = pctCityMiles.Value;
            localSettings.Values["gasCost"] = gasCost.Value;
            localSettings.Values["interestRate"] = interestRate.Value;
            localSettings.Values["taxRate"] = taxRate.Value;
            localSettings.Values["annualMiles"] = AnnualMilesDisplay.Text;
            localSettings.Values["cityPctg"] = CityPercentageDisplay.Text;
            localSettings.Values["gasCostDisplay"] = GasCostDisplay.Text;
            localSettings.Values["interestRateDisplay"] = InterestRateDisplay.Text;
            localSettings.Values["taxRateDisplay"] = TaxRateDisplay.Text;
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
#endregion

        private void aboutPageClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AboutPage));
            }
        }

        private void updatePersonalDataDisplay(object sender, RangeBaseValueChangedEventArgs e)
        {
            AnnualMilesDisplay.Text = string.Format("{0:n0}", numMiles.Value).ToString();
            CityPercentageDisplay.Text = (pctCityMiles.Value).ToString() + "%";
            GasCostDisplay.Text = "$" + Math.Round(gasCost.Value, 2).ToString("F");
            InterestRateDisplay.Text = Math.Round(interestRate.Value, 2).ToString("F") + "%";
            TaxRateDisplay.Text = Math.Round(taxRate.Value, 2).ToString("F") + "%";
            updateVehicleDataDisplay(sender, e);
            updateCentsPerMileDisplay();
        }
        
        private void updateVehicleTextData(object sender, TextChangedEventArgs e)
        {
            VehicleDisplay.Text = VehicleYear.Text + " " + VehicleMake.Text + " " + VehicleModel.Text;
            if (VehicleSource.Text != "") VehicleDisplay.Text += " (" + VehicleSource.Text + ")";
        }

        private void updateVehicleDataDisplay(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                double milesPerYear = numMiles.Value; // personal info
                double tax = taxRate.Value / 100; // personal info

                double initialCost = (vehiclePrice.Value + vehicleRepairCost.Value) * (1 + tax);
                double initialMileage = vehicleInitialMileage.Value;
                double finalMileage = (vehicleFinalMileage.Value < initialMileage) ? (initialMileage) : (vehicleFinalMileage.Value);
                double totalMileage = finalMileage - initialMileage;
                double totalYears = (totalMileage > 0) ? (totalMileage / milesPerYear) : (0);

                InitialCostDisplay.Text = "$" + string.Format("{0:n0}", initialCost).ToString() + ".00";
                MinimumFinalMileage.Text = (initialMileage / 1000).ToString() + "k";
                vehicleFinalMileage.Minimum = initialMileage;
                LifeSpanDisplay.Text = totalYears.ToString("F") + " years";

                updateCentsPerMileDisplay();
            }
            catch (Exception ex) { }
        }

        private void updateCentsPerMileDisplay()
        {
            double centsPerMile = calculateCentsPerMile();
            centsPerMileDisplay.Text = centsPerMile.ToString("F");
            centsPerMileDisplay.Text += "¢ per mile";
        }
        // old calculateCentsPerMile
        private double calculateCentsPerMile()
        {
            double milesPerYear = numMiles.Value;
            double percentCityMiles = pctCityMiles.Value / 100;
            double fuelCost = gasCost.Value;
            double interest = interestRate.Value / 100;
            double tax = taxRate.Value / 100;

            double purchaseCost = vehiclePrice.Value;
            double repairCost = vehicleRepairCost.Value;
            double initialCost = (purchaseCost + repairCost) * (1 + tax);
            double initialCostNoTax = (purchaseCost + repairCost);
            double initialMileage = vehicleInitialMileage.Value;
            double finalMileage = (vehicleFinalMileage.Value < initialMileage) ? (initialMileage) : (vehicleFinalMileage.Value);
            double totalMileage = finalMileage - initialMileage;
            double totalYears = (totalMileage > 0) ? (totalMileage / milesPerYear) : (0);
            double cityMilesPerGallon = cityMPG.Value;
            double highwayMilesPerGallon = highwayMPG.Value;

            double price = initialCost * Math.Exp(interest * totalYears);
            double priceRate = price / totalMileage;

            double fuelRate = fuelCost / ((cityMilesPerGallon * percentCityMiles)
                + (highwayMilesPerGallon * (1 - percentCityMiles)));

            double maintenanceRate = (initialMileage + finalMileage) / 4444000;

            double depreciatedMileageValue = 0.25 *
                ((Math.PI / 2) - Math.Atan(((2.5 * finalMileage) / 100000) - 2.1)) - .36;
            double depreciatedTimeValue = 0.9332 * Math.Exp(-0.177 * totalYears);
            double depreciatedRate = depreciatedMileageValue + depreciatedTimeValue;
            if (depreciatedRate > 1) depreciatedRate = 1;
            if (depreciatedRate < 0) depreciatedRate = 0;
            double resellValueRate = (initialCostNoTax * (depreciatedRate)) / totalMileage;

            double centsPerMile = (priceRate + fuelRate + maintenanceRate - resellValueRate) * 100;
            if (Double.IsNaN(centsPerMile) || Double.IsInfinity(centsPerMile))
                centsPerMile = 0;

            return centsPerMile;
        }

        private void AddVehicle_Click(object sender, TappedRoutedEventArgs e)
        {
                vehicles.Add(new Vehicle(ContentStackPanel));
        }
    }
}
