using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

// Controller Class

namespace Grampa_Bob_s_Calculator
{
    class Calculator
    {
        public static double totalCost(User user, Vehicle vehicle)
        {
            double tax = user.getSalesTaxRate();
            double initialCostNoTax = vehicle.getInitialCostNoTax();
            double initialCostWithTax = initialCostNoTax * (1 + tax);
            return initialCostWithTax;
        }

        private static double totalFinalCost(User user, Vehicle vehicle)
        {
            double interest = user.getInterestRate();
            double years = totalYears(user, vehicle);
            double initialCostWithTax = totalCost(user, vehicle);
            double finalCost = initialCostWithTax * Math.Exp(interest * years);
            return finalCost;
        }

        public static double totalYears(User user, Vehicle vehicle)
        {
            double totalMileage = vehicle.getTotalMiles();
            double milesPerYear = user.getMilesPerYear();
            double totalYears = (totalMileage > 0) ? (totalMileage / milesPerYear) : (0);
            return totalYears;
        }
        
        public static double centsPerMile(User user, Vehicle vehicle)
        {
            //double milesPerYear = user.getMilesPerYear();
            double percentCityMiles = user.getPercentCityMiles();
            double percentHighwayMiles = user.getPercentHighwayMiles();
            double fuelCost = user.getPriceOfFuel();
            //double interest = user.getInterestRate();
            //double tax = user.getSalesTaxRate();

            double initialCostNoTax = vehicle.getInitialCostNoTax();
            //double initialCostWithTax = initialCostNoTax * (1 + tax);
            double initialMileage = vehicle.getInitialMileage();
            double finalMileage = vehicle.getFinalMileage();
            double totalMileage = vehicle.getTotalMiles();
            double years = totalYears(user, vehicle);
            double cityMilesPerGallon = vehicle.getCityMPG();
            double highwayMilesPerGallon = vehicle.getHighwayMPG();

            /*Debug.Assert(((cityMilesPerGallon * percentCityMiles)
                + (highwayMilesPerGallon * percentHighwayMiles)) != 0, "Divide by zero");
            Debug.Assert(totalMileage != 0, "Divide by zero");
            */

            double price = totalCost(user, vehicle);
            double priceRate = price / totalMileage;

            double fuelRate = fuelCost / ((cityMilesPerGallon * percentCityMiles)
                + (highwayMilesPerGallon * percentHighwayMiles));

            double maintenanceRate = (initialMileage + finalMileage) / 4444000;

            double depreciatedMileageValue = 0.25 *
                ((Math.PI / 2) - Math.Atan(((2.5 * finalMileage) / 100000) - 2.1)) - .36;
            double depreciatedTimeValue = 0.9332 * Math.Exp(-0.177 * years);
            double depreciatedRate = depreciatedMileageValue + depreciatedTimeValue;
            if (depreciatedRate > 1) depreciatedRate = 1;
            if (depreciatedRate < 0) depreciatedRate = 0;
            double resellValueRate = (initialCostNoTax * (depreciatedRate)) / totalMileage;

            double centsPerMile = (priceRate + fuelRate + maintenanceRate - resellValueRate) * 100;
            if (Double.IsNaN(centsPerMile) || Double.IsInfinity(centsPerMile))
                centsPerMile = 0;

            return centsPerMile;
        }
    }
}
