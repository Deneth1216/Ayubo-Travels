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
namespace KryptoonTest
{
    public partial class OrderDetails : KryptonForm
    {
        public OrderDetails()
        {
            InitializeComponent();
        }

        private void dgv_OrderDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        database_Functions dbFunc = new database_Functions();
        private void OrderDetails_Load(object sender, EventArgs e)
        {
            dbFunc.conOpen();
            dgv_OrderDetails.DataSource = dbFunc.showTable("Select * from Orders");
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
