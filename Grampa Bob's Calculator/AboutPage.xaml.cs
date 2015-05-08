using Grampa_Bob_s_Calculator.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Grampa_Bob_s_Calculator
{
    public sealed partial class AboutPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel { get { return this.defaultViewModel; } }

        public NavigationHelper NavigationHelper { get { return this.navigationHelper; } }

        public AboutPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e) { }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e) { }

        protected override void OnNavigatedTo(NavigationEventArgs e) { navigationHelper.OnNavigatedTo(e); }

        protected override void OnNavigatedFrom(NavigationEventArgs e) { navigationHelper.OnNavigatedFrom(e); }
    }
}
