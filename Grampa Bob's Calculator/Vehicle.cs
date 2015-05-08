using System;

namespace Grampa_Bob_s_Calculator
{
    class Vehicle
    {
        private string _year = "";
        private string _make = "";
        private string _model = "";
        private string _source = "";
        private int _price = 0;
        private int _repairCost = 0;
        private int _initialMileage = 0;
        private int _finalMileage = 0;
        private int _cityMPG = 1;
        private int _highwayMPG = 1;
        private string _notes = "";

        public string VehicleDescription
        {
            get
            {
                if (_source != null && _source != "")
                    return _year + " " + _make + " " + _model + " (" + _source + ")";
                else
                    return _year + " " + _make + " " + _model;
            }
        }

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value < MinPrice || value > MaxPrice) throw new ArgumentOutOfRangeException();
                else _price = value;
            }
        }

        public int RepairCost
        {
            get { return _repairCost; }
            set
            {
                if (value < MinRepairCost || value > MaxRepairCost) throw new ArgumentOutOfRangeException();
                else _repairCost = value;
            }
        }

        public int InitialCostNoTax
        {
            get { return _price + _repairCost; }
        }

        public int InitialMileage
        {
            get { return _initialMileage; }
            set
            {
                if (value < MinInitialMileage || value > MaxInitialMileage) throw new ArgumentOutOfRangeException();
                else
                {
                    _initialMileage = value;
                    if (_finalMileage < _initialMileage) _finalMileage = _initialMileage;
                }
            }
        }

        public int FinalMileage
        {
            get { return _finalMileage; }
            set
            {
                if (value < MinFinalMileage || value > MaxFinalMileage) throw new ArgumentOutOfRangeException();
                else _finalMileage = value;
            }
        }

        public int TotalMiles
        {
            get { return _finalMileage - _initialMileage; }
        }

        public int CityMPG
        {
            get { return _cityMPG; }
            set
            {
                if (value < MinCityMPG || value > MaxCityMPG) throw new ArgumentOutOfRangeException();
                else _cityMPG = value;
            }
        }

        public int HighwayMPG
        {
            get { return _highwayMPG; }
            set
            {
                if (value < MinHighwayMPG || value > MaxHighwayMPG) throw new ArgumentOutOfRangeException();
                else _highwayMPG = value;
            }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
 
        private static int _MAX_PRICE = 35000;
        private static int _MAX_REPAIR_COST = 10000;
        private static int _MAX_INITIAL_MILEAGE = 300000;
        private static int _MAX_FINAL_MILEAGE = 300000;
        private static int _MAX_CITY_MPG = 75;
        private static int _MAX_HIGHWAY_MPG = 75;

        public static int MaxPrice { get { return _MAX_PRICE; } }
        public static int MaxRepairCost { get { return _MAX_REPAIR_COST; } }
        public static int MaxInitialMileage { get { return _MAX_INITIAL_MILEAGE; } }
        public static int MaxFinalMileage { get { return _MAX_FINAL_MILEAGE; } }
        public static int MaxCityMPG { get { return _MAX_CITY_MPG; } }
        public static int MaxHighwayMPG { get { return _MAX_HIGHWAY_MPG; } }

        public static int MinPrice { get { return 0; } }
        public static int MinRepairCost { get { return 0; } }
        public static int MinInitialMileage { get { return 0; } }
        public int MinFinalMileage { get { return _initialMileage; } }
        public static int MinCityMPG { get { return 1; } }
        public static int MinHighwayMPG { get { return 1; } }
    }
}