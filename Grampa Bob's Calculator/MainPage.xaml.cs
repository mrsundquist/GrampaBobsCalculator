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

        User user;
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
                /*
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
                 * */
            }

            // Restore values stored in app data.
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            /*
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
             */
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

            /*
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
             */
        }

        private void saveAppData(object sender, RoutedEventArgs e)
        {

            /*
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
             * 
             */
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

        private void AddVehicle_Click(object sender, TappedRoutedEventArgs e)
        {
            vehicles.Add(new Vehicle(VehicleStack, vehicles.Count(), user));
            if (vehicles.Count() == 10) NewVehicle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void AddUser_Click(object sender, TappedRoutedEventArgs e)
        {
            pageTitle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            NewUser.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            user = new User(UserStack);
            NewVehicle.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
