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
    class DisplayNameBox
    {
        private Border nameBox = null;
        public TextBlock rowName;

        public DisplayNameBox(Windows.UI.Xaml.Controls.StackPanel p, SolidColorBrush color2)
        {
            nameBox = new Border();
            p.Children.Add(nameBox);
            nameBox.Background = color2;
            nameBox.Width = 260;
            nameBox.Height = 200;

            rowName = new TextBlock();
            nameBox.Child = rowName;
            rowName.TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords;
            //rowName.Padding = new Windows.UI.Xaml.Thickness(30, 0, 0,0);
            rowName.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            rowName.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            rowName.FontSize = 56;
            rowName.LineHeight = 37;
            rowName.FontFamily = new FontFamily("Segoe UI");
            rowName.FontWeight = Windows.UI.Text.FontWeights.Light;
            rowName.LineStackingStrategy = Windows.UI.Xaml.LineStackingStrategy.BlockLineHeight;
        }
    }
    
    class DisplayTextBlockStack
    {
        public StackPanel stackP = null;

        public DisplayTextBlockStack(Windows.UI.Xaml.Controls.StackPanel p, string t1, string t2, string t3, string t4)
        {
            this.stackP = new StackPanel();
            p.Children.Add(this.stackP);
            this.stackP.Margin = new Windows.UI.Xaml.Thickness(0, 0, 10, 0);
            this.stackP.Width = 120;
            DisplayTextBlock text1 = new DisplayTextBlock(this.stackP, t1);
            DisplayTextBlock text2 = new DisplayTextBlock(this.stackP, t2);
            DisplayTextBlock text3 = new DisplayTextBlock(this.stackP, t3);
            DisplayTextBlock text4 = new DisplayTextBlock(this.stackP, t4);
        }
    }
    
    class DisplayTextBlock
    {
        private TextBlock textB = null;

        public DisplayTextBlock(Windows.UI.Xaml.Controls.StackPanel p, string t)
        {
            this.textB = new TextBlock();
            p.Children.Add(this.textB);
            this.textB.Text = t;
            this.textB.Height = 50;
            this.textB.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            this.textB.Margin = new Windows.UI.Xaml.Thickness(40, 0, 0, 0);
            this.textB.Padding = new Windows.UI.Xaml.Thickness(0, 0, 0, 20);
            this.textB.FontFamily = new FontFamily("Segoe UI");
            this.textB.FontSize = 27;
            this.textB.FontWeight = Windows.UI.Text.FontWeights.Light;
            this.textB.LineHeight = 30;
        }
    }

    class DisplayTextBoxStack
    {
        public StackPanel stackP = null;

        public DisplayTextBoxStack(Windows.UI.Xaml.Controls.StackPanel p)
        {
            this.stackP = new StackPanel();
            p.Children.Add(this.stackP);
            this.stackP.Margin = new Windows.UI.Xaml.Thickness(0, 0, 40, 0);
            this.stackP.Width = 300;
            DisplayTextBox text1 = new DisplayTextBox(this.stackP, "year");
            DisplayTextBox text2 = new DisplayTextBox(this.stackP, "make");
            DisplayTextBox text3 = new DisplayTextBox(this.stackP, "model");
            DisplayTextBox text4 = new DisplayTextBox(this.stackP, "source");
        }
    }

    class DisplayTextBox
    {
        private TextBox textB = null;

        public DisplayTextBox(Windows.UI.Xaml.Controls.StackPanel p, string s)
        {
            this.textB = new TextBox();
            p.Children.Add(this.textB);
            this.textB.Height = 30;
            this.textB.Margin = new Windows.UI.Xaml.Thickness(0, 5, 0, 15);
            this.textB.Padding = new Windows.UI.Xaml.Thickness(10, 3, 10, 5);
            this.textB.Name = s;
        }
    }

    class DisplaySliderStack
    {
        public StackPanel stackP = null;

        public DisplaySliderStack(Windows.UI.Xaml.Controls.StackPanel p,
            string t1, string t2, int min1, int max1, string minText1, string maxText1,
            int min2, int max2, string minText2, string maxText2,
            SolidColorBrush color1, SolidColorBrush color2)
        {
            this.stackP = new StackPanel();
            p.Children.Add(this.stackP);
            this.stackP.Margin = new Windows.UI.Xaml.Thickness(0, 0, 40, 0);
            this.stackP.Width = 540;
            DisplayTextBlock text1 = new DisplayTextBlock(this.stackP, t1);
            DisplaySlider slide1 = new DisplaySlider(this.stackP, min1, max1, minText1, maxText1, color1, color2);
            DisplayTextBlock text2 = new DisplayTextBlock(this.stackP, t2);
            DisplaySlider slide2 = new DisplaySlider(this.stackP, min2, max2, minText2, maxText2, color1, color2);
        }
    }

    class DisplaySoloSliderStack
    {
        public StackPanel stackP = null;

        public DisplaySoloSliderStack(Windows.UI.Xaml.Controls.StackPanel p,
            string t1, int min1, int max1, string minText1, string maxText1,
            SolidColorBrush color1, SolidColorBrush color2)
        {
            this.stackP = new StackPanel();
            p.Children.Add(this.stackP);
            this.stackP.Margin = new Windows.UI.Xaml.Thickness(0, 0, 40, 0);
            this.stackP.Width = 540;
            DisplayTextBlock text1 = new DisplayTextBlock(this.stackP, t1);
            DisplaySlider slide1 = new DisplaySlider(this.stackP, min1, max1, minText1, maxText1, color1, color2);
        }
    }

    class DisplaySlider
    {
        public StackPanel sliderStack = null;
        public Slider slide = null;
        private int minVal = 0;
        private int maxVal = 0;
        private string minValText = null;
        private string maxValText = null;


        public DisplaySlider(Windows.UI.Xaml.Controls.StackPanel p, int min, int max,
            string minText, string maxText, SolidColorBrush color1, SolidColorBrush color2)
        {
            this.sliderStack = new StackPanel();
            p.Children.Add(this.sliderStack);
            this.sliderStack.Margin = new Windows.UI.Xaml.Thickness(40, 0, 0, 0);
            this.sliderStack.Orientation = Orientation.Horizontal;

            TextBlock loText = new TextBlock();
            sliderStack.Children.Add(loText);
            loText.Text = minText;
            loText.Width = 42;
            loText.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            loText.Margin = new Windows.UI.Xaml.Thickness(0, 0, 5, 0);
            loText.Padding = new Windows.UI.Xaml.Thickness(0, 0, 0, 10);
            loText.FontFamily = new FontFamily("Segoe UI");
            loText.FontSize = 12;
            loText.LineHeight = 20;

            this.slide = new Slider();
            sliderStack.Children.Add(slide);
            slide.Background = color1;
            slide.Foreground = color2;
            slide.Maximum = max;
            slide.Minimum = min;
            slide.LargeChange = Math.Pow(10, (Math.Floor(Math.Log10((int)(max - min)))-1));
            if (slide.LargeChange == 0) slide.LargeChange = 1;
            slide.SmallChange = slide.LargeChange;
            slide.StepFrequency = ((slide.LargeChange)/10);
            //if (slide.StepFrequency == 0) slide.StepFrequency = 1;
            slide.TickFrequency = ((max - min) / 10);
            slide.Width = 406;
            slide.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;

            TextBlock hiText = new TextBlock();
            sliderStack.Children.Add(hiText);
            hiText.Text = maxText;
            hiText.Width = 42;
            hiText.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            hiText.Margin = new Windows.UI.Xaml.Thickness(5, 0, 0, 0);
            hiText.Padding = new Windows.UI.Xaml.Thickness(0, 0, 0, 10);
            hiText.FontFamily = new FontFamily("Segoe UI");
            hiText.FontSize = 12;
            hiText.LineHeight = 20;
        }
    }

    class DisplayNotesStack
    {
        public StackPanel stackP = null;

        public DisplayNotesStack(Windows.UI.Xaml.Controls.StackPanel p, string t1, string t2)
        {
            this.stackP = new StackPanel();
            p.Children.Add(this.stackP);
            this.stackP.Margin = new Windows.UI.Xaml.Thickness(0, 0, 40, 0);
            this.stackP.Width = 540;
            DisplayTextBlock text1 = new DisplayTextBlock(this.stackP, t1);
            DisplayNotesBox memo = new DisplayNotesBox(this.stackP, t2);
        }
    }

    class DisplayNotesBox
    {
        private TextBox textB = null;

        public DisplayNotesBox(Windows.UI.Xaml.Controls.StackPanel p, string s)
        {
            this.textB = new TextBox();
            p.Children.Add(this.textB);
            this.textB.AcceptsReturn = true;
            this.textB.PlaceholderText = s;
            this.textB.Width = 500;
            this.textB.Margin = new Windows.UI.Xaml.Thickness(40, 5, 0, 15);
            this.textB.MaxHeight = 130;
            this.textB.Padding = new Windows.UI.Xaml.Thickness(10, 3, 10, 5);
            this.textB.TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap;
        }
    }

    class DisplayMemoScroll
    {
        public ScrollViewer memoScroll = null;

        public DisplayMemoScroll(StackPanel p)
        {
            //memoBar -> memoScroll
            this.memoScroll = new ScrollViewer();
            p.Children.Add(memoScroll);
            memoScroll.VerticalScrollMode = ScrollMode.Disabled;
            memoScroll.ZoomMode = ZoomMode.Disabled;
            memoScroll.Width = p.Width - 260;
            memoScroll.Margin = new Windows.UI.Xaml.Thickness(260, 0, 0, 0);
            memoScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            memoScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

            //memoBar -> memoScroll -> memoStack
            StackPanel memoStack = new StackPanel();
            memoScroll.Content = memoStack;
            memoStack.Orientation = Orientation.Horizontal;
        }
    }

    class DisplayMemoTextBlock
    {
        public TextBlock memoText = null;

        public DisplayMemoTextBlock(StackPanel p, string s)
        {
            this.memoText = new TextBlock();
            p.Children.Add(memoText);
            this.memoText.Text = s;
            this.memoText.Margin = new Windows.UI.Xaml.Thickness(0, 16, 0, 0);
            this.memoText.FontSize += 5;
        }
    }

    class DisplayMemoTextBox
    {
        public TextBox memoText = null;

        public DisplayMemoTextBox(StackPanel p, string s, SolidColorBrush color)
        {
            this.memoText = new TextBox();
            p.Children.Add(memoText);
            this.memoText.Text = s;
            this.memoText.Foreground = color;
            this.memoText.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            this.memoText.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            this.memoText.IsReadOnly = true;
            this.memoText.Width = 90;
            this.memoText.Margin = new Windows.UI.Xaml.Thickness(0, 10, 10, 0);
        }
    }
}