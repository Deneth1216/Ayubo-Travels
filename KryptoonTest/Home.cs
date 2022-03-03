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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace KryptoonTest
{
    public partial class Home : KryptonForm
    {
        public Home()
        {
            InitializeComponent();
        }

        database_Functions dbFunc = new database_Functions();

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Login loginfrm = new Login();
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where UserName = '"+lbl_Username.Text+"'");
            this.Hide();
            loginfrm.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            string designaion;
            designaion = "No One";

            try
            {
                dbFunc.conOpen();
                SqlDataReader sdr = dbFunc.dataRead("Select * from Login where Status = 1");
                lbl_Username.Text = sdr[1].ToString();
                int ID = int.Parse(sdr[0].ToString());
                dbFunc.conClose();
                dbFunc.conOpen();
                SqlDataReader sdr1 = dbFunc.dataRead("Select * from Employees where Emp_Id = '"+ID+"'");
                designaion = sdr1[3].ToString();
                dbFunc.conClose();
                

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

            if (designaion.Trim() == "Owner"||designaion.Trim() == "Director")
            {
                grp_Admin.Visible = true;
                grp_Admin.Enabled = true;
            }
            else
            {
                grp_Admin.Enabled = false;
                grp_Admin.Text = "You Are not an Administrator!!";
                grp_Admin.Cursor = Cursors.No;
            }

        }

        private void btn_NewOrder_Click(object sender, EventArgs e)
        {
            New_Order neworder = new New_Order();
            this.Hide();
            neworder.Show();
        }

        private void btn_MyAcc_Click(object sender, EventArgs e)
        {
            My_Account myacc = new My_Account();
            this.Hide();
            myacc.Show();
        }

        private void btn_Customers_Click(object sender, EventArgs e)
        {
            Customers cusfrm = new Customers();
            this.Hide();
            cusfrm.Show();
        }

        private void btn_CalculateCost_Click(object sender, EventArgs e)
        {
            Finish_Order finish_Order = new Finish_Order();
            this.Hide();
            finish_Order.Show();
        }

        private void btn_Packages_Click(object sender, EventArgs e)
        {
            Packages packfrm = new Packages();
            this.Hide();
            packfrm.Show();
        }

        private void btn_Vehicles_Click(object sender, EventArgs e)
        {
            Vehicles vehicles = new Vehicles();
            this.Hide();
            vehicles.Show();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login loginfrm = new Login();
            dbFunc.conOpen();
            dbFunc.recData("Update Login Set Status = 0 where UserName = '" + lbl_Username.Text + "'");
            this.Hide();
            loginfrm.Show();
        }

        private void btn_Revenue_Click(object sender, EventArgs e)
        {
            
            string creator, month;
            int revenue, orderCount;
            month = DateTime.Today.ToString("MMMM");

            //Order Count and Revenue
            dbFunc.conOpen();
            SqlDataReader sdr = dbFunc.dataRead("Select SUM(Ord_Amount) From Orders");
            revenue = int.Parse(sdr[0].ToString());
            dbFunc.conClose();

            dbFunc.conOpen();
            SqlDataReader sdr1 = dbFunc.dataRead("Select Count(Ord_id) From Orders");
            orderCount = int.Parse(sdr1[0].ToString());
            dbFunc.conClose();

            creator = lbl_Username.Text;

            try
            {


                #region Common Part
                PdfPTable pdfTableBlank = new PdfPTable(1);
                pdfTableBlank.DefaultCell.BorderWidth = 0;

                //Footer Section
                PdfPTable pdfTableFooter = new PdfPTable(1);
                pdfTableFooter.DefaultCell.BorderWidth = 0;
                pdfTableFooter.WidthPercentage = 100;
                pdfTableFooter.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                Chunk cnkFooter = new Chunk("Ayubo Travels", FontFactory.GetFont("Poppins"));
                //cnkFooter.Font.SetStyle(1);
                cnkFooter.Font.Size = 12;
                pdfTableFooter.AddCell(new Phrase(cnkFooter));
                //End Of Footer Section

                pdfTableBlank.AddCell(new Phrase(" "));
                pdfTableBlank.DefaultCell.Border = 0;
                #endregion

                #region Page
                #region Section-1

                PdfPTable pdfTable1 = new PdfPTable(1);//Here 1 is Used For Count of Column
                PdfPTable pdfTable2 = new PdfPTable(1);
                PdfPTable pdfTable3 = new PdfPTable(2);

                //Font Style
                System.Drawing.Font fontH1 = new System.Drawing.Font("Playfair Display", 16);

                //pdfTable1.DefaultCell.Padding = 5;
                pdfTable1.WidthPercentage = 80;
                pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
                pdfTable1.DefaultCell.BorderWidth = 0;


                //pdfTable1.DefaultCell.Padding = 5;
                pdfTable2.WidthPercentage = 80;
                pdfTable2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable2.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
                pdfTable2.DefaultCell.BorderWidth = 0;

                pdfTable3.DefaultCell.Padding = 5;
                pdfTable3.WidthPercentage = 80;
                pdfTable3.DefaultCell.BorderWidth = 0.5f;


                Chunk c1 = new Chunk("Revenue Statement", FontFactory.GetFont("Playfair Display"));
                c1.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c1.Font.SetStyle(0);
                c1.Font.Size = 14;
                Phrase p1 = new Phrase();
                p1.Add(c1);
                pdfTable1.AddCell(p1);
                Chunk c2 = new Chunk("107/D, Ihalayagoda, Gampaha", FontFactory.GetFont("Playfair Display"));
                c2.setLineHeight(30);
                c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c2.Font.SetStyle(0);//0 For Normal Font
                c2.Font.Size = 11;
                Phrase p2 = new Phrase();
                p2.Add(c2);
                pdfTable2.AddCell(p2);
                Chunk c3 = new Chunk("Private and Confidential. Don't Share with anyone", FontFactory.GetFont("Times New Roman"));
                c3.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c3.Font.SetStyle(0);
                c3.Font.Size = 11;
                Phrase p3 = new Phrase();
                p3.Add(c3);
                pdfTable2.AddCell(p3);

                #endregion
                #region Section-Image





                #region section Table
                pdfTable3.AddCell(new Phrase("Month "));
                pdfTable3.AddCell(new Phrase(month));
                pdfTable3.AddCell(new Phrase("Created By "));
                pdfTable3.AddCell(new Phrase(creator));

                pdfTable3.AddCell(new Phrase("Total Revenue"));
                pdfTable3.AddCell(new Phrase(revenue.ToString()));
                pdfTable3.AddCell(new Phrase("Total Orders "));
                pdfTable3.AddCell(new Phrase(orderCount.ToString()));
                #endregion

                #endregion


                #region Pdf Generation
                string folderPath = "D:\\PDF\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //File Name
                int fileCount = Directory.GetFiles("D:\\PDF").Length;
                string strFileName = "Revenue Statement" + DateTime.Today.Day +" - "+ DateTime.Today.Month + ".pdf";

                using (FileStream stream = new FileStream(folderPath + strFileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    #region PAGE-1
                    pdfDoc.Add(pdfTable1);
                    pdfDoc.Add(pdfTable2);
                    pdfDoc.Add(pdfTableBlank);
                    pdfDoc.Add(pdfTable3);
                    pdfDoc.Add(pdfTableBlank);
                    pdfDoc.Add(pdfTableBlank);
                    pdfDoc.Add(pdfTableFooter);
                    pdfDoc.NewPage();
                    #endregion
                    pdfDoc.Close();
                    stream.Close();
                }
                #endregion

                #region Display PDF
                System.Diagnostics.Process.Start(folderPath + "\\" + strFileName);
                #endregion
                #endregion

            }
            catch (Exception ex)
            {

                
            }
        }

        private void btn_EmployeeDetails_Click(object sender, EventArgs e)
        {
            OrderDetails orderDetails = new OrderDetails();
            this.Hide();
            orderDetails.Show();
        }
    }
    }

