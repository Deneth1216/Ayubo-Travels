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
    public partial class Error : KryptonForm
    {
        public string ex;

        public Error()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void Error_Load(object sender, EventArgs e)
        {
            lbl_ex.Text = ex;
        }
    }
}
