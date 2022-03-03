using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;

namespace KryptoonTest
{
    public partial class Finish_Order : KryptonForm
    {
        public Finish_Order()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();
        Calculation calc = new Calculation();
        public int Pac_Id;
        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Orders where Ord_Id = '" + int.Parse(txt_OrderId.Text) + "'");
                string Type = sdr[10].ToString();

                if (Type.Trim() == "Rent-With" || Type.Trim() == "Rent-Without")
                {
                    dtp_StartTime.Format = DateTimePickerFormat.Long;
                    Pac_Id = 0;
                    dtp_EndTime.Enabled = false;
                    dtp_StartTime.Enabled = false;
                }
                else if (Type.Trim() == "Hire-Day")
                {
                    dtp_EndTime.Enabled = true;
                    dtp_StartTime.Enabled = true;
                    dtp_StartTime.Format = DateTimePickerFormat.Time;
                    dtp_EndTime.Format = DateTimePickerFormat.Time;
                    Pac_Id = int.Parse(sdr[11].ToString());
                }
                else if (Type.Trim() == "Hire-Long")
                {
                    dtp_EndTime.Enabled = true;
                    dtp_StartTime.Enabled = true;
                    dtp_StartTime.Format = DateTimePickerFormat.Long;
                    dtp_EndTime.Format = DateTimePickerFormat.Long;
                    Pac_Id = int.Parse(sdr[11].ToString());
                }

                txt_CusId.Text = sdr[4].ToString();
                dtp_OrdDate.Value = DateTime.Parse(sdr[1].ToString());
                txt_Location.Text = sdr[5].ToString();
                txt_StartKm.Text = sdr[6].ToString();
                dtp_StartTime.Value = DateTime.Parse(sdr[7].ToString());
                txt_Estimation.Text = sdr[2].ToString();
                
                dbFunc.conClose();

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show(); 
        }

        private void btn_Calc_Click(object sender, EventArgs e)
        {
            string Type;
            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr9 = dbFunc.dataRead("Select * from Orders where Ord_Id = '" + int.Parse(txt_OrderId.Text) + "'");
                Type = sdr9[10].ToString();
            }
            catch (Exception ex)
            {
                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
                Type = "No Type";
            }

            if (Type.Trim() == "Rent-With" || Type.Trim() == "Rent-Without")
            {
                int Estimation, Value;
                Estimation = int.Parse(txt_Estimation.Text);
                Value = Estimation;
                lbl_Total.Text = Value.ToString();
            }
            else if (Type.Trim() == "Hire-Day")
            {
                int Estimation, StartMile, EndMile, Max_Distance, ExtraKmTotal, Value, Pac_Id, ExtraHourCharge;
                DateTime startTime, EndTime;
                Estimation = int.Parse(txt_Estimation.Text);
                StartMile = int.Parse(txt_StartKm.Text);
                EndMile = int.Parse(txt_EndKm.Text);
                startTime = dtp_StartTime.Value;
                EndTime = dtp_EndTime.Value;

                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Orders where Ord_Id = '" + int.Parse(txt_OrderId.Text) + "'");
                Pac_Id = int.Parse(sdr[11].ToString());
                dbFunc.conClose();

                Max_Distance = calc.PackageDistance(Pac_Id);
                ExtraKmTotal = calc.ExtraKM(Max_Distance, EndMile, StartMile, Pac_Id);
                ExtraHourCharge = calc.ExtraDays(startTime, EndTime, Pac_Id);
                Value = Estimation + ExtraKmTotal+ ExtraHourCharge;
                lbl_Total.Text = Value.ToString();

            }
            else if (Type.Trim() == "Hire-Long")
            {
                int Estimation, StartMile, EndMile, Max_Distance, ExtraKmTotal, Value, Pac_Id, Overnight_Charge;
                DateTime startDate, EndDate;
                Estimation = int.Parse(txt_Estimation.Text);
                StartMile = int.Parse(txt_StartKm.Text);
                EndMile = int.Parse(txt_EndKm.Text);
                startDate = dtp_StartTime.Value;
                EndDate = dtp_EndTime.Value;

                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Orders where Ord_Id = '" + int.Parse(txt_OrderId.Text) + "'");
                Pac_Id = int.Parse(sdr[11].ToString());
                dbFunc.conClose();

                Max_Distance = calc.PackageDistance(Pac_Id);
                ExtraKmTotal = calc.ExtraKM(Max_Distance, EndMile, StartMile, Pac_Id);
                Overnight_Charge = calc.ExtraDays(startDate, EndDate, Pac_Id);
                Value = Estimation + ExtraKmTotal + Overnight_Charge;
                lbl_Total.Text = Value.ToString();
            }

        }

        private void btn_FinishOrder_Click(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                dbFunc.recData("Update Orders Set Status = 1, Ord_Amount = '" + int.Parse(lbl_Total.Text) + "' where Ord_Id = '" + int.Parse(txt_OrderId.Text) + "'");
                SqlDataReader sdr10 = dbFunc.dataRead("Select * from Orders where Ord_Id = '" + int.Parse(txt_OrderId.Text) + "'");
                int Vehi_Id = int.Parse(sdr10[3].ToString());
                dbFunc.conClose();
                dbFunc.conOpen();
                dbFunc.recData("Update Vehicles Set Tripping = 0, Veh_Millage = '" + int.Parse(txt_EndKm.Text) + "' where Veh_Id = '" + Vehi_Id + "' ");
                dbFunc.conClose();
                KryptonMessageBox.Show("The Order is Finished. Your Order Id is :'" + txt_OrderId.Text + "'", "Order Complete!");
            }
            catch (Exception ex)
            {
                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void Finish_Order_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where Status = 1");
            dbFunc.conClose();
        }

        private void Finish_Order_Load(object sender, EventArgs e)
        {

        }
    }
}
