using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data.SqlClient;

namespace KryptoonTest
{
    public partial class Customers : KryptonForm
    {
        public Customers()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();

        public void Clear()
        {
            txt_Search.Clear();
            txt_Name.Clear();
            txt_Email.Clear();
            txt_Number.Clear();
            txt_Address.Clear();
        }

        private void Customers_Load(object sender, EventArgs e)
        {

            try
            {
                dbFunc.conOpen();
                dgv_CusData.DataSource = dbFunc.showTable("Select * from Customers");
                dbFunc.conClose();

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
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Customers where Cus_Id = '" + int.Parse(txt_Search.Text) + "'");
                txt_Name.Text = sdr[1].ToString();
                txt_Email.Text = sdr[2].ToString();
                txt_Number.Text = sdr[3].ToString();
                txt_Address.Text = sdr[4].ToString();
                dbFunc.conClose();

            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                dbFunc.recData("Insert into Customers Values ('" + txt_Name.Text + "','" + txt_Email.Text + "','" + int.Parse(txt_Number.Text) + "', '"+ txt_Address.Text + "')");
                dgv_CusData.DataSource = dbFunc.showTable("Select * from Customers");
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

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                dbFunc.conOpen();
                dbFunc.recData("Update Customers Set Cus_Name = '" + txt_Name.Text + "', Cus_Email = '" + txt_Email.Text + "', Cus_Tele = '" + int.Parse(txt_Number.Text) + "', Cus_Address = '" + txt_Address.Text + "' where Cus_Id = '"+int.Parse(txt_Search.Text)+"'");
                dgv_CusData.DataSource = dbFunc.showTable("Select * from Customers");
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
                dbFunc.recData("Delete from Customers where Cus_Id = '" + int.Parse(txt_Search.Text) + "'");
                dgv_CusData.DataSource = dbFunc.showTable("Select * from Customers");
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

        private void btn_Back_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        private void Customers_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where Status = 1");
            dbFunc.conClose();
        }
    }
}
