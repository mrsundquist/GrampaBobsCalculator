using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

namespace Grampa_Bob_s_Calculator
{
    class VehicleDisplay
    {
        public StackPanel display = null;
        public TextBox yearDisplay;
        
        
        public VehicleDisplay(Windows.UI.Xaml.Controls.StackPanel p)
        {
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
            DisplayNameBox borderName = new DisplayNameBox(display, color2, "Vehicle\n Name");
            
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
        
            //display -> scroller -> dataStack -> priceStack
            DisplaySliderStack priceStack = new DisplaySliderStack(dataStack,
                "Price", "Cost to Repair", 0, 35000, "$0", "$35,000", 0, 10000,
                "$0", "$10,000", color1, color2);
        }
    }
}
