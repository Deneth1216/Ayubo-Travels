using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace KryptoonTest
{
    internal class database_Functions
    {
        //Create Instant Variables
        SqlConnection sqlCon;
        SqlCommand sqlCom;
        SqlDataReader sqlRead;
        SqlDataAdapter sqlAdapt;
        DataSet dsEmp;

        //Create Memory Variables
        string sql;


        //Connection Open Method
        public SqlConnection conOpen()
        {
            sql = @"Data Source=DESKTOP-K0VCCF8\SQLEXPRESS;Initial Catalog=AyuboTravels;Integrated Security=True";
            sqlCon = new SqlConnection(sql);
            sqlCon.Open();
            return sqlCon;
        }

        //Connection Close Method
        public void conClose()
        {
            sqlCon.Close();
        }

        //Record Operations
        public void recData(string querry)
        {
            sqlCom = new SqlCommand(querry, sqlCon);
            sqlCom.ExecuteNonQuery();
        }

        //Show Table Method
        public object showTable(string querry)
        {
            sqlAdapt = new SqlDataAdapter(querry, sqlCon);
            sqlAdapt.Fill(dsEmp = new DataSet());
            object dgv = dsEmp.Tables[0];
            return dgv;
        }

        //Search Method
        public SqlDataReader dataRead(string querry)
        {
            sqlCom = new SqlCommand(querry, sqlCon);
            sqlRead = sqlCom.ExecuteReader();
            sqlRead.Read();
            return sqlRead;
        }

        //data adapting
        public SqlDataAdapter dataAdapt(string querry)
        {

            sqlAdapt = new SqlDataAdapter(querry, sqlCon);
            return sqlAdapt;

        }

    }
}
