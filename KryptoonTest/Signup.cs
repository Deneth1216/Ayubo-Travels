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
    public partial class Signup : KryptonForm
    {
        public Signup()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            Login loginfrm = new Login();
            this.Hide();
            loginfrm.Show();
        }

        private void btn_Signup_Click(object sender, EventArgs e)
        {

            //Assigning Values from Text Boxes
            string username, password, email, conpassword, id;
            username = txt_UserName.Text;
            email = txt_Email.Text;
            password = txt_Password.Text;
            conpassword = txt_ConPass.Text;

            if (password==conpassword)
            {
                try
                {
                    dbFunc.conOpen();
                    dbFunc.recData("Insert into Employees (Emp_Email) Values ('" + email + "') ");
                }
                catch (Exception ex)
                {

                    Error er = new Error();
                    er.ex = ex.Message;
                    er.Show();
                }
                finally
                {
                    dbFunc.conClose();
                }
                try

                {
                    dbFunc.conOpen();
                    SqlDataReader sdr = dbFunc.dataRead("select * from Employees where Emp_Email = '" + email + "'");
                    id = sdr[0].ToString();
                    dbFunc.conClose();
                    dbFunc.conOpen();
                    dbFunc.recData("Insert into Login Values ('" + int.Parse(id) + "', '" + username + "', '" + password + "', '" + 0 + "' ) ");
                }
                catch (Exception ex)

                {
                    Error er = new Error();
                    er.ex = ex.Message;
                    er.Show();
                }

                finally
                {
                    dbFunc.conClose();
                    lbl_sign.Visible = true;
                }


            }
            else
            {
                lbl_pass.Visible = true;
            }
            
        }
    }
}
