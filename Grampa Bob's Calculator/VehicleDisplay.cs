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
        
        public VehicleDisplay(Windows.UI.Xaml.Controls.StackPanel p)
        {
            //parent -> display
            this.display = new StackPanel();
            p.Children.Add(this.display);
            this.display.Background = new SolidColorBrush(Color.FromArgb(179, 212, 107, 61)); // Red1
            this.display.Height = 200;
            this.display.Width = p.Width; // set width to parent's width
            //don't think i need horiz allignmnet b/c it is default
            //this.display.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            //this.display.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            this.display.Orientation = Orientation.Horizontal;
            //need to set scroll bar visibility for vertical and horizontal?

            //display -> nameBox
            Border nameBox = new Border();
            this.display.Children.Add(nameBox);
            nameBox.Background = new SolidColorBrush(Color.FromArgb(255, 118, 42, 9));
            nameBox.Width = 260;
            nameBox.Height = 200;

            //display -> nameBox -> nameBoxLabel
            TextBlock nameBoxLabel = new TextBlock();
            nameBox.Child = nameBoxLabel;
            nameBoxLabel.Text = "Vehicle\n Data";
            nameBoxLabel.Padding = new Windows.UI.Xaml.Thickness(40, 40, 0, 0);
            nameBoxLabel.FontFamily = new FontFamily("Segoe UI");
            nameBoxLabel.FontSize = 56;
            nameBoxLabel.FontWeight = Windows.UI.Text.FontWeights.Light;
            nameBoxLabel.LineStackingStrategy = Windows.UI.Xaml.LineStackingStrategy.BlockLineHeight;
            nameBoxLabel.LineHeight = 40;

            //display -> scroller
            ScrollViewer scroller = new ScrollViewer();
            this.display.Children.Add(scroller);
            scroller.VerticalScrollMode = ScrollMode.Disabled;
            scroller.ZoomMode = ZoomMode.Disabled;
            scroller.Width = this.display.Width - nameBox.Width;
            scroller.Height = 200;
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;


            //display -> scroller -> dataStack
            StackPanel dataStack = new StackPanel();
            scroller.Content = dataStack;  // ???
            dataStack.Height = 200;
            dataStack.Orientation = Orientation.Horizontal;
            dataStack.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;

            //display -> scroller -> dataStack -> descStack1
            DisplayTextBlockStack descStack1 = new DisplayTextBlockStack(dataStack, "Year:", "Make:", "Model:", "Source:");

            //display -> scroller -> dataStack -> descStack2
            DisplayTextBoxStack descStack2 = new DisplayTextBoxStack(dataStack);
        
            //display -> scroller -> dataStack -> priceStack
            DisplaySliderStack priceStack = new DisplaySliderStack(dataStack, "Price", "Cost to Repair", 0, 35000, "$0", "$35,000", 0, 10000, "$0", "$10,000");
        }
    }
}
