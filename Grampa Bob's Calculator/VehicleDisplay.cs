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
    class VehicleDisplay
    {
        public StackPanel display = null;
        public StackPanel memoBar = null;
        public Vehicle theVehicle = null;


        public VehicleDisplay(Windows.UI.Xaml.Controls.StackPanel p, Vehicle theVehicle, int numVehicles)
        {
            this.theVehicle = theVehicle;

            //set colors
            SolidColorBrush color1;
            SolidColorBrush color2;
            if (numVehicles % 3 == 0) // red
            {
                color1 = new SolidColorBrush(Color.FromArgb(179, 212, 107, 61));
                color2 = new SolidColorBrush(Color.FromArgb(255, 118, 42, 9));
            }
            else if (numVehicles % 3 == 1) // green
            {
                color1 = new SolidColorBrush(Color.FromArgb(179, 31, 166, 71));
                color2 = new SolidColorBrush(Color.FromArgb(255, 21, 83, 39));
            }
            else // blue
            {
                color1 = new SolidColorBrush(Color.FromArgb(179, 100, 130, 200));
                color2 = new SolidColorBrush(Color.FromArgb(255, 45, 63, 104));
            }

            //parent -> display
            this.display = new StackPanel();
            p.Children.Add(this.display);
            this.display.Background = color1;
            this.display.Height = 200;
            this.display.Width = p.Width; // set width to parent's width
            this.display.Orientation = Orientation.Horizontal;

            //parent -> display -> borderName
            DisplayNameBox borderName = new DisplayNameBox(display, color2);
            updateName("New Vehicle");

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

            //parent -> display -> scroller -> dataStack -> descStack1
            DisplayTextBlockStack descStack1 = new DisplayTextBlockStack(dataStack, "Year:", "Make:", "Model:", "Source:");

            //parent -> display -> scroller -> dataStack -> descStack2
            DisplayTextBoxStack descStack2 = new DisplayTextBoxStack(dataStack);
            UIElementCollection textBoxes = descStack2.stackP.Children;
            ((TextBox)textBoxes[0]).TextChanged += new TextChangedEventHandler(updateYear);
            ((TextBox)textBoxes[1]).TextChanged += new TextChangedEventHandler(updateMake);
            ((TextBox)textBoxes[2]).TextChanged += new TextChangedEventHandler(updateModel);
            ((TextBox)textBoxes[3]).TextChanged += new TextChangedEventHandler(updateSource);

            //parent -> display -> scroller -> dataStack -> priceStack
            DisplaySliderStack priceStack = new DisplaySliderStack(dataStack,
                "Price", "Cost to Repair", Vehicle.getMinPrice(), Vehicle.getMaxPrice(),
                "$" + Vehicle.getMinPrice().ToString(), "$" + Vehicle.getMaxPrice().ToString(),
                Vehicle.getMinRepairCost(), Vehicle.getMaxRepairCost(),
                "$" + Vehicle.getMinRepairCost().ToString(), "$" + Vehicle.getMaxRepairCost().ToString(),
                color1, color2);
            UIElementCollection priceStackChildren = ((StackPanel)(priceStack.stackP).Children[1]).Children;
            ((Slider)priceStackChildren[1]).ValueChanged += updatePrice; // price
            priceStackChildren = ((StackPanel)(priceStack.stackP).Children[3]).Children;
            ((Slider)priceStackChildren[1]).ValueChanged += updateRepairCost; // repair

            //parent -> display -> scroller -> dataStack -> mileageStack
            DisplaySliderStack mileageStack = new DisplaySliderStack(dataStack,
                "Initial Mileage", "Estimated Final Mileage",
                Vehicle.getMinInitialMileage(), Vehicle.getMaxInitialMileage(),
                ((int)(Vehicle.getMinInitialMileage() / 1000)).ToString() + "k",
                ((int)(Vehicle.getMaxInitialMileage() / 1000)).ToString() + "k",
                theVehicle.getMinFinalMileage(), Vehicle.getMaxFinalMileage(),
                ((int)(theVehicle.getMinFinalMileage() / 1000)).ToString() + "k",
                ((int)(Vehicle.getMaxFinalMileage() / 1000)).ToString() + "k",
                color1, color2);
            UIElementCollection mileageStackChildren = ((StackPanel)(mileageStack.stackP).Children[1]).Children;
            ((Slider)mileageStackChildren[1]).ValueChanged += updateInitialMileage; // initial mileage
            mileageStackChildren = ((StackPanel)(mileageStack.stackP).Children[3]).Children;
            ((Slider)mileageStackChildren[1]).ValueChanged += updateFinalMileage;

            //parent -> display -> scroller -> dataStack -> mpgStack
            DisplaySliderStack mpgStack = new DisplaySliderStack(dataStack,
                "City MPG", "Highway MPG",
                Vehicle.getMinCityMPG(), Vehicle.getMaxCityMPG(),
                ((int)(Vehicle.getMinCityMPG())).ToString(),
                ((int)(Vehicle.getMaxCityMPG())).ToString(),
                Vehicle.getMinHighwayMPG(), Vehicle.getMaxHighwayMPG(),
                ((int)(Vehicle.getMinHighwayMPG())).ToString(),
                ((int)(Vehicle.getMaxHighwayMPG())).ToString(),
                color1, color2);
            UIElementCollection mpgStackChildren = ((StackPanel)(mpgStack.stackP).Children[1]).Children;
            ((Slider)mpgStackChildren[1]).ValueChanged += updateCityMPG;
            mpgStackChildren = ((StackPanel)(mpgStack.stackP).Children[3]).Children;
            ((Slider)mpgStackChildren[1]).ValueChanged += updateHighwayMPG;

            //parent -> display -> scroller -> dataStack -> notesStack
            DisplayNotesStack notesStack = new DisplayNotesStack(dataStack, "Notes:", "Insert any notes about your vehicle here.");
            UIElementCollection notesStackChildren = notesStack.stackP.Children;
            ((TextBox)notesStackChildren[1]).TextChanged += updateNotes;

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
        }
        
        private void updateYear(object sender, TextChangedEventArgs e)
        {
            theVehicle.updateYear(((TextBox)sender).Text);
            updateName(theVehicle.getVehicleDescription());
        }

        private void updateMake(object sender, TextChangedEventArgs e)
        {
            theVehicle.updateMake(((TextBox)sender).Text);
            updateName(theVehicle.getVehicleDescription());
        }
        
        private void updateModel(object sender, TextChangedEventArgs e)
        {
            theVehicle.updateModel(((TextBox)sender).Text);
            updateName(theVehicle.getVehicleDescription());
        }

        private void updateSource(object sender, TextChangedEventArgs e)
        {
            theVehicle.updateSource(((TextBox)sender).Text);
            updateName(theVehicle.getVehicleDescription());
        }
        
        public void updateName(string t)
        {
            UIElementCollection borderAndScroller = this.display.Children;
            Border nameBox = (Border)borderAndScroller[0]; // the border
            TextBlock rowName = (TextBlock)nameBox.Child;

            List<int> partLengths = new List<int>
                { theVehicle.getYear().Length, theVehicle.getMake().Length,
                    theVehicle.getModel().Length, theVehicle.getSource().Length, 1 };
            int maxLength = partLengths.Max();

            double fontSize = 230 * (Math.Pow(maxLength,-0.8)); // adjust font size based on length of name
            if (fontSize > 56) fontSize = 56;
            if (fontSize < 30) fontSize = 30;

            rowName.FontSize = fontSize;
            
            rowName.Text = t;
        }

        void updatePrice(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updatePrice((int)((Slider)sender).Value);
            updateMemoBar();
        }

        void updateRepairCost(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateRepairCost((int)((Slider)sender).Value);
            updateMemoBar();
        }

        void updateInitialMileage(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateInitialMileage((int)e.NewValue);
            updateFinalMileageMin((int)e.NewValue);
            updateMemoBar();
        }

        void updateFinalMileage(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateFinalMileage((int)e.NewValue);
            //theVehicle.updateFinalMileage((int)((Slider)sender).Value);
            updateMemoBar();
        }

        public void updateFinalMileageMin(int m)
        {
            // change text
            UIElementCollection borderAndScroller = this.display.Children;
            ScrollViewer scroller = (ScrollViewer)borderAndScroller[1]; // the scroller
            StackPanel dataStack = (StackPanel)scroller.Content; // scroller stacks
            StackPanel mileageStack = (StackPanel)dataStack.Children[3]; //mileage stack
            StackPanel finalMileageSliderStack = (StackPanel)mileageStack.Children[3]; // mileage slider stuff
            TextBlock finalMileageMin = (TextBlock)finalMileageSliderStack.Children[0]; //test of fin mge min
            finalMileageMin.Text = ((int)(m / 1000)).ToString() + "k";
            
            //change the slider properties
            Slider mileageSlider = (Slider)finalMileageSliderStack.Children[1]; // slider
            mileageSlider.Minimum = theVehicle.getInitialMileage();
        }

        void updateCityMPG(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateCityMPG((int)((Slider)sender).Value);
            updateMemoBar();
        }

        void updateHighwayMPG(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateHighwayMPG((int)((Slider)sender).Value);
            updateMemoBar();
        }

        private void updateNotes(object sender, TextChangedEventArgs e)
        {
            theVehicle.updateNotes(((TextBox)sender).Text);
        }

        private void updateMemoBar()
        {
            //IMPORTANT NOTE: NEED TO INSERT ACTUAL USER IN HERE


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

        }

        static private string dollarFormat(double i)
        {
            return "$" + String.Format("{0:#,0}", i);
        }

    }
}

