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
    public partial class Vehicles : KryptonForm
    {
        public Vehicles()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();

        private void Vehicles_Load(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                dgv_Vehicles.DataSource = dbFunc.showTable("Select * From Vehicles");
                dbFunc.conClose();

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }

        }

        private void rdb_Veh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdb_Veh.Checked)
                {
                    dbFunc.conOpen();
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From Vehicles");
                    dbFunc.conClose();
                    txt_Search.Text = "Search with Vehicle ID";
                    lbl_AvailKm.Text = "Availability";
                    lbl_MillType.Text = "Millage";
                    lbl_TypePrice.Text = "Vehicle Type";
                }
                else
                {
                    dbFunc.conOpen();
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From VehicleTypes");
                    dbFunc.conClose();
                    txt_Search.Text = "Search with Vehicle Type";
                    lbl_AvailKm.Text = "Extra KM Cost";
                    lbl_MillType.Text = "Vehicle Type";
                    txt_AvailKm.Enabled = true;
                    lbl_TypePrice.Text = "Daily Price";
                }

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
            
        }

        public void Clear()
        {
            txt_Search.Clear();
            txt_MillType.Clear();
            txt_AvailKm.Clear();
            txt_typePrice.Clear();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdb_Veh.Checked)
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Insert into Vehicles Values ('"+int.Parse(txt_MillType.Text)+"', '"+txt_typePrice.Text+"', 0)");
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From Vehicles");
                    dbFunc.conClose();

                }
                else
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Insert into VehicleTypes Values ('" + txt_MillType.Text + "', '" + txt_typePrice.Text + "', '"+txt_AvailKm.Text+"')");
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From VehicleTypes");
                    dbFunc.conClose();
                }

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdb_Veh.Checked)
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Update Vehicles Set Veh_Millage = '" + int.Parse(txt_MillType.Text) + "', Veh_Type = '" + txt_typePrice.Text + "' where Veh_Id = '"+int.Parse(txt_Search.Text)+"'");
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From Vehicles");
                    dbFunc.conClose();

                }
                else
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Update VehicleTypes Set  DayPrice = '" + txt_typePrice.Text + "' ,  ExtraKM = '" + txt_AvailKm.Text + "' where Veh_Type= '" + txt_MillType.Text+ "'");
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From VehicleTypes");
                    dbFunc.conClose();
                }

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdb_Veh.Checked)
                {
                    dbFunc.conOpen();
                    SqlDataReader sdr = dbFunc.dataRead("Select * from Vehicles where Veh_Id = '" + int.Parse(txt_Search.Text) + "'");
                    txt_MillType.Text = sdr[1].ToString();
                    txt_typePrice.Text = sdr[2].ToString();
                    txt_AvailKm.Text = sdr[3].ToString();
                    dbFunc.conClose();

                }
                else
                {
                    dbFunc.conOpen();
                    SqlDataReader sdr = dbFunc.dataRead("Select * from VehicleTypes where Veh_Type = '" + txt_Search.Text + "'");
                    txt_MillType.Text = sdr[0].ToString();
                    txt_typePrice.Text = sdr[1].ToString();
                    txt_AvailKm.Text = sdr[2].ToString();
                    dbFunc.conClose();
                }

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdb_Veh.Checked)
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Delete from Vehicles where Veh_Id = '" + int.Parse(txt_Search.Text) + "'");
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From Vehicles");
                    dbFunc.conClose();

                }
                else
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Delete from VehicleTypes where Veh_Type= '" + txt_MillType.Text + "'");
                    dgv_Vehicles.DataSource = dbFunc.showTable("Select * From VehicleTypes");
                    dbFunc.conClose();
                }

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        private void Vehicles_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where Status = 1");
            dbFunc.conClose();
        }
    }
}
