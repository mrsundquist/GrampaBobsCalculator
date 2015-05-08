using System;
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
            this.display = new VehicleDisplay(p, this, totalVehiclesCreated);
            this._theUser = theUser;
            VehicleWrapper._vehicleDisplays.Add(display);
            VehicleWrapper.vehicles.Add(this);
            VehicleWrapper.theButton = theButton;
            VehicleWrapper.totalVehiclesCreated++;
            if (VehicleWrapper.vehicles.Count() >= 10)
                VehicleWrapper.theButton.Visibility = Visibility.Collapsed;
        }

        private static List<VehicleWrapper> vehicles = new List<VehicleWrapper>();
        private static  List<VehicleDisplay> _vehicleDisplays = new List<VehicleDisplay>();
        private static Button theButton;
        private static int totalVehiclesCreated;

        public static List<VehicleDisplay> VehicleDisplays { get { return _vehicleDisplays; } }

        private User _theUser = null;
        private VehicleDisplay display = null;

        public User TheUser { get { return _theUser; } }

        private string year = "";
        private string make = "";
        private string model = "";
        private string source = "";
        private int price = 0;
        private int repairCost = 0;
        private int initialMileage = 0;
        private int finalMileage = 0;
        private int cityMPG = 1;
        private int highwayMPG = 1;
        private string notes = "";

        public static int NumVehicles()
        {
            return vehicles.Count();
        }

        public static int TotalNumVehicles()
        {
            return totalVehiclesCreated;
        }

        public static void DeleteAllVehicles()
        {
            while (vehicles.Count != 0)
            {
                vehicles[0].Delete();
                totalVehiclesCreated = 0;
            }
        }
        
        public void Delete()
        {
            this.display.clearDisplay();
            _vehicleDisplays.Remove(this.display);
            vehicles.Remove(this);
            if (vehicles.Count() < 10)
                theButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public string getVehicleDescription()
        {
            if (source != null && source != "")
                return year + " " + make + " " + model + " (" + source + ")";
            else
                return year + " " + make + " " + model;
        }
        public string getYear() { return year; }
        public string getMake() { return make; }
        public string getModel() { return model; }
        public string getSource() { return source; }
        public int getPrice() { return price; }
        public int getRepairCost() { return repairCost; }
        public int getInitialCostNoTax() { return price + repairCost; }
        public int getInitialMileage() { return initialMileage; }
        public int getFinalMileage() { return finalMileage; }
        public int getTotalMiles() { return finalMileage - initialMileage; }
        public int getCityMPG() { return cityMPG; }
        public int getHighwayMPG() { return highwayMPG; }
        public string getNotes() { return notes; }

        public void updateYear(string s) { year = s; }
        public void updateMake(string s) { make = s; }
        public void updateModel(string s) { model = s; }
        public void updateSource(string s) { source = s; }
        public void updatePrice(int i)
        {
            if (i < VehicleWrapper.getMinPrice() || i > VehicleWrapper.getMaxPrice())
                throw new ArgumentOutOfRangeException();
            else
                price = i;
        }
        public void updateRepairCost(int i)
        {
            if(i < VehicleWrapper.getMinRepairCost() || i > VehicleWrapper.getMaxRepairCost())
                throw new ArgumentOutOfRangeException();
            else
                repairCost = i;
        }
        public void updateInitialMileage(int i)
        {
            if (i < VehicleWrapper.getMinInitialMileage() || i > VehicleWrapper.getMaxInitialMileage())
                throw new ArgumentOutOfRangeException();
            else
            {
                initialMileage = i;
                if (finalMileage < initialMileage)
                    finalMileage = initialMileage;
            }
        }
        public void updateFinalMileage(int i)
        {
            if (i < this.getMinFinalMileage() || i > VehicleWrapper.getMaxFinalMileage())
                throw new ArgumentOutOfRangeException();
            else
                finalMileage = i;
        }
        public void updateCityMPG(int i)
        {
            if (i < VehicleWrapper.getMinCityMPG() || i > VehicleWrapper.getMaxCityMPG())
                throw new ArgumentOutOfRangeException();
            else
                cityMPG = i;
        }
        public void updateHighwayMPG(int i)
        {
            if (i < VehicleWrapper.getMinHighwayMPG() || i > VehicleWrapper.getMaxHighwayMPG())
                throw new ArgumentOutOfRangeException();
            else
                highwayMPG = i;
        }
        public void updateNotes(string s) { notes = s; }

        #region max and min values
        private static int MAX_PRICE = 35000;
        private static int MAX_REPAIR_COST = 10000;
        private static int MAX_INITIAL_MILEAGE = 300000;
        private static int MAX_FINAL_MILEAGE = 300000;
        private static int MAX_CITY_MPG = 75;
        private static int MAX_HIGHWAY_MPG = 75;

        public static int getMaxPrice() { return MAX_PRICE; }
        public static int getMaxRepairCost() { return MAX_REPAIR_COST; }
        public static int getMaxInitialMileage() { return MAX_INITIAL_MILEAGE; }
        public static int getMaxFinalMileage() { return MAX_FINAL_MILEAGE; }
        public static int getMaxCityMPG() { return MAX_CITY_MPG; }
        public static int getMaxHighwayMPG() { return MAX_HIGHWAY_MPG; }

        public static int getMinPrice() { return 0; }
        public static int getMinRepairCost() { return 0; }
        public static int getMinInitialMileage() { return 0; }
        public int getMinFinalMileage() { return initialMileage; }
        public static int getMinCityMPG() { return 1; }
        public static int getMinHighwayMPG() { return 1; }
        #endregion
    }
}