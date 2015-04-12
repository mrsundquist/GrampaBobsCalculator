using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Controller Class

namespace Grampa_Bob_s_Calculator
{
    class Calculator
    {
        User user;
        List<Vehicle> vehicles;

        public double calculate(Vehicle vehicle)
        {
            double milesPerYear = user.getMilesPerYear();
            double percentCityMiles = user.getPercentCityMiles();
            double percentHighwayMiles = user.getPercentHighwayMiles();
            double fuelCost = user.getPriceOfFuel();
            double interest = user.getInterestRate();
            double tax = user.getSalesTaxRate();

            double initialCostNoTax = vehicle.getInitialCostNoTax();
            double initialCostWithTax = initialCostNoTax * (1 + tax);
            double initialMileage = vehicle.getInitialMileage();
            double finalMileage = vehicle.getFinalMileage();
            double totalMileage = vehicle.getTotalMiles();
            double totalYears = (totalMileage > 0) ? (totalMileage / milesPerYear) : (0);
            double cityMilesPerGallon = vehicle.getCityMPG();
            double highwayMilesPerGallon = vehicle.getHighwayMPG();

            double price = initialCostWithTax * Math.Exp(interest * totalYears);
            double priceRate = price / totalMileage;

            double fuelRate = fuelCost / ((cityMilesPerGallon * percentCityMiles)
                + (highwayMilesPerGallon * percentHighwayMiles));

            double maintenanceRate = (initialMileage + finalMileage) / 4444000;

            double depreciatedMileageValue = 0.25 *
                ((Math.PI / 2) - Math.Atan(((2.5 * finalMileage) / 100000) - 2.1)) - .36;
            double depreciatedTimeValue = 0.9332 * Math.Exp(-0.177 * totalYears);
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
