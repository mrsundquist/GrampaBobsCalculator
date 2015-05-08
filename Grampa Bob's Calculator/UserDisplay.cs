using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

namespace Grampa_Bob_s_Calculator
{
    class UserDisplay
    {
        public StackPanel display = null;
        public StackPanel memoBar = null;
        public User theUser = null;

        public UserDisplay(Windows.UI.Xaml.Controls.StackPanel p, User theUser)
        {
            this.theUser = theUser;

            //set colors
            SolidColorBrush color1 = new SolidColorBrush(Color.FromArgb(179, 100, 130, 200));
            SolidColorBrush color2 = new SolidColorBrush(Color.FromArgb(255, 45, 63, 104));

            //parent -> display
            this.display = new StackPanel();
            p.Children.Add(this.display);
            this.display.Background = color1;
            this.display.Height = 200;
            this.display.Width = p.Width; // set width to parent's width
            this.display.Orientation = Orientation.Horizontal;

            //parent -> display -> borderName
            DisplayNameBox borderName = new DisplayNameBox(display, color2);
            TextBlock rowName = (TextBlock)borderName.rowName;
            rowName.Text = "Personal Data";

            //parent -> display -> scroller
            ScrollViewer scroller = new ScrollViewer();
            this.display.Children.Add(scroller);
            scroller.VerticalScrollMode = ScrollMode.Disabled;
            scroller.ZoomMode = ZoomMode.Disabled;
            scroller.Width = this.display.Width - 260;
            scroller.Height = this.display.Height;
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

            //parent -> display -> scroller -> dataStack
            StackPanel dataStack = new StackPanel();
            scroller.Content = dataStack;
            dataStack.Height = 200;
            dataStack.Orientation = Orientation.Horizontal;
            dataStack.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

            //parent -> display -> scroller -> dataStack -> milesStack
            DisplaySliderStack milesStack = new DisplaySliderStack(dataStack,
                "How many miles do you drive a year?", "What percent are city miles (vs highway)?",
                User.MinMiles, User.MaxMiles,
                ((int)(User.MinMiles / 1000)).ToString() + "k", ((int)(User.MaxMiles / 1000)).ToString() + "k",
                (int)(User.MinPercentCity * 100), (int)(User.MaxPercentCity * 100),
                ((int)(User.MinPercentCity * 100)).ToString() + "%", ((int)(User.MaxPercentCity * 100)).ToString() + "%",
                color1, color2);
            UIElementCollection milesStackChildren = ((StackPanel)(milesStack.stackP).Children[1]).Children;
            ((Slider)milesStackChildren[1]).ValueChanged += updateMilesPerYear;
            milesStackChildren = ((StackPanel)(milesStack.stackP).Children[3]).Children;
            ((Slider)milesStackChildren[1]).ValueChanged += updatePercentCityMiles;

            //parent -> display -> scroller -> dataStack -> gasStack
            DisplaySoloSliderStack gasStack = new DisplaySoloSliderStack(dataStack,
                "How much does a gallon of gas cost?",
                (int)User.MinPriceFuel, (int)User.MaxPriceFuel,
                "$" + User.MinPriceFuel.ToString("F"), "$" + User.MaxPriceFuel.ToString("F"),
                color1, color2);
            UIElementCollection gasStackChildren = ((StackPanel)(gasStack.stackP).Children[1]).Children;
            ((Slider)gasStackChildren[1]).ValueChanged += updateGasPrice;

            //parent -> display -> scroller -> dataStack -> interestTaxStack
            DisplaySliderStack interestTaxStack = new DisplaySliderStack(dataStack,
                "What is your interest rate?", "What is your sales tax rate?",
                (int)User.MinInterest, (int)User.MaxInterest,
                ((int)(User.MinInterest)).ToString() + "%",
                ((int)(User.MaxInterest)).ToString() + "%",
                (int)User.MinTax, (int)User.MaxTax,
                ((int)(User.MinTax)).ToString() + "%",
                ((int)(User.MaxTax)).ToString() + "%",
                color1, color2);
            UIElementCollection interestTaxStackChildren = ((StackPanel)(interestTaxStack.stackP).Children[1]).Children;
            ((Slider)interestTaxStackChildren[1]).ValueChanged += updateInterest;
            interestTaxStackChildren = ((StackPanel)(interestTaxStack.stackP).Children[3]).Children;
            ((Slider)interestTaxStackChildren[1]).ValueChanged += updateTax;

            //parent -> display -> scroller -> dataStack -> deleteStack
            DisplayDeleteButton displayDeleteButton = new DisplayDeleteButton(dataStack, "Reset User Data", color2);
            Button deleteButton = displayDeleteButton.deleteB;
            deleteButton.Tapped += clearData;

            //parent -> memoBar
            this.memoBar = new StackPanel();
            p.Children.Add(this.memoBar);
            this.memoBar.Width = p.Width; // set width to parent's width
            this.memoBar.Height = 64;
            this.memoBar.Orientation = Orientation.Horizontal;

            //parent -> memoBar -> memoScroll -> memoStack -> elements
            DisplayMemoScroll memoScroll = new DisplayMemoScroll(this.memoBar);
            StackPanel memoStack = (StackPanel)memoScroll.memoScroll.Content;
            DisplayMemoTextBlock milesLabel = new DisplayMemoTextBlock(memoStack, "Annual Miles:");
            DisplayMemoTextBox miles = new DisplayMemoTextBox(memoStack, (0.ToString()), color1);
            DisplayMemoTextBlock percentCityLabel = new DisplayMemoTextBlock(memoStack, "Percent City Miles:");
            DisplayMemoTextBox percentCity = new DisplayMemoTextBox(memoStack, (0.ToString()) + "%", color1);
            DisplayMemoTextBlock fuelCostLabel = new DisplayMemoTextBlock(memoStack, "Fuel Cost:");
            DisplayMemoTextBox fuelCost = new DisplayMemoTextBox(memoStack, "$" + (0.ToString("F")), color1);
            DisplayMemoTextBlock interestLabel = new DisplayMemoTextBlock(memoStack, "Interest Rate:");
            DisplayMemoTextBox interest = new DisplayMemoTextBox(memoStack, (0.ToString("F")) + "%", color1);
            DisplayMemoTextBlock taxLabel = new DisplayMemoTextBlock(memoStack, "Tax Rate:");
            DisplayMemoTextBox tax = new DisplayMemoTextBox(memoStack, (0.ToString("F")) + "%", color1);
        }

