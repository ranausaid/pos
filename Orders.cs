using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Transactions;


namespace RMS
{
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }
        Regex rg = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        Retrieval rt = new Retrieval();
        Updation up = new Updation();
        Insertion i = new Insertion();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Orders_Load(object sender, EventArgs e)
        {
            DatagridviewRunning.AutoGenerateColumns = false;
            datagridviewtaking.AutoGenerateColumns = false;
            Customerdatagridview.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;

            
            rt.showCustomers(Customerdatagridview, CustomeridGv, CustomerGv, phoneGv, AddressGv);
            rt.getList("st_getMenu", ddItemName, "Menu Item", "MenuID");
            //rt.getListListbox("st_getMenu",itemListbox, "Menu Item", "MenuID");
            ddItemName.SelectedIndex = -1;
            rt.getList("st_getfloors", ddFloorNo, "name", "ID");
            rt.showRunningOrders(DatagridviewRunning, Invoicegv, OrderTypegv, phoneNmbergv, Customeradressgv, Customernamegv, netbillgv, ordertimegv, Tablenumbergv, FloornumberGv, statusgv);
            ddFloorNo.SelectedIndex = 0;
            ddtableNo.SelectedIndex = 0;
            txtRate.Text = "0";
            ddOrderType.SelectedIndex = 0;

        }


        //Int64 custID;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ddItemName_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtQuantity.Clear();
            txtRate.Clear();
            txtTotalAmount.Clear();

        }

        private void ddFloorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddFloorNo.SelectedIndex != -1)
            {
                rt.getList("st_gettablesWRTFloors", ddtableNo, "tablenumber", "tableID", "@floorID", Convert.ToInt32(ddFloorNo.SelectedValue.ToString()));
                ddtableNo.SelectedIndex = 0;
            }
        }

        private void ddOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddOrderType.SelectedIndex != -1)
            {
                if (ddOrderType.SelectedIndex == 0)
                {
                    ddFloorNo.Enabled = true;
                    ddtableNo.Enabled = true;
                    ddFloorNo.SelectedIndex = 0;
                    ddtableNo.SelectedIndex = 0;
                    panel3.Enabled = false;
                    txtname.Clear();
                    txtphone.Clear(); txtaddress.Clear();
                    ddFloorNo.BackColor = Color.Lime;
                    ddtableNo.BackColor = Color.Lime;
                    lblfloor.ForeColor = Color.White;
                    lbltable.ForeColor = Color.White;
                    

                }
                else
                {
                    panel3.Enabled = true;
                    ddFloorNo.Enabled = false;
                    ddtableNo.Enabled = false;
                    ddFloorNo.BackColor = Color.White;
                    ddtableNo.BackColor = Color.White;
                    lblfloor.ForeColor = Color.Black;
                    lbltable.ForeColor = Color.Black;
                    ddFloorNo.SelectedIndex = -1;
                    ddtableNo.SelectedIndex = -1;
                }



               if (ddOrderType.SelectedIndex ==0 )
               {
                   ddtableNo.SelectedIndex = 0;
                   ddFloorNo.SelectedIndex = 0;
               }
            }
        }

        //private void Insertorderdetails()
        //{
        //    Int64 orderID = rt.getlastorderID();
        //    int count = 0;

        //    foreach (DataGridViewRow row in datagridviewtaking.Rows)
        //    {
        //        count += i.insertOrderDetail(orderID, Convert.ToInt32(row.Cells["ItemIdGv"].Value.ToString()),Convert.ToInt16(row.Cells["quantitygv"].Value.ToString()));
        //    }
        //    if (count > 0)
        //    {
        //        MainClass.ShowMessage("Order Placed", "Succes..", "Success");
        //    }
        //    else
        //    {
        //        //MainClass.ShowMessage("No order places", "Error..", "Error");
        //    }
        //}
           private void Insertorderdetails(Int64 orderID,DataGridView gv)
        {
            int count = 0;

            foreach (DataGridViewRow row in datagridviewtaking.Rows)
            {
                count += i.insertOrderDetail(orderID, Convert.ToInt32(row.Cells["ItemIdGv"].Value.ToString()), Convert.ToInt16(row.Cells["quantitygv"].Value.ToString()));
            }
            if (count > 0)
            {
                MainClass.ShowMessage("Order Placed", "Succes..", "Success");
            }
            else
            {
            }
        }
        private void btnCustomer_Click_1(object sender, EventArgs e)
        {
           
        }

        float  GROSS=0;
        private void btnCart_Click(object sender, EventArgs e)
        {
            DataRowView drItem = ddItemName.SelectedItem as DataRowView;

            try
            {
                if (ddOrderType.SelectedIndex == 0)
                {

                    if (ddItemName.SelectedIndex != -1 && txtRate.Text != ""
                       && txtQuantity.Text != "" && txtTotalAmount.Text != "")
                    {
                        datagridviewtaking.Rows.Add(Convert.ToInt32(ddItemName.SelectedValue.ToString()), drItem["Menu Item"], Convert.ToSingle(txtRate.Text), Convert.ToInt32(txtQuantity.Text), Convert.ToSingle(txtTotalAmount.Text));
                    }
                }
                else
                {
                    if (ddItemName.SelectedIndex != -1 && txtRate.Text != "" && txtQuantity.Text != "" && txtTotalAmount.Text != "" && txtname.Text != "" && txtphone.Text != "" && txtaddress.Text != "")
                    {
                        //foreach (DataGridViewRow row in Customerdatagridview.Rows)
                        //{
                        //    if (Retrieval.checkCustomer((row.Cells["phoneGv"].Value.ToString())))
                        //    {
                        //        up.updateCustomers(Retrieval.custID, txtname.Text, txtaddress.Text, txtphone.Text);

                        //        //MainClass.ShowMessage("Added", "success", "Success");
                        //    }
                        //    else
                        //    {

                        i.insertCustomer(txtname.Text, txtaddress.Text, txtphone.Text);
                        rt.showCustomers(Customerdatagridview, CustomeridGv, CustomerGv, phoneGv, AddressGv);
                        Retrieval.getCustomerIDWRTPhone(txtphone.Text);
                        MessageBox.Show(Retrieval.custID.ToString());
                        //}
                        //}
                    }
                    datagridviewtaking.Rows.Add(
                 Convert.ToInt32(ddItemName.SelectedValue.ToString()), drItem["Menu Item"],
                 Convert.ToSingle(txtRate.Text), Convert.ToInt32(txtQuantity.Text),
                 Convert.ToSingle(txtTotalAmount.Text));
                }
                foreach (DataGridViewRow item in datagridviewtaking.Rows)
                {
                    GROSS += Convert.ToSingle(item.Cells["Totalgv"].Value.ToString());
                }
                txtBillAmount.Text = Math.Ceiling(GROSS).ToString();
                GROSS = 0;

                ddItemName.SelectedIndex = -1;
                txtRate.Clear();
                txtQuantity.Clear(); txtTotalAmount.Clear(); ddItemName.Focus();

            }
            catch (Exception ex)
            {
                MainClass.ShowMessage(ex.Message, "Error..", "Error");

            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    if (rg.Match(txtQuantity.Text).Success)
                    {
                        float quan, price, total;
                        quan = Convert.ToSingle(txtQuantity.Text);
                        price = Convert.ToSingle(txtRate.Text);
                        total = quan * price;
                        txtTotalAmount.Text = total.ToString("#######.##");
                    }
                    else
                    {
                        txtQuantity.Text = "";
                        txtQuantity.Focus();
                    }
                }

                else
                {
                    txtTotalAmount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MainClass.ShowMessage(ex.Message, "Error..", "Error");

            }
        }

        private void datagridviewtaking_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex !=-1 && e.ColumnIndex  !=-1)
            {
                if(e.ColumnIndex==5)
                {
                    DataGridViewRow row = datagridviewtaking.Rows[e.RowIndex];
                    float prc = Convert.ToSingle(row.Cells["Totalgv"].Value.ToString());
                    GROSS = Convert.ToSingle(txtBillAmount.Text);
                    GROSS =GROSS- prc;
                    txtBillAmount.Text = GROSS.ToString();
                    datagridviewtaking.Rows.Remove(row);

                }
            }
        }

        private void txtphone_Leave(object sender, EventArgs e)
        {
           
        }
        private void DatagridviewRunning_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if(e.ColumnIndex ==0)
                {
                    DialogResult dr = MessageBox.Show("Are You Sure to Done Order..", "Question ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                   if (dr==DialogResult.Yes)
                    {
                        Update ups = new Update();
                        ups.ShowDialog();}}

                       if(e.ColumnIndex ==1)
                       {
                           POS pos = new POS();
                           pos.ShowDialog();
                       }
                        //rt.showRunningOrders(DatagridviewRunning, Invoicegv, OrderTypegv, phoneNmbergv, Customeradressgv, Customernamegv, netbillgv, ordertimegv, Tablenumbergv, FloornumberGv, statusgv);


                    }
                }
            

        private void DatagridviewRunning_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void DatagridviewRunning_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {



                DataGridViewRow row = DatagridviewRunning.Rows[e.RowIndex];
                txtorder.Text = (row.Cells["Invoicegv"].Value.ToString());
                //rt.getRunningOrderDetails(Convert.ToInt64(txtorder.Text), datagridviewtaking);
                rt.getRunningOrderDetails(Convert.ToInt64(txtorder.Text), datagridviewtaking, ItemIdGv, ItemNamegv, pricegv, quantitygv, Totalgv);



                foreach (DataGridViewRow item in datagridviewtaking.Rows)
                {
                    GROSS += Convert.ToSingle(item.Cells["Totalgv"].Value.ToString());
                }
                txtBillAmount.Text = Math.Ceiling(GROSS).ToString();
                GROSS = 0;

            }
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    if (ddOrderType.SelectedIndex == 0)
                    {
                        i.insertOrder(DateTime.Now, 1, Convert.ToInt16(ddOrderType.SelectedIndex.ToString()), Convert.ToInt16(ddFloorNo.SelectedValue.ToString()), Convert.ToInt16(ddtableNo.SelectedValue.ToString()), Convert.ToSingle(txtBillAmount.Text), 0, 0, 0);
                        Insertorderdetails(Retrieval.getlastorderID(),DatagridviewRunning);
                        rt.showRunningOrders(DatagridviewRunning, Invoicegv, OrderTypegv, phoneNmbergv, Customeradressgv, Customernamegv, netbillgv, ordertimegv, Tablenumbergv, FloornumberGv,statusgv);
                    }
                  
                    else
                    {
                        i.insertOrder(DateTime.Now, Retrieval.custID, Convert.ToInt16(ddOrderType.SelectedIndex.ToString()), 0, 0, Convert.ToSingle(txtBillAmount.Text), 0, 0, 0);
                        Insertorderdetails(Retrieval.getlastorderID(),DatagridviewRunning);
                        rt.showRunningOrders(DatagridviewRunning, Invoicegv, OrderTypegv, phoneNmbergv, Customeradressgv, Customernamegv, netbillgv, ordertimegv, Tablenumbergv, FloornumberGv, statusgv);
                       
                    }


                    KitchenReceiptF kh = new KitchenReceiptF();
                    kh.Show();
                    //SaleReport sr = new SaleReport();
                    //sr.Show();
                }
                catch (Exception)
                {

                    throw;
                }

                sc.Complete();
               
                datagridviewtaking.Rows.Clear();
                txtBillAmount.Clear();
                ddItemName.Focus();

            }
        }

        private void btnPrinOnly_Click(object sender, EventArgs e)
        {
            //using (TransactionScope sc = new TransactionScope())
            //{

            //    try
            //    {
            //        Insertorderdetails(Convert.ToInt64(txtorder.Text),DatagridviewRunning);
            //        //KitchenForm kh = new KitchenForm();
            //        //kh.Show();

                  SaleReport sr = new SaleReport();
                   sr.Show();

            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }

            //    sc.Complete();
            //    orderID = 0;


            //    datagridviewtaking.Rows.Clear();
               

            //}

        }

        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            datagridviewtaking.Rows.Clear();
            txtname.Clear(); txtaddress.Clear(); txtphone.Clear(); txtBillAmount.Clear(); ddOrderType.Focus();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            datagridviewtaking.Rows.Clear();
            txtname.Clear(); txtaddress.Clear(); txtphone.Clear(); txtBillAmount.Clear(); ddOrderType.Focus();
        }

        private void btnPaidandPrint_Click(object sender, EventArgs e)
        {
            //POS bill = new POS();
            //bill.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ddItemName_Validating(object sender, CancelEventArgs e)
        {

            if (ddItemName.SelectedIndex != -1)
            {
                try
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("st_getPriceWRTItem", Connection.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mid", Convert.ToInt32(ddItemName.SelectedValue.ToString()));
                    Connection.con.Open();
                    txtRate.Text = cmd.ExecuteScalar().ToString();
                    Connection.con.Close();
                    cmd.Dispose();
                    ddItemName.Refresh();

                    txtQuantity.Text = "1";
                    txtQuantity.Focus();
                }
                catch (Exception ex)
                {
                    MainClass.ShowMessage(ex.Message, "Error", "Errorr");
                    Connection.con.Close();
                }

            }
            else
            {
                txtRate.Text = "";
            }
            

        }

        private void txtQuantity_Click(object sender, EventArgs e)
        {
            DataRowView drItem = ddItemName.SelectedItem as DataRowView;

            try
            {
                if (ddOrderType.SelectedIndex == 0)
                {

                    if (ddItemName.SelectedIndex != -1 && txtRate.Text != ""
                       && txtQuantity.Text != "" && txtTotalAmount.Text != "")
                    {
                        datagridviewtaking.Rows.Add(Convert.ToInt32(ddItemName.SelectedValue.ToString()), drItem["Menu Item"], Convert.ToSingle(txtRate.Text), Convert.ToInt32(txtQuantity.Text), Convert.ToSingle(txtTotalAmount.Text));
                    }
                }
                else
                {
                    if (ddItemName.SelectedIndex != -1 && txtRate.Text != "" && txtQuantity.Text != "" && txtTotalAmount.Text != "" && txtname.Text != "" && txtphone.Text != "" && txtaddress.Text != "")
                    {
                        //foreach (DataGridViewRow row in Customerdatagridview.Rows)
                        //{
                        //    if (Retrieval.checkCustomer((row.Cells["phoneGv"].Value.ToString())))
                        //    {
                        //        up.updateCustomers(Retrieval.custID, txtname.Text, txtaddress.Text, txtphone.Text);

                        //        //MainClass.ShowMessage("Added", "success", "Success");
                        //    }
                        //    else
                        //    {

                        i.insertCustomer(txtname.Text, txtaddress.Text, txtphone.Text);
                        rt.showCustomers(Customerdatagridview, CustomeridGv, CustomerGv, phoneGv, AddressGv);
                        Retrieval.getCustomerIDWRTPhone(txtphone.Text);
                        MessageBox.Show(Retrieval.custID.ToString());
                        //}
                        //}
                    }
                    datagridviewtaking.Rows.Add(
                 Convert.ToInt32(ddItemName.SelectedValue.ToString()), drItem["Menu Item"],
                 Convert.ToSingle(txtRate.Text), Convert.ToInt32(txtQuantity.Text),
                 Convert.ToSingle(txtTotalAmount.Text));
                }
                foreach (DataGridViewRow item in datagridviewtaking.Rows)
                {
                    GROSS += Convert.ToSingle(item.Cells["Totalgv"].Value.ToString());
                }
                txtBillAmount.Text = Math.Ceiling(GROSS).ToString();
                GROSS = 0;

                ddItemName.SelectedIndex = -1;
                txtRate.Clear();
                txtQuantity.Clear(); txtTotalAmount.Clear(); ddItemName.Focus();

            }
            catch (Exception ex)
            {
                MainClass.ShowMessage(ex.Message, "Error..", "Error");

            }
        }

    }
}

            

        

       

       
      
    
