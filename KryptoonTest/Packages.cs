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
    public partial class Packages : KryptonForm
    {
        public Packages()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();
        private void Packages_Load(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                dgv_PacData.DataSource = dbFunc.showTable("Select * from Packages");
                dbFunc.conClose();

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
            txt_Name.Clear();
            txt_PacPrice.Clear();
            txt_MaxKm.Clear();
            txt_MaxHrs.Clear();
            txt_VehType.Clear();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                dbFunc.recData("Insert into Packages Values ('" + txt_Name.Text + "','" + int.Parse(txt_MaxHrs.Text) + "','" + int.Parse(txt_MaxKm.Text) + "', '" + txt_VehType.Text + "', '"+int.Parse(txt_PacPrice.Text) +"')");
                dgv_PacData.DataSource = dbFunc.showTable("Select * from Packages");
                dbFunc.conClose();
                Clear();

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Packages where Pac_Id = '" + int.Parse(txt_Search.Text) + "'");
                txt_Name.Text = sdr[1].ToString();
                txt_MaxHrs.Text = sdr[2].ToString();
                txt_MaxKm.Text = sdr[3].ToString();
                txt_VehType.Text = sdr[4].ToString();
                txt_PacPrice.Text = sdr[5].ToString();
                dbFunc.conClose();

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
                dbFunc.conOpen();
                dbFunc.recData("Update Packages Set Pac_Name = '" + txt_Name.Text + "', Pac_MaxHours = '" + int.Parse(txt_MaxHrs.Text) + "', Pac_MaxKm = '" + int.Parse(txt_MaxKm.Text) + "', Veh_Type = '" + txt_VehType.Text + "', Pac_Price = '" + int.Parse(txt_PacPrice.Text) + "' where Pac_Id = '"+int.Parse(txt_Search.Text)+"'");
                dgv_PacData.DataSource = dbFunc.showTable("Select * from Packages");
                dbFunc.conClose();
                Clear();

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
                dbFunc.conOpen();
                dbFunc.recData("Delete from Packages where Pac_Id = '" + int.Parse(txt_Search.Text) + "'");
                dgv_PacData.DataSource = dbFunc.showTable("Select * from Packages");
                dbFunc.conClose();
                Clear();

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
        }

        private void Packages_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where Status = 1");
            dbFunc.conClose();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }
    }
}
