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
        public Vehicle theVehicle = null;
        
        
        public VehicleDisplay(Windows.UI.Xaml.Controls.StackPanel p, Vehicle theVehicle)
        {
            this.theVehicle = theVehicle;

            //parent -> display
            this.display = new StackPanel();
            p.Children.Add(this.display);
            SolidColorBrush color1 = new SolidColorBrush(Color.FromArgb(179, 212, 107, 61));
            SolidColorBrush color2 = new SolidColorBrush(Color.FromArgb(255, 118, 42, 9));
            this.display.Background = color1;
            this.display.Height = 200;
            this.display.Width = p.Width; // set width to parent's width
            this.display.Orientation = Orientation.Horizontal;

            //display -> borderName
            DisplayNameBox borderName = new DisplayNameBox(display, color2);
            updateName("New Vehicle");
            
            //display -> scroller
            ScrollViewer scroller = new ScrollViewer();
            this.display.Children.Add(scroller);
            scroller.VerticalScrollMode = ScrollMode.Disabled;
            scroller.ZoomMode = ZoomMode.Disabled;
            scroller.Width = this.display.Width - 260;
            scroller.Height = 200;
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            
            //display -> scroller -> dataStack
            StackPanel dataStack = new StackPanel();
            scroller.Content = dataStack;
            dataStack.Height = 200;
            dataStack.Orientation = Orientation.Horizontal;
            dataStack.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

            //display -> scroller -> dataStack -> descStack1
            DisplayTextBlockStack descStack1 = new DisplayTextBlockStack(dataStack, "Year:", "Make:", "Model:", "Source:");

            //display -> scroller -> dataStack -> descStack2
            DisplayTextBoxStack descStack2 = new DisplayTextBoxStack(dataStack);
            UIElementCollection textBoxes = descStack2.stackP.Children;
            ((TextBox)textBoxes[0]).TextChanged += new TextChangedEventHandler(updateYear);
            ((TextBox)textBoxes[1]).TextChanged += new TextChangedEventHandler(updateMake);
            ((TextBox)textBoxes[2]).TextChanged += new TextChangedEventHandler(updateModel);
            ((TextBox)textBoxes[3]).TextChanged += new TextChangedEventHandler(updateSource);
        
            //display -> scroller -> dataStack -> priceStack
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

            //display -> scroller -> dataStack -> mileageStack
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
            ((Slider)priceStackChildren[1]).ValueChanged += updateFinalMileage;
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
        }

        void updateRepairCost(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateRepairCost((int)((Slider)sender).Value);
        }

        void updateInitialMileage(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateInitialMileage((int)((Slider)sender).Value);
            updateFinalMileageMin(theVehicle.getInitialMileage());
        }

        void updateFinalMileage(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            theVehicle.updateFinalMileage((int)((Slider)sender).Value);
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
    }
}

