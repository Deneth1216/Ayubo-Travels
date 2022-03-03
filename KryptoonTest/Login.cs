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
    public partial class Login : KryptonForm
    {
        public Login()
        {
            InitializeComponent();
        }
        //Referencing DB Functions Class
        database_Functions dbFunc = new database_Functions();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            Signup signupfrm = new Signup();
            this.Hide();
            signupfrm.Show();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            //Assigning memory Variables
            string username, password;
            username = txt_UserName.Text.Trim();
            password = txt_Password.Text.Trim();
            try
            {
                //Opening DB Connection
                dbFunc.conOpen();

                //Checking for data
                SqlDataAdapter sda = dbFunc.dataAdapt("select * from Login where Username = '" + username + "' and Password = '" + password + "'");
                

                //Adding the data to a table
                DataTable dataTbl = new DataTable();
                sda.Fill(dataTbl);

                //Checking if there is a match for the given values
                if (dataTbl.Rows.Count > 0)
                {
                    dbFunc.recData("Update Login Set Status = 1 where Username = '" + username + "' and Password = '" + password + "'");
                    Home homefrm = new Home();
                    this.Hide();
                    homefrm.Show();

                }
                else
                {
                    lbl_Error.Visible = true;
                    txt_Password.Clear();
                    txt_UserName.Clear();
                    txt_UserName.Focus();
                }
            }
            catch (Exception ex)
            {

                Error er = new Error();
                er.ex = ex.Message;
                er.Show();
            }
            finally
            {
                
            }
            

        }
    }
}
