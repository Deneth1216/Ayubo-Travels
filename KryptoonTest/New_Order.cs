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
    public partial class New_Order : KryptonForm
    {
        public New_Order()
        {
            InitializeComponent();
        }

        Calculation calculate = new Calculation();
        database_Functions dbFunc = new database_Functions();

        private void New_Order_Load(object sender, EventArgs e)
        {
         
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cb_Rent_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Rent.Checked && !cb_Hire.Checked)
            {
                grp_Rent.Visible = true;
                grp_Hire.Visible = false;
                lbl_NatureChk.Visible = false;
            }
            else if (!cb_Rent.Checked && cb_Hire.Checked)
            {
                grp_Rent.Visible = false;
                grp_Hire.Visible = true;
                lbl_NatureChk.Visible = false;
            }
            else if (cb_Rent.Checked && cb_Hire.Checked)
            {
                grp_Rent.Visible = false;
                lbl_NatureChk.Visible = true;
                grp_Hire.Visible = false;
            }
            else 
            {
                grp_Hire.Visible = false;
                grp_Rent.Visible = false;
                lbl_NatureChk.Visible = false;
            }
            
        }

        private void txt_VehiType_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Calc_Click_1(object sender, EventArgs e)
        {
            if (cb_Rent.Checked)
            {
                string Veh_Type;
                int dayCount, dailyRent, value;

                dayCount = int.Parse(txt_dayCount.Text);
                Veh_Type = txt_VehiType.Text;
                dailyRent = calculate.VehDayRent(Veh_Type);

                if (cb_WithoutDriver.Checked && !cb_WithDriver.Checked)
                {
                    value = calculate.RentCost(dayCount, dailyRent);
                    lbl_Value.Text = value.ToString();
                }
                else if (cb_WithDriver.Checked && !cb_WithoutDriver.Checked)
                {
                    value = calculate.RentCost(dayCount, dailyRent) + calculate.DriverCost(dayCount);
                    lbl_Value.Text = value.ToString();
                }
                else if (cb_WithDriver.Checked && cb_WithoutDriver.Checked)
                {
                    Error er = new Error();
                    er.ex = "Please Check only one Driver Check Box";
                    er.Show();
                }
                else if (!cb_WithDriver.Checked && !cb_WithoutDriver.Checked)
                {
                    Error er = new Error();
                    er.ex = "Please Check atleast one Driver Check Box";
                    er.Show();

                }
                try
                {
                    string VehType = txt_VehiType.Text;
                    dbFunc.conOpen();
                    SqlDataReader sdr2 = dbFunc.dataRead("Select Top 1 * from Vehicles where Tripping = 0 and Veh_Type = '" + VehType + "'");
                    int VehId = int.Parse(sdr2[0].ToString());
                    dbFunc.conClose();
                    txt_VehiId.Text = VehId.ToString();
                }
                catch (Exception ex)
                {

                    Error error = new Error();
                    error.ex = "No Vehicles Available of that type";
                    error.Show();
                }


            }
            else if (cb_Hire.Checked)
            {
                
                string Pac_type;
                int Pac_vlaue, Max_Hours;

                Pac_type = txt_PacaName.Text;
                Pac_vlaue = calculate.PackagePrice(Pac_type);
                Max_Hours = calculate.PackageHours(Pac_type);
                lbl_Value.Text = Pac_vlaue.ToString();

                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select Top 1 * from Packages where Pac_Name= '" + Pac_type + "'");
                string VehType = sdr[4].ToString();
                dbFunc.conClose();

                dbFunc.conOpen();
                SqlDataReader sdr2 = dbFunc.dataRead("Select Top 1 * from Vehicles where Tripping = 0 and Veh_Type = '" + VehType + "'");
                txt_StartKm.Text = sdr2[1].ToString();
                dbFunc.conClose();

                if (Max_Hours > 24)
                {
                    cb_DayTour.Checked = false;
                    cb_LongTour.Checked = true;
                    dtp_HireStart.Format = DateTimePickerFormat.Short;
                    lbl_Time.Text = "Start Date:";
                }
                else 
                {
                    cb_DayTour.Checked = true;
                    cb_LongTour.Checked = false;
                    dtp_HireStart.Format = DateTimePickerFormat.Time;
                    lbl_Time.Text = "Start Time:";
                }


            }




        }

        private void cb_Hire_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Rent.Checked && !cb_Hire.Checked)
            {
                grp_Rent.Visible = true;
                grp_Hire.Visible = false;
                lbl_NatureChk.Visible = false;
            }
            else if (!cb_Rent.Checked && cb_Hire.Checked)
            {
                grp_Rent.Visible = false;
                grp_Hire.Visible = true;
                lbl_NatureChk.Visible = false;
            }
            else if (cb_Rent.Checked && cb_Hire.Checked)
            {
                grp_Rent.Visible = false;
                lbl_NatureChk.Visible = true;
                grp_Hire.Visible = false;
            }
            else
            {
                grp_Hire.Visible = false;
                grp_Rent.Visible = false;
                lbl_NatureChk.Visible = false;
            }
        }

        private void kryptonGroupBox1_Panel_Paint(object sender, PaintEventArgs e)
        {


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_PlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //Creating Variables
                DateTime Ord_Date, Start_Time;
                int Ord_Amount, VehId, CusId, Millage, EmpId, Pac_Id;
                String Location, Package, Type;
                Type = "No Type";

                Ord_Date = dtp_OrdDate.Value;

                Ord_Amount = int.Parse(lbl_Value.Text);
                CusId = int.Parse(txt_CusId.Text);
                Package = txt_PacaName.Text;

                //Finding a Vehicle ID and current Millage
                string VehType;

                if (cb_Hire.Checked)
                {
                    Location = txt_HLocation.Text;
                    Start_Time = dtp_HireStart.Value;
                    dbFunc.conOpen();
                    SqlDataReader sdr6 = dbFunc.dataRead("Select Top 1 * from Packages where Pac_Name= '" + Package + "'");
                    VehType = sdr6[4].ToString();
                    Pac_Id = int.Parse(sdr6[0].ToString());
                    dbFunc.conClose();
                    if (cb_DayTour.Checked)
                    {
                        Type = "Hire-Day";
                    }
                    else if (cb_LongTour.Checked)
                    {
                        Type = "Hire-Long";
                    }
                }
                else
                {
                    Start_Time = DateTime.Today;
                    Location = txt_Location.Text;
                    VehType = txt_VehiType.Text;
                    Pac_Id = 0;
                    if (cb_WithDriver.Checked)
                    {
                        Type = "Rent-With";
                    }
                    else if (cb_WithoutDriver.Checked)
                    {
                        Type = "Rent-Without";

                    }
                }
                try
                {
                    dbFunc.conOpen();
                    SqlDataReader sdr2 = dbFunc.dataRead("Select Top 1 * from Vehicles where Tripping = 0 and Veh_Type = '" + VehType + "'");
                    VehId = int.Parse(sdr2[0].ToString());
                    Millage = int.Parse(sdr2[1].ToString());
                    dbFunc.conClose();
                    txt_VehiId.Text = VehId.ToString();
                }
                catch (Exception ex)
                {

                    Error error = new Error();
                    error.ex = "No Vehicles Available of that type";
                    error.Show();
                    throw; 
                }



                //get the currently Logged in User
                dbFunc.conOpen();
                SqlDataReader sdr3 = dbFunc.dataRead("Select * from Login where Status = 1");
                EmpId = int.Parse(sdr3[0].ToString());
                dbFunc.conClose();

                //Update the Order Table
                dbFunc.conOpen();
                dbFunc.recData("Insert into Orders Values ('" + Ord_Date.Date + "','" + Ord_Amount + "','" + VehId + "','" + CusId + "','" + Location + "','" + Millage + "','" + Start_Time + "','" + EmpId + "', '" + 0 + "', '" + Type + "', '"+Pac_Id+"')");
                dbFunc.recData("Update Vehicles set Tripping = 1 where Veh_Id = '" + VehId + "'");
                dbFunc.recData("Insert into Trips Values ('" + CusId + "','" + EmpId + "','" + VehId + "')");
                SqlDataReader sdr = dbFunc.dataRead("Select max(Ord_id) from Orders");
                string Ord_Id = sdr[0].ToString();
                KryptonMessageBox.Show("The Order is Updated. Your Order Id is :'"+Ord_Id+"'", "Order Started!");
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

        private void New_Order_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where Status = 1");
            dbFunc.conClose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
