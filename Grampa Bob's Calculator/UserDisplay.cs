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
            //UIElementCollection borderAndScroller = this.display.Children;
            //Border nameBox = (Border)borderAndScroller[0]; // the border
            TextBlock rowName = (TextBlock)borderName.rowName;
            rowName.Text = "Personal Data";


            //parent -> display -> scroller
            ScrollViewer scroller = new ScrollViewer();
            this.display.Children.Add(scroller);
            scroller.VerticalScrollMode = ScrollMode.Disabled;
            scroller.ZoomMode = ZoomMode.Disabled;
            scroller.Width = this.display.Width - 260;
            scroller.Height = 200;
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
                User.getMinMiles(), User.getMaxMiles(),
                ((int)(User.getMinMiles() / 1000)).ToString() + "k", ((int)(User.getMaxMiles() / 1000)).ToString() + "k",
                (int)(User.getMinPercentCity() * 100), (int)(User.getMaxPercentCity() * 100),
                ((int)(User.getMinPercentCity() * 100)).ToString() + "%", ((int)(User.getMaxPercentCity() * 100)).ToString() + "%",
                color1, color2);
            UIElementCollection milesStackChildren = ((StackPanel)(milesStack.stackP).Children[1]).Children;
            ((Slider)milesStackChildren[1]).ValueChanged += updateMilesPerYear;
            milesStackChildren = ((StackPanel)(milesStack.stackP).Children[3]).Children;
            ((Slider)milesStackChildren[1]).ValueChanged += updatePercentCityMiles;

            //parent -> display -> scroller -> dataStack -> gasStack
            DisplaySoloSliderStack gasStack = new DisplaySoloSliderStack(dataStack,
                "How much does a gallon of gas cost?",
                (int)User.getMinPriceFuel(), (int)User.getMaxPriceFuel(),
                "$" + User.getMinPriceFuel().ToString("F"), "$" + User.getMaxPriceFuel().ToString("F"),
                color1, color2);
            UIElementCollection gasStackChildren = ((StackPanel)(gasStack.stackP).Children[1]).Children;
            ((Slider)gasStackChildren[1]).ValueChanged += updateGasPrice;
            
            //parent -> display -> scroller -> dataStack -> interestTaxStack
            DisplaySliderStack interestTaxStack = new DisplaySliderStack(dataStack,
                "What is your interest rate?", "What is your sales tax rate?",
                (int)User.getMinInterest(), (int)User.getMaxInterest(),
                ((int)(User.getMinInterest())).ToString() + "%",
                ((int)(User.getMaxInterest())).ToString() + "%",
                (int)User.getMinTax(), (int)User.getMaxTax(),
                ((int)(User.getMinTax())).ToString() + "%",
                ((int)(User.getMaxTax())).ToString() + "%",
                color1, color2);
            UIElementCollection interestTaxStackChildren = ((StackPanel)(interestTaxStack.stackP).Children[1]).Children;
            ((Slider)interestTaxStackChildren[1]).ValueChanged += updateInterest;
            interestTaxStackChildren = ((StackPanel)(interestTaxStack.stackP).Children[3]).Children;
            ((Slider)interestTaxStackChildren[1]).ValueChanged += updateTax;

            /*
            //parent -> display -> scroller -> dataStack -> notesStack
            DisplayNotesStack notesStack = new DisplayNotesStack(dataStack, "Notes:", "Insert any notes about your vehicle here.");
            UIElementCollection notesStackChildren = notesStack.stackP.Children;
            ((TextBox)notesStackChildren[1]).TextChanged += updateNotes;
             * 

            //parent -> memoBar
            this.memoBar = new StackPanel();
            p.Children.Add(this.memoBar);
            this.memoBar.Width = p.Width; // set width to parent's width
            this.memoBar.Height = 64;
            this.memoBar.Orientation = Orientation.Horizontal;

            //parent -> memoBar -> memoScroll -> memoStack -> elements
            DisplayMemoScroll memoScroll = new DisplayMemoScroll(this.memoBar);
            StackPanel memoStack = (StackPanel)memoScroll.memoScroll.Content;
            //DisplayMemoTextBlock cpmLabel = new DisplayMemoTextBlock(memoStack, "CPM:");
            DisplayMemoTextBox cpm = new DisplayMemoTextBox(memoStack, (0.ToString("F") + "¢ per mile"), color1);
            cpm.memoText.FontSize += 13; // make cents per mile display bigger
            cpm.memoText.FontWeight = Windows.UI.Text.FontWeights.Thin;
            cpm.memoText.CharacterSpacing = 75;
            cpm.memoText.Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 0);
            cpm.memoText.Width = 225;
            DisplayMemoTextBlock totalCostLabel = new DisplayMemoTextBlock(memoStack, "Total Cost:");
            DisplayMemoTextBox totalCost = new DisplayMemoTextBox(memoStack, "$" + (0.ToString()), color1);
            DisplayMemoTextBlock lifeSpanLabel = new DisplayMemoTextBlock(memoStack, "Life Span:");
            DisplayMemoTextBox lifeSpan = new DisplayMemoTextBox(memoStack, (0.ToString("F") + " years"), color1);
            DisplayMemoTextBlock avgMPGLabel = new DisplayMemoTextBlock(memoStack, "Average MPG:");
            DisplayMemoTextBox aveMPG = new DisplayMemoTextBox(memoStack, (1.ToString("F")), color1);
            DisplayMemoTextBlock maintenanceLabel = new DisplayMemoTextBlock(memoStack, "Maintenance:");
            DisplayMemoTextBox maintenance = new DisplayMemoTextBox(memoStack, "$" + (0.ToString()), color1);
            DisplayMemoTextBlock resellLabel = new DisplayMemoTextBlock(memoStack, "Resell:");
            DisplayMemoTextBox resell = new DisplayMemoTextBox(memoStack, "$" + (0.ToString()), color1);
        
        

             * 
             * 
             * /*
             * */

        }

        void updateMilesPerYear(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.updateMilesPerYear((int)((Slider)sender).Value);
            updateMemoBar();
        }

        void updatePercentCityMiles(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.updatePercentCityMiles(((Slider)sender).Value / 100);
            updateMemoBar();
        }

        void updateGasPrice(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.updatePriceOfFuel(((Slider)sender).Value);
            updateMemoBar();
        }

        void updateInterest(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.updateInterestRate(((Slider)sender).Value);
            updateMemoBar();
        }

        void updateTax(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theUser.updateSalesTaxRate(((Slider)sender).Value);
            updateMemoBar();
        }

        private void updateMemoBar()
        {
            
            /*
            ScrollViewer memoScroll = (ScrollViewer)this.memoBar.Children[0];
            StackPanel memoStack = (StackPanel)memoScroll.Content;
            
            TextBox cpmText = (TextBox)memoStack.Children[0];
            
            User tempUser = new User();
            cpmText.Text = (Calculator.centsPerMile(tempUser, this.theVehicle)).ToString("F") + "¢ per mile";

            TextBox totalCostText = (TextBox)memoStack.Children[2];
            totalCostText.Text = dollarFormat(Calculator.totalCost(tempUser, this.theVehicle));

            TextBox lifeSpanText = (TextBox)memoStack.Children[4];
            lifeSpanText.Text = (Calculator.totalYears(tempUser, this.theVehicle)).ToString("F") + " years";

            TextBox avgMPGText = (TextBox)memoStack.Children[6];
            avgMPGText.Text = (Calculator.averageMPG(tempUser, this.theVehicle)).ToString("F");

            TextBox maintenanceText = (TextBox)memoStack.Children[8];
            maintenanceText.Text = dollarFormat(Calculator.lifetimeMaintenance(this.theVehicle));

            TextBox resellText = (TextBox)memoStack.Children[10];
            resellText.Text = dollarFormat(Calculator.resellValue(tempUser, this.theVehicle));
            */



            foreach (VehicleDisplay thisDisplay in Vehicle.vehicleDisplays)
            {
                thisDisplay.updateMemoBar();
            }
        }
        


    }
}

