using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Grampa_Bob_s_Calculator
{
    class User
    {       
        private StackPanel container = null;
        private UserDisplay display = null;
        
        public User(StackPanel p)
        {
            container = p;
            display = new UserDisplay(p, this);
        }
        
        private double milesPerYear = 20000; // 0
        private double percentCityMiles = .5; //0
        private double priceOfFuel = 2.50; // 0
        private double interestRate = 0.10; // 0
        private double salesTaxRate = 0.10; // 0

        public double getMilesPerYear() { return milesPerYear; }
        public double getPercentCityMiles() { return percentCityMiles; }
        public double getPercentHighwayMiles() { return 1 - percentCityMiles; }
        public double getPriceOfFuel() { return priceOfFuel; }
        public double getInterestRate() { return interestRate; }
        public double getSalesTaxRate() { return salesTaxRate; }

        public void updateMilesPerYear(double d)
        {
            if (d < User.getMinMiles() || d > User.getMaxMiles())
                throw new ArgumentOutOfRangeException();
            else
                milesPerYear = d;
        }
        public void updatePercentCityMiles(double d)
        {
            if (d < User.getMinPercentCity() || d > User.getMaxPercentCity())
                throw new ArgumentOutOfRangeException();
            else
                percentCityMiles = d;
        }
        public void updatePriceOfFuel(double d)
        {
            if (d < User.getMinPriceFuel() || d > User.getMaxPriceFuel())
                throw new ArgumentOutOfRangeException();
            else
                priceOfFuel = d;
        }
        public void updateInterestRate(double d)
        {
            if (d < User.getMinInterest() || d > User.getMaxInterest())
                throw new ArgumentOutOfRangeException();
            else
                interestRate = d;
        }
        public void updateSalesTaxRate(double d)
        {
            if (d < User.getMinTax() || d > User.getMaxTax())
                throw new ArgumentOutOfRangeException();
            else
                salesTaxRate = d;
        }
        
        #region max and min values
        private static int MAX_MILES_PER_YEAR = 100000;
        private static double MAX_PERCENT_CITY_MILES = 1.0;
        private static double MAX_PRICE_OF_FUEL = 10.0;
        private static double MAX_INTEREST_RATE = 10.0;
        private static double MAX_SALES_TAX_RATE = 20.0;

        public static int getMaxMiles() { return MAX_MILES_PER_YEAR; }
        public static double getMaxPercentCity() { return MAX_PERCENT_CITY_MILES; }
        public static double getMaxPriceFuel() { return MAX_PRICE_OF_FUEL; }
        public static double getMaxInterest() { return MAX_INTEREST_RATE; }
        public static double getMaxTax() { return MAX_SALES_TAX_RATE; }

        public static int getMinMiles() { return 0; }
        public static double getMinPercentCity() { return 0; }
        public static double getMinPriceFuel() { return 0; }
        public static double getMinInterest() { return 0; }
        public static double getMinTax() { return 0; }
        #endregion
    }
}
