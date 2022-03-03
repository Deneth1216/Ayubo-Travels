using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KryptoonTest
{
    internal class Calculation
    {
        //Creating Common Variables
        int driverDayCost = 1500;
        int ExtraDayCharge = 1000;
        int ExtraHourCharge = 250;

        database_Functions dbFunc = new database_Functions();

        //Find the Daily Rent
        public int VehDayRent(string Veh_Type)
        {
            int dayPrice;
            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from VehicleTypes where Veh_Type = '" + Veh_Type + "'");
                dayPrice = int.Parse(sdr[1].ToString());
                return dayPrice;
            }
            catch (Exception ex)
            {
                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                return 0;

            }
            finally
            {
                dbFunc.conClose();

            }

        }

        //Rent Calc
        public int RentCost(int DayCount, int dailyRent)
        {
            int Value;
            Value = (dailyRent * DayCount);
            return Value;
        }

        //Driver Cost
        public int DriverCost(int DayCount)
        {
            int Value;
            Value = (driverDayCost * DayCount);
            return Value;
        }



        //Packages Price
        public int PackagePrice(string Pac_Type)
        {
            
            try
            {
                int Pac_Price;
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("select * from Packages where Pac_Name = '" + Pac_Type + "'");
                Pac_Price = int.Parse(sdr[5].ToString());
                return Pac_Price;
            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                return 0;
            }
            finally 
            {
                dbFunc.conClose();
            }
           

        }

        //Package Time
        public int PackageHours(string Pac_Type)
        {
            try
            {
                int Max_Hours;
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("select * from Packages where Pac_Name = '" + Pac_Type + "'");
                Max_Hours = int.Parse(sdr[2].ToString());
                return Max_Hours;
            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                return 0;
            }
            finally
            {
                dbFunc.conClose();
            }
        }

        //Package Distance
        public int PackageDistance(int Pac_Id)
        {
            try
            {
                int Max_Distance;
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("select * from Packages where Pac_Id = '" + Pac_Id + "'");
                Max_Distance = int.Parse(sdr[3].ToString());
                return Max_Distance;
                
            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                return 0;
            }
            finally
            {
                dbFunc.conClose();
            }
        }

        //Extra KM Calc
        public int ExtraKM(int Max_Distance, int endMile, int startMile, int Pac_Id)
        {
            string Veh_Type;
            int ExtraKmCharge, ExtraKm, ExtraKmTotal, Mile_Difference;

            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("select * from Packages where Pac_Id = '" + Pac_Id + "'");
                Veh_Type = sdr[4].ToString();
                dbFunc.conClose();
                dbFunc.conOpen();
                SqlDataReader sdr2 = dbFunc.dataRead("select * from VehicleTypes where Veh_Type = '" + Veh_Type + "'");
                ExtraKmCharge = int.Parse(sdr2[2].ToString());
                dbFunc.conClose();
            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                ExtraKmCharge = 0;
                
            }
            finally
            {
                dbFunc.conClose();
            }

            Mile_Difference = endMile - startMile;

            if (Max_Distance>Mile_Difference)
            {
                ExtraKmTotal = 0;
            } else
            {
                ExtraKm = Mile_Difference-Max_Distance;
                ExtraKmTotal = ExtraKm * ExtraKmCharge;
            }
           

            return ExtraKmTotal;
        }

        //Extra Days or Time 
        public int ExtraDays(DateTime start, DateTime end, int Pac_Id)
        {
            int Max_Hours, OvernightCharge, ExtraDays, Max_Days;
            
            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("select * from Packages where Pac_Id = '" + Pac_Id + "'");
                Max_Hours = int.Parse(sdr[2].ToString());
                dbFunc.conClose();
                
            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                OvernightCharge = 0;
                Max_Hours = 0;

            }
            if (Max_Hours>24)
            {
                Max_Days = Max_Hours / 24;
                ExtraDays = (end - start).Days;

                if (Max_Days > ExtraDays)
                {
                    OvernightCharge = 0;
                }
                else
                {
                    OvernightCharge = (ExtraDays - Max_Days) * ExtraDayCharge;
                }
            }
            else
            {
                ExtraDays = (end - start).Hours;
                OvernightCharge = (ExtraDays - Max_Hours) * ExtraHourCharge;
            }
            
            return OvernightCharge;
        }





    }
}
