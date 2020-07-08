using System;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace RMS
{
    class Retrieval
    {
        public void showDailySales(DateTime date, DataGridView gv, DataGridViewColumn salidgv,
       DataGridViewColumn totalgv, DataGridViewColumn amountgivgv, DataGridViewColumn amtreturngv)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getSaleRecord", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                salidgv.DataPropertyName = dt.Columns["OrderID"].ToString();
                totalgv.DataPropertyName = dt.Columns["TotalAmount"].ToString();
                amountgivgv.DataPropertyName = dt.Columns["Received"].ToString();
                amtreturngv.DataPropertyName = dt.Columns["Return"].ToString();
                

                gv.DataSource = dt;
            }
            catch (Exception ex)
            {

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }
        }
        public void showkitchenReport(ReportDocument rd, CrystalReportViewer crv,Int64 orderid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getKitchen", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderID", orderid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rd.Load(Application.StartupPath + "\\SaleReports\\KitchenReceipt.rpt");
                rd.SetDataSource(dt);
                crv.ReportSource = rd;
                crv.RefreshReport();

            }
            catch (Exception ex)
            {
                if(rd != null)
                {
                    rd.Close();
                }

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }
        }
        public void showsaleReport(ReportDocument rd, CrystalReportViewer crv, Int64 orderid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getOrderReport", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderID", orderid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rd.Load(Application.StartupPath + "\\SaleReports\\SaleReceipt.rpt");
                rd.SetDataSource(dt);
                crv.ReportSource = rd;
                crv.RefreshReport();

            }
            catch (Exception ex)
            {
                if (rd != null)
                {
                    rd.Close();
                }

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }
        }

         public void getordersbill(int tableid,DataGridView gv,DataGridViewColumn itemgv,DataGridViewColumn quangv, DataGridViewColumn amountgv,DataGridViewColumn orderidgv)
        {
            try
            {
                 SqlCommand cmd = new SqlCommand("st_getOrderDetailsWRTTable", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableid", tableid);
                Connection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                itemgv.DataPropertyName = dt.Columns["Item"].ToString();
                amountgv.DataPropertyName = dt.Columns["Total Amount"].ToString();
                quangv.DataPropertyName = dt.Columns["Quantity"].ToString();
                orderidgv.DataPropertyName = dt.Columns["ID"].ToString();
              

                gv.DataSource = dt;
                Connection.con.Close();
            }
            catch (Exception ex)
            {
                Connection.con.Close();

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }
        }
         //public void getRunningOrderDetails(Int64 orderID, DataGridView gv, DataGridViewColumn proidgv, DataGridViewColumn pronamgv, DataGridViewColumn pricegv, DataGridViewColumn quangv, DataGridViewColumn totalgv)
         //{
         //    try
         //    {

         //        SqlCommand cmd = new SqlCommand("st_getOrderDetailsWRTOrderID", Connection.con);
         //        cmd.CommandType = CommandType.StoredProcedure;
         //        cmd.Parameters.AddWithValue("@orderID", orderID);
         //        //Connection.con.Open();
         //        SqlDataAdapter da = new SqlDataAdapter(cmd);
         //        DataTable dt = new DataTable();
         //        da.Fill(dt);
         //        proidgv.DataPropertyName = dt.Columns["P.Code"].ToString();
         //        pronamgv.DataPropertyName = dt.Columns["P.Name"].ToString();
         //        pricegv.DataPropertyName = dt.Columns["Price"].ToString();
         //        quangv.DataPropertyName = dt.Columns["Quan"].ToString();
         //        totalgv.DataPropertyName = dt.Columns["Total"].ToString();

         //        gv.DataSource = dt;
         //        //Connection.con.Close();


         //    }
         //    catch (Exception ex)
         //    {

         //        MainClass.ShowMessage(ex.Message, "Error..", "Error");
         //        //Connection.con.Close();
         //    }

         //}
         public void getRunningOrderDetails(Int64 orderID, DataGridView gv, DataGridViewColumn proidgv, DataGridViewColumn pronamgv, DataGridViewColumn pricegv, DataGridViewColumn quangv, DataGridViewColumn totalgv)
         {
             try
             {

                 SqlCommand cmd = new SqlCommand("st_getOrderDetailsWRTOrderID", Connection.con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@orderID", orderID);
                 //Connection.con.Open();
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 proidgv.DataPropertyName = dt.Columns["P.Code"].ToString();
                 pronamgv.DataPropertyName = dt.Columns["P.Name"].ToString();
                 pricegv.DataPropertyName = dt.Columns["Price"].ToString();
                 quangv.DataPropertyName = dt.Columns["Quan"].ToString();
                 totalgv.DataPropertyName = dt.Columns["Total"].ToString();

                 gv.DataSource = dt;
                 //Connection.con.Close();


             }
             catch (Exception ex)
             {

                 MainClass.ShowMessage(ex.Message, "Error..", "Error");
                 //Connection.con.Close();
             }

         }
         //public void getRunningOrderDetails(Int64 orderID, DataGridView gv)
         //{
         //    try
         //    {

         //        SqlCommand cmd = new SqlCommand("st_getOrderDetailsWRTOrderIDghgh", Connection.con);
         //        cmd.CommandType = CommandType.StoredProcedure;
         //        cmd.Parameters.AddWithValue("@orderID", orderID);
         //        Connection.con.Open();
         //        SqlDataAdapter da = new SqlDataAdapter(cmd);
         //        DataSet dt = new DataSet();
         //        da.Fill(dt);
         //        gv.DataSource = dt;
             
         //        Connection.con.Close();


         //    }
         //    catch (Exception ex)
         //    {

         //        MainClass.ShowMessage(ex.Message, "Error..", "Error");
         //        Connection.con.Close();
         //    }

         //}


        //public void showkitchenReport(ReportDocument rd, CrystalReportViewer crv, string proc, string param1, object val1)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(proc, Connection.con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue(param1, val1);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        rd.Load(Application.StartupPath + "\\SaleReports\\kitchenreceipt.rpt");
        //        rd.SetDataSource(dt);
        //        crv.ReportSource = rd;
        //        crv.RefreshReport();

        //    }
        //    catch (Exception ex)
        //    {

        //        MainClass.ShowMessage(ex.Message, "Error", "Error");
        //    }
        //}

        public static  Int64  custID=0;
        public static Int64  getCustomerIDWRTPhone(string phone)
        {
            try 
	      {	        
		
                SqlCommand cmd = new SqlCommand("st_getCustomerIDWRTPhone", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@phone", phone);
                Connection.con.Open();
                custID = Convert.ToInt64(cmd.ExecuteScalar().ToString());

                Connection.con.Close();


	                  }
	                  catch (Exception ex)
	                 {

                            MainClass.ShowMessage(ex.Message, "Error..", "Error");
                            Connection.con.Close();
	}
          return  custID;
    }
        private static Boolean checkCustomerExistance;
        public static bool checkCustomer(string custPhone)
         {
             try
             {
                 SqlCommand cmd = new SqlCommand("st_checkCustomerID", Connection.con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@custPhone", custPhone);
                 Connection.con.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.HasRows)
                 {
                     checkCustomerExistance = true;
                 }
                 else
                 {
                     checkCustomerExistance = false;
                 }
                 Connection.con.Close();
             }
             catch (Exception)
             {

                 Connection.con.Close();
             }
             return checkCustomerExistance;
         }
         public void showRunningOrders(DataGridView gv, DataGridViewColumn orderiddGv, DataGridViewColumn ordertypeGv, DataGridViewColumn phonegv, DataGridViewColumn Addressgv, DataGridViewColumn namegv, DataGridViewColumn amountGv,DataGridViewColumn dategv,DataGridViewColumn  tablgv,DataGridViewColumn floorgv,DataGridViewColumn statusgv)
         {
             try
             {

                 SqlCommand cmd = new SqlCommand("st_getRunningorders", Connection.con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 orderiddGv.DataPropertyName = dt.Columns["Order Id"].ToString();
                 ordertypeGv.DataPropertyName = dt.Columns["OrderType"].ToString();
                 phonegv.DataPropertyName = dt.Columns["Phone"].ToString();
                 Addressgv.DataPropertyName = dt.Columns["Address"].ToString();
                 namegv.DataPropertyName = dt.Columns["Name"].ToString();
                 amountGv.DataPropertyName = dt.Columns["Amount"].ToString();
                 dategv.DataPropertyName = dt.Columns["Date"].ToString();
                 floorgv.DataPropertyName = dt.Columns["FloorName"].ToString();
                 tablgv.DataPropertyName = dt.Columns["Table Number"].ToString();
                 statusgv.DataPropertyName = dt.Columns["Status"].ToString();
                 gv.DataSource = dt;

             }
             catch (Exception ex)
             {
                 MainClass.ShowMessage(ex.Message, "Error", "Errorr");
             }
         }

         public void showRunningOrdersWRTOrderID(Int64 OrderID, DataGridView gv, DataGridViewColumn orderiddGv, DataGridViewColumn ordertypeGv, DataGridViewColumn phonegv, DataGridViewColumn Addressgv, DataGridViewColumn namegv, DataGridViewColumn dategv, DataGridViewColumn tablgv, DataGridViewColumn floorgv, DataGridViewColumn Itemgv,
                 DataGridViewColumn quangv,DataGridViewColumn rategv,DataGridViewColumn peritem,DataGridViewColumn statusgv)
         {
             try
             {

                 SqlCommand cmd = new SqlCommand("st_getRunningOrderWRTOIds", Connection.con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 cmd.Parameters.AddWithValue("@orderID", OrderID);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 orderiddGv.DataPropertyName = dt.Columns["OrderID"].ToString();
                 ordertypeGv.DataPropertyName = dt.Columns["OrderType"].ToString();
                 phonegv.DataPropertyName = dt.Columns["Phone"].ToString();
                 Addressgv.DataPropertyName = dt.Columns["Address"].ToString();
                 namegv.DataPropertyName = dt.Columns["CustomerName"].ToString();
                 dategv.DataPropertyName = dt.Columns["Date"].ToString();
                 floorgv.DataPropertyName = dt.Columns["Floor Name"].ToString();
                 tablgv.DataPropertyName = dt.Columns["Table Number"].ToString();
                 Itemgv.DataPropertyName = dt.Columns["Item"].ToString();
                 quangv.DataPropertyName = dt.Columns["Quantity"].ToString();
                 rategv.DataPropertyName = dt.Columns["Rate"].ToString();
                 peritem.DataPropertyName = dt.Columns["Per Item Price"].ToString();
                 statusgv.DataPropertyName = dt.Columns["Status"].ToString();
                 gv.DataSource = dt;

             }
             catch (Exception ex)
             {
                 MainClass.ShowMessage(ex.Message, "Error", "Errorr");
             }
         }

        public void showMenuItem(DataGridView gv, DataGridViewColumn menuidGv, DataGridViewColumn menunameGv,DataGridViewColumn pricegv ,DataGridViewColumn catidgv,DataGridViewColumn catenamegv,DataGridViewColumn statusGv)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("st_getMenu", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                menuidGv.DataPropertyName = dt.Columns["MenuID"].ToString();
                menunameGv.DataPropertyName = dt.Columns["Menu Item"].ToString();
                statusGv.DataPropertyName = dt.Columns["Status"].ToString();
                catidgv.DataPropertyName = dt.Columns["Category ID"].ToString();
                catenamegv.DataPropertyName = dt.Columns["Category Name"].ToString();
                pricegv.DataPropertyName = dt.Columns["Price"].ToString();
                gv.DataSource = dt;

            }
            catch (Exception ex)
            {
                MainClass.ShowMessage(ex.Message, "Error", "Errorr");
            }
        }
    

        public void showCategories(DataGridView gv, DataGridViewColumn cateidGv, DataGridViewColumn catenameGv, DataGridViewColumn statusGv)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("st_getCategoriesData", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cateidGv.DataPropertyName = dt.Columns["ID"].ToString();
                catenameGv.DataPropertyName = dt.Columns["Category"].ToString();
                statusGv.DataPropertyName = dt.Columns["Status"].ToString();
                gv.DataSource = dt;

            }
            catch (Exception ex)
            {
                MainClass.ShowMessage(ex.Message, "Error", "Errorr");
            }
        }
             public static  Int64 orderID ;
             public static Int64 getlastorderID()
            {
            
            try
            {

                SqlCommand cmd = new SqlCommand("st_getlastorderid", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                Connection.con.Open();
                orderID = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                Connection.con.Close();

            }
            catch (Exception ex)
            {
                MainClass.ShowMessage(ex.Message, "Error", "Errorr");
            }
            return orderID;

        }
    
        public void getRoles(DataGridView gv, DataGridViewColumn roleIDGv, DataGridViewColumn rolenameGv)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("st_getRoles", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                roleIDGv.DataPropertyName = dt.Columns["RolesID"].ToString();
                rolenameGv.DataPropertyName = dt.Columns["Roles"].ToString();
                gv.DataSource = dt;

            }
            catch (Exception)
            {
                MainClass.ShowMessage("Unable to load Roles...", "Error", "Errorr");
            }
        }
      
        public  void getList(string proc, ComboBox cb, string displayeMember, string valueMember,string param=null,int val=0)
        {
            
            try
            {
                 
                   SqlCommand cmd = new SqlCommand(proc, Connection.con);
                    cmd.CommandType = CommandType.StoredProcedure;
               
               
                if(param==null && val==0)
                {

                  
                }
                else
                {
                    cmd.Parameters.AddWithValue(param,val);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cb.DisplayMember = displayeMember;
                cb.ValueMember = valueMember;
                cb.DataSource = dt;

            }
            catch (Exception ex)
            {

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }

        }
        public void getListOfRunningIDs(string proc, ComboBox cb, string displayeMembe)
        {

            try
            {

                SqlCommand cmd = new SqlCommand(proc, Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cb.DisplayMember = displayeMembe;
                cb.DataSource = dt;

            }
            catch (Exception ex)
            {

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }

        }
        public void getListListbox(string proc,ListBox cb, string displayeMember, string valueMember, string param = null, int val = 0)
        {

            try
            {

                SqlCommand cmd = new SqlCommand(proc, Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;


                if (param == null && val == 0)
                {


                }
                else
                {
                    cmd.Parameters.AddWithValue(param, val);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cb.DisplayMember = displayeMember;
                cb.ValueMember = valueMember;
                cb.DataSource = dt;

            }
            catch (Exception ex)
            {

                MainClass.ShowMessage(ex.Message, "Error", "Error");
            }

        }
       
       


       public void showUsers(DataGridView gv, DataGridViewColumn useridGv, DataGridViewColumn nameGv, DataGridViewColumn usernameGv,
        DataGridViewColumn passwordGv, DataGridViewColumn phoneGv, DataGridViewColumn emailGv, DataGridViewColumn roleidgv,DataGridViewColumn rolegv)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("st_getUsersData", Connection.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                useridGv.DataPropertyName = dt.Columns["ID"].ToString();
                nameGv.DataPropertyName = dt.Columns["Name"].ToString();
                usernameGv.DataPropertyName = dt.Columns["UserName"].ToString();
                passwordGv.DataPropertyName = dt.Columns["Password"].ToString();
                phoneGv.DataPropertyName = dt.Columns["Phone"].ToString();
                emailGv.DataPropertyName = dt.Columns["Email"].ToString();
                roleidgv.DataPropertyName = dt.Columns["RoleID"].ToString();
                rolegv.DataPropertyName = dt.Columns["Role"].ToString();
                gv.DataSource = dt;

            }
            catch (Exception)
            {
                MainClass.ShowMessage("Unable to load Roles...", "Error", "Errorr");
            }
        }
       public void showCustomers(DataGridView gv, DataGridViewColumn customeridGv, DataGridViewColumn nameGv,
       DataGridViewColumn phoneGv, DataGridViewColumn adressGv, string data = null)
       {
           try
           {
               SqlCommand cmd;


               if (data == null)
               {
                   cmd = new SqlCommand("st_getCustomers", Connection.con);
               }
               else
               {
                   cmd = new SqlCommand("st_getCustomersDataLike", Connection.con);
                   cmd.Parameters.AddWithValue("@data", data);
               }
               cmd = new SqlCommand("st_getCustomers", Connection.con);
               cmd.CommandType = CommandType.StoredProcedure;
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               customeridGv.DataPropertyName = dt.Columns["ID"].ToString();
               nameGv.DataPropertyName = dt.Columns["Name"].ToString();
               adressGv.DataPropertyName = dt.Columns["address"].ToString();
               phoneGv.DataPropertyName = dt.Columns["Phone"].ToString();
               gv.DataSource = dt;

           }
           catch (Exception ex)
           {
               MainClass.ShowMessage(ex.Message, "Error", "Errorr");
           }
       }
       public void showFloors(DataGridView gv, DataGridViewColumn flooridGv, DataGridViewColumn floornameGv,
     DataGridViewColumn floornumbergv)
       {
           try
           {
               SqlCommand cmd;
               cmd = new SqlCommand("st_getfloors", Connection.con);
               cmd.CommandType = CommandType.StoredProcedure;
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               flooridGv.DataPropertyName = dt.Columns["ID"].ToString();
               floornameGv.DataPropertyName = dt.Columns["name"].ToString();
               floornumbergv.DataPropertyName = dt.Columns["number"].ToString();
               
               gv.DataSource = dt;

           }
           catch (Exception ex)
           {
               MainClass.ShowMessage(ex.Message, "Error", "Errorr");
           }
       }
       public void showtables(DataGridView gv, DataGridViewColumn flooridGv, DataGridViewColumn floornameGv,
       DataGridViewColumn tableidgv,DataGridViewColumn tablenamegv,DataGridViewColumn chairsgv)
       {
           try
           {
               SqlCommand cmd;
               cmd = new SqlCommand("st_gettables", Connection.con);
               cmd.CommandType = CommandType.StoredProcedure;
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               da.Fill(dt);
               flooridGv.DataPropertyName = dt.Columns["floorid"].ToString();
               floornameGv.DataPropertyName = dt.Columns["floorname"].ToString();
               tableidgv.DataPropertyName = dt.Columns["tableID"].ToString();
               tablenamegv.DataPropertyName = dt.Columns["tablenumber"].ToString();
               chairsgv.DataPropertyName = dt.Columns["Chairs"].ToString();

               gv.DataSource = dt;

           }
           catch (Exception ex)
           {
               MainClass.ShowMessage(ex.Message, "Error", "Errorr");
           }
       }
       public static int USER_ID   // create for login
       {
           get;
           private set;   // private means use only in this class
       }
       public static string EMP_NAME   // for login of employ
       {
           get;
           private set;
       }
       private static string role;
       private static string user_name, pass_word;
       private static Boolean CheckLogin;
        public static string Role
       {
           get
           {
               return role;
           }
         private  set
           {
               role = value;
           }
       }
       public static bool getUserDetails(string username, string password)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("st_getUserDetail", Connection.con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@user", username);
               cmd.Parameters.AddWithValue("@pass", password);
               Connection.con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {
                   CheckLogin = true;
                   while (dr.Read())
                   {
                       USER_ID = Convert.ToInt32(dr["ID"].ToString());
                       EMP_NAME = dr["Name"].ToString();
                       user_name = dr["UserName"].ToString();
                       pass_word = dr["Password"].ToString();
                       Role = dr["Role"].ToString();

                   }
               }
               else
               {
                   CheckLogin = false;
                   if (username != null && password != null)
                   {
                       if (user_name != username && pass_word == password)
                       {
                           MainClass.ShowMessage(" Sorry Invalid Username..", "Error", "Error");
                       }
                       if (user_name == username && pass_word != password)
                       {
                           MainClass.ShowMessage(" Sorry Invalid Password..", "Error", "Error");
                       }
                       else
                       {
                           MainClass.ShowMessage(" Sorry Invalid  Username & Password ..", "Error", "Error");
                       }

                   }

               }

               Connection.con.Close();
           }
           catch (Exception)
           {

               Connection.con.Close();
               MainClass.ShowMessage(EMP_NAME + " Unable to login", "Error", "Error");
           }
           return CheckLogin;
       }

    }
}
