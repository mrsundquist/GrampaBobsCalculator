using System;

namespace Grampa_Bob_s_Calculator
{
    static class Calculator
    {
        public static double TotalCost(User theUser, VehicleWrapper theVehicle)
        {
            double tax = theUser.SalesTaxRate;
            double initialCostNoTax = theVehicle.InitialCostNoTax;
            double initialCostWithTax = initialCostNoTax * (1 + tax);
            return initialCostWithTax;
        }

        private static double totalFinalCost(User theUser, VehicleWrapper theVehicle)
        {
            double interest = theUser.InterestRate;
            double years = TotalYears(theUser, theVehicle);
            double initialCostWithTax = TotalCost(theUser, theVehicle);
            double finalCost = initialCostWithTax * Math.Exp(interest * years);
            return finalCost;
        }

        public static double TotalYears(User theUser, VehicleWrapper theVehicle)
        {
            double totalMileage = theVehicle.TotalMiles;
            double milesPerYear = theUser.MilesPerYear;
            double totalYears = (totalMileage > 0 && milesPerYear > 0) ? (totalMileage / milesPerYear) : (0);
            return totalYears;
        }

        public static double AverageMPG(User theUser, VehicleWrapper theVehicle)
        {
            double percentCityMiles = theUser.PercentCityMiles;
            double percentHighwayMiles = theUser.PercentHighwayMiles;
            double cityMilesPerGallon = theVehicle.CityMPG;
            double highwayMilesPerGallon = theVehicle.HighwayMPG;
            double mpg = ((cityMilesPerGallon * percentCityMiles)
                + (highwayMilesPerGallon * percentHighwayMiles));
            return mpg;
        }

        public static double LifetimeMaintenance(VehicleWrapper theVehicle)
        {
            double totalMileage = theVehicle.TotalMiles;
            double lifetimeMaintenance = totalMileage * maintenancePerMile(theVehicle);
            return lifetimeMaintenance;
        }

        private static double maintenancePerMile(VehicleWrapper theVehicle)
        {
            double initialMileage = theVehicle.InitialMileage;
            double finalMileage = theVehicle.FinalMileage;
            double lifetimeMaintenance = ((initialMileage + finalMileage) / 4444000);
            return lifetimeMaintenance;
        }

        public static double ResellValue(User theUser, VehicleWrapper theVehicle)
        {
            double finalMileage = theVehicle.FinalMileage;
            double years = TotalYears(theUser, theVehicle);
            double initialCostNoTax = theVehicle.InitialCostNoTax;
            double depreciatedMileageValue = 
                0.25 * ((Math.PI / 2) - Math.Atan(((2.5 * finalMileage) / 100000) - 2.1)) - .36;
            double depreciatedTimeValue = 0.9332 * Math.Exp(-0.177 * years);
            double depreciatedRate = depreciatedMileageValue + depreciatedTimeValue;
            if (depreciatedRate > 1) depreciatedRate = 1;
            if (depreciatedRate < 0) depreciatedRate = 0;
            double resellValue = initialCostNoTax * (depreciatedRate);
            return resellValue;
        }
        
        public static double CentsPerMile(User theUser, VehicleWrapper theVehicle)
        {
            double totalMileage = theVehicle.TotalMiles;
            double price = totalFinalCost(theUser, theVehicle);
            double priceRate = price / totalMileage;

            double fuelCost = theUser.PriceOfFuel;
            double mpg = AverageMPG(theUser, theVehicle);
            double fuelRate = fuelCost / mpg;

            double maintenanceRate = maintenancePerMile(theVehicle);
            
            double resellValueRate = (ResellValue(theUser, theVehicle)) / totalMileage;

            double centsPerMile = (priceRate + fuelRate + maintenanceRate - resellValueRate) * 100;
            if (Double.IsNaN(centsPerMile) || Double.IsInfinity(centsPerMile))
                centsPerMile = 0;

            return centsPerMile;
        }
    }
}