        void updateMilesPerYear(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.MilesPerYear = (int)((Slider)sender).Value;
            updateMemoBar();
        }

        void updatePercentCityMiles(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.PercentCityMiles = ((Slider)sender).Value / 100;
            updateMemoBar();
        }

        void updateGasPrice(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.PriceOfFuel = ((Slider)sender).Value;
            updateMemoBar();
        }

        void updateInterest(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.InterestRate = ((Slider)sender).Value;
            updateMemoBar();
        }

        void updateTax(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.SalesTaxRate = ((Slider)sender).Value;
            updateMemoBar();
        }

        private void updateMemoBar()
        {
            ScrollViewer memoScroll = (ScrollViewer)this.memoBar.Children[0];
            StackPanel memoStack = (StackPanel)memoScroll.Content;

            TextBox milesText = (TextBox)memoStack.Children[1];
            milesText.Text = String.Format("{0:#,0}", this.theUser.MilesPerYear);

            TextBox percentCityText = (TextBox)memoStack.Children[3];
            percentCityText.Text = (this.theUser.PercentCityMiles * 100).ToString() + "%";

            TextBox fuelCostText = (TextBox)memoStack.Children[5];
            fuelCostText.Text = "$" + this.theUser.PriceOfFuel.ToString("F");

            TextBox interestText = (TextBox)memoStack.Children[7];
            interestText.Text = (this.theUser.InterestRate * 100).ToString("F") + "%";

            TextBox taxText = (TextBox)memoStack.Children[9];
            taxText.Text = (this.theUser.SalesTaxRate * 100).ToString("F") + "%";

            foreach (VehicleDisplay thisDisplay in VehicleWrapper.VehicleDisplays)
            {
                thisDisplay.updateMemoBar();
            }
        }

        private void clearData(object sender, TappedRoutedEventArgs e)
        {
            clearData();
        }

        public void clearData()
        {
            this.theUser.MilesPerYear = User.MinMiles;
            this.theUser.PercentCityMiles = User.MinPercentCity;
            this.theUser.PriceOfFuel = User.MinPriceFuel;
            this.theUser.InterestRate = User.MinInterest;
            this.theUser.SalesTaxRate = User.MinTax;

            StackPanel dataStack = (StackPanel)((ScrollViewer)display.Children[1]).Content;
            StackPanel milesStack = (StackPanel)dataStack.Children[0];
            StackPanel gasStack = (StackPanel)dataStack.Children[1];
            StackPanel interestTaxStack = (StackPanel)dataStack.Children[2];
            ((Slider)((StackPanel)milesStack.Children[1]).Children[1]).Value = this.theUser.MilesPerYear;
            ((Slider)((StackPanel)milesStack.Children[3]).Children[1]).Value = this.theUser.PercentCityMiles;
            ((Slider)((StackPanel)gasStack.Children[1]).Children[1]).Value = this.theUser.PriceOfFuel;
            ((Slider)((StackPanel)interestTaxStack.Children[1]).Children[1]).Value = this.theUser.InterestRate;
            ((Slider)((StackPanel)interestTaxStack.Children[3]).Children[1]).Value = this.theUser.SalesTaxRate;

            this.updateMemoBar();
        }
    }
}