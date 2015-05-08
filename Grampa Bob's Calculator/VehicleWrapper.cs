using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grampa_Bob_s_Calculator
{
    class VehicleWrapper
    {        
        public VehicleWrapper(StackPanel p, User theUser, Button theButton)
        {
            this._theUser = theUser;
            this.theVehicle = new Vehicle();
            this.theDisplay = new VehicleDisplay(p, this, totalVehiclesCreated);
            VehicleWrapper.vehicleWrappers.Add(this);
            VehicleWrapper.vehicles.Add(theVehicle);
            VehicleWrapper._vehicleDisplays.Add(theDisplay);
            VehicleWrapper.theButton = theButton;
            VehicleWrapper.totalVehiclesCreated++;
            if (VehicleWrapper.vehicleWrappers.Count() >= 10)
                VehicleWrapper.theButton.Visibility = Visibility.Collapsed;
        }

        private static List<VehicleWrapper> vehicleWrappers = new List<VehicleWrapper>();
        private static List<Vehicle> vehicles = new List<Vehicle>();
        private static  List<VehicleDisplay> _vehicleDisplays = new List<VehicleDisplay>();
        private static Button theButton;
        private static int totalVehiclesCreated;

        public static int NumVehicles { get { return vehicleWrappers.Count(); } }
        public static int TotalNumVehicles { get { return totalVehiclesCreated; } }
        public static List<VehicleDisplay> VehicleDisplays { get { return _vehicleDisplays; } }

        private User _theUser = null;
        private Vehicle theVehicle = null;
        private VehicleDisplay theDisplay = null;

        public User TheUser { get { return _theUser; } }

        public static void DeleteAllVehicles()
        {
            while (vehicleWrappers.Count != 0)
            {
                vehicleWrappers[0].Delete();
                totalVehiclesCreated = 0;
            }
        }
        
        public void Delete()
        {
            this.theDisplay.clearDisplay();
            VehicleWrapper.vehicles.Remove(this.theVehicle);
            VehicleWrapper._vehicleDisplays.Remove(this.theDisplay);
            VehicleWrapper.vehicleWrappers.Remove(this);
            if (VehicleWrapper.vehicleWrappers.Count() < 10)
                theButton.Visibility = Visibility.Visible;
        }

        public string VehicleDescription { get { return theVehicle.VehicleDescription; } }
        public string Year { get { return theVehicle.Year; } set { theVehicle.Year = value; } }
        public string Make { get { return theVehicle.Make; } set { theVehicle.Make = value; } }
        public string Model { get { return theVehicle.Model; } set { theVehicle.Model = value; } }
        public string Source { get { return theVehicle.Source; } set { theVehicle.Source = value; } }
        public int Price { get { return theVehicle.Price; } set { theVehicle.Price = value; } }
        public int RepairCost { get { return theVehicle.RepairCost; } set { theVehicle.RepairCost = value; } }
        public int InitialCostNoTax { get { return theVehicle.InitialCostNoTax; } }
        public int InitialMileage { get { return theVehicle.InitialMileage; } set { theVehicle.InitialMileage = value; } }
        public int FinalMileage { get { return theVehicle.FinalMileage; } set { theVehicle.FinalMileage = value; } }
        public int TotalMiles { get { return theVehicle.TotalMiles; } }
        public int CityMPG { get { return theVehicle.CityMPG; } set { theVehicle.CityMPG = value; } }
        public int HighwayMPG { get { return theVehicle.HighwayMPG; } set { theVehicle.HighwayMPG = value; } }
        public string Notes { get { return theVehicle.Notes; } set { theVehicle.Notes = value; } }

        public static int MaxPrice { get { return Vehicle.MaxPrice; } }
        public static int MaxRepairCost { get { return Vehicle.MaxRepairCost; } }
        public static int MaxInitialMileage { get { return Vehicle.MaxInitialMileage; } }
        public static int MaxFinalMileage { get { return Vehicle.MaxFinalMileage; } }
        public static int MaxCityMPG { get { return Vehicle.MaxCityMPG; } }
        public static int MaxHighwayMPG { get { return Vehicle.MaxHighwayMPG; } }
        public static int MinPrice { get { return Vehicle.MinPrice; } }
        public static int MinRepairCost { get { return Vehicle.MinRepairCost; } }
        public static int MinInitialMileage { get { return Vehicle.MinInitialMileage; } }
        public int MinFinalMileage { get { return theVehicle.MinFinalMileage; } }
        public static int MinCityMPG { get { return Vehicle.MinCityMPG; } }
        public static int MinHighwayMPG { get { return Vehicle.MinHighwayMPG; } }
    }
}