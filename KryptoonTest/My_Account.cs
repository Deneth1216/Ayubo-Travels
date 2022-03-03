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
    public partial class My_Account : KryptonForm
    {
        public My_Account()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();

        private void My_Account_Load(object sender, EventArgs e)
        {
            try
            {
                //Getting Active EMP ID
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Login where Status = 1");
                int emp_Id = int.Parse(sdr[0].ToString());
                dbFunc.conClose();

                //Getting Other Details
                dbFunc.conOpen();
                SqlDataReader sdr2 = dbFunc.dataRead("Select * from Employees where Emp_Id = '" + emp_Id + "'");

                //Assigning Values
                txt_EmpId.Text = sdr2[0].ToString();
                txt_EmpName.Text = sdr2[1].ToString();
                txt_EmpEmail.Text = sdr2[2].ToString();
                txt_Designation.Text = sdr2[3].ToString();
                txt_Telephone.Text = sdr2[4].ToString();
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
                dbFunc.recData("Update Employees set Emp_Name = '" + txt_EmpName.Text + "', Emp_Email = '" + txt_EmpEmail.Text + "', Emp_Designation = '" + txt_Designation.Text + "', Emp_Tele = '" + int.Parse(txt_Telephone.Text) + "' where Emp_Id = '" + int.Parse(txt_EmpId.Text) + "'");
                dbFunc.conClose();
                lbl_Update.Visible = true;
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

        private void My_Account_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where Status = 1");
            dbFunc.conClose();
        }
    }
}
