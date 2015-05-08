using System;
using Windows.UI.Xaml.Controls;

namespace Grampa_Bob_s_Calculator
{
    class User
    {       
        public User(StackPanel p)
        {
            container = p;
            display = new UserDisplay(p, this);
        }

        private StackPanel container = null;
        private UserDisplay display = null;
        
        private double _milesPerYear = 0;
        private double _percentCityMiles = 0;
        private double _priceOfFuel = 0;
        private double _interestRate = 0;
        private double _salesTaxRate = 0;

        public double MilesPerYear
        {
            get { return _milesPerYear; }
            set
            {
                if (value < MinMiles || value > MaxMiles) throw new ArgumentOutOfRangeException();
                else _milesPerYear = value;
            }
        }

        public double PercentCityMiles
        {
            get { return _percentCityMiles; }
            set
            {
                if (value < MinPercentCity || value > MaxPercentCity) throw new ArgumentOutOfRangeException();
                else _percentCityMiles = value;
            }
        }

        public double PercentHighwayMiles
        {
            get { return 1 - _percentCityMiles; }
        }

        public double PriceOfFuel
        {
            get { return _priceOfFuel; }
            set
            {
                if (value < MinPriceFuel || value > MaxPriceFuel) throw new ArgumentOutOfRangeException();
                else _priceOfFuel = value;
            }
        }

        public double InterestRate
        {
            get { return _interestRate / 100; }
            set
            {
                if (value < MinInterest || value > MaxInterest) throw new ArgumentOutOfRangeException();
                else _interestRate = value;
            }
        }

        public double SalesTaxRate
        {
            get { return _salesTaxRate / 100; }
            set
            {
                if (value < MinTax || value > MaxTax) throw new ArgumentOutOfRangeException();
                else _salesTaxRate = value;
            }
        }

        public void ClearDisplay()
        {
            display.clearData();
        }

        private static int _MAX_MILES_PER_YEAR = 100000;
        private static double _MAX_PERCENT_CITY_MILES = 1.0;
        private static double _MAX_PRICE_OF_FUEL = 10.0;
        private static double _MAX_INTEREST_RATE = 10.0;
        private static double _MAX_SALES_TAX_RATE = 20.0;

        public static int MaxMiles { get { return _MAX_MILES_PER_YEAR; } }
        public static double MaxPercentCity { get { return _MAX_PERCENT_CITY_MILES; } }
        public static double MaxPriceFuel { get { return _MAX_PRICE_OF_FUEL; } }
        public static double MaxInterest { get { return _MAX_INTEREST_RATE; } }
        public static double MaxTax { get { return _MAX_SALES_TAX_RATE; } }

        public static int MinMiles { get { return 0; } }
        public static double MinPercentCity { get { return 0; } }
        public static double MinPriceFuel { get { return 0; } }
        public static double MinInterest { get { return 0; } }
        public static double MinTax { get { return 0; } }
    }
}
