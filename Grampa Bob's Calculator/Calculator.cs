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

        public static double averageMPG(User user, Vehicle vehicle)
        {
            double percentCityMiles = user.getPercentCityMiles();
            double percentHighwayMiles = user.getPercentHighwayMiles();
            double cityMilesPerGallon = vehicle.getCityMPG();
            double highwayMilesPerGallon = vehicle.getHighwayMPG();
            double mpg = ((cityMilesPerGallon * percentCityMiles)
                + (highwayMilesPerGallon * percentHighwayMiles));
            return mpg;
        }

        public static double lifetimeMaintenance(Vehicle vehicle)
        {
            double totalMileage = vehicle.getTotalMiles();
            double lifetimeMaintenance = totalMileage * maintenancePerMile(vehicle);
            return lifetimeMaintenance;
        }

        private static double maintenancePerMile(Vehicle vehicle)
        {
            double initialMileage = vehicle.getInitialMileage();
            double finalMileage = vehicle.getFinalMileage();
            double lifetimeMaintenance = ((initialMileage + finalMileage) / 4444000);
            return lifetimeMaintenance;
        }

        public static double resellValue(User user, Vehicle vehicle)
        {
            double finalMileage = vehicle.getFinalMileage();
            double years = totalYears(user, vehicle);
            double initialCostNoTax = vehicle.getInitialCostNoTax();
            double depreciatedMileageValue = 
                0.25 * ((Math.PI / 2) - Math.Atan(((2.5 * finalMileage) / 100000) - 2.1)) - .36;
            double depreciatedTimeValue = 0.9332 * Math.Exp(-0.177 * years);
            double depreciatedRate = depreciatedMileageValue + depreciatedTimeValue;
            if (depreciatedRate > 1) depreciatedRate = 1;
            if (depreciatedRate < 0) depreciatedRate = 0;
            double resellValue = initialCostNoTax * (depreciatedRate);
            return resellValue;
        }
        
        public static double centsPerMile(User user, Vehicle vehicle)
        {
            double totalMileage = vehicle.getTotalMiles();
            double price = totalCost(user, vehicle);
            double priceRate = price / totalMileage;

            double fuelCost = user.getPriceOfFuel();
            double mpg = averageMPG(user, vehicle);
            double fuelRate = fuelCost / mpg;

            double maintenanceRate = maintenancePerMile(vehicle);
            
            double resellValueRate = (resellValue(user, vehicle)) / totalMileage;

            double centsPerMile = (priceRate + fuelRate + maintenanceRate - resellValueRate) * 100;
            if (Double.IsNaN(centsPerMile) || Double.IsInfinity(centsPerMile))
                centsPerMile = 0;

            return centsPerMile;
        }
    }
}
