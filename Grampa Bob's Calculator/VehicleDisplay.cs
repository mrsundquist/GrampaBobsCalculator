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
                "Price", "Cost to Repair", 0, 35000, "$0", "$35,000", 0, 10000,
                "$0", "$10,000", color1, color2);
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
            if (fontSize < 12) fontSize = 12;


            rowName.FontSize = Math.Floor(fontSize);
            

            rowName.Text = t;
        }

    }
}
