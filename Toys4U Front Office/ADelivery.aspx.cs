using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toys4U_Classes;

namespace Toys4U_Front_Office
{
    public partial class ADelivery : System.Web.UI.Page
    {
        //variable to store the primary key with page level scope
        Int32 DeliveryID;

        //event handler for the page load event
        protected void Page_Load(object sender, EventArgs e)
        {

            //get the number of the delivery to be processed
            DeliveryID = Convert.ToInt32(Session["DeliveryID"]);

            if (IsPostBack == false)
            {
                //if this is not a new record
                if (DeliveryID != -1)
                {
                    //display the current data for the record
                    DisplayDelivery();
                }
            }
        }

        void DisplayDelivery()
        {
            //create an instance of deliveries
            clsDeliveryCollection MyDeliveries = new clsDeliveryCollection();
            //find the record to update
            MyDeliveries.ThisDelivery.Find(DeliveryID);
            //display the data for this record
            txtOrderID.Text = Convert.ToString(MyDeliveries.ThisDelivery.OrderID);
            txtHouseNo.Text = MyDeliveries.ThisDelivery.HouseNo;
            txtStreet.Text = MyDeliveries.ThisDelivery.Street;
            txtTown.Text = MyDeliveries.ThisDelivery.Town;
            txtCity.Text = MyDeliveries.ThisDelivery.City;
            txtPostcode.Text = MyDeliveries.ThisDelivery.Postcode;
            txtDateAdded.Text = MyDeliveries.ThisDelivery.DateAdded.ToString("yyyy-MM-dd");
            txtDateEstimated.Text = MyDeliveries.ThisDelivery.DateEstimated.ToString("yyyy-MM-dd");
        }

        //function for adding new records
        void Add()
        {
            //create an instance of a clsDeliveryCollection
            clsDeliveryCollection MyDeliveries = new clsDeliveryCollection();
            //validate the data on the web form
            lblError.Text = "";
            String Error = MyDeliveries.ThisDelivery.Valid(txtOrderID.Text, txtHouseNo.Text, txtStreet.Text, txtTown.Text, txtCity.Text, txtPostcode.Text, txtDateAdded.Text, txtDateEstimated.Text);
            if (Error == "")
            {
                //get the data entered by the user
                MyDeliveries.ThisDelivery.OrderID = Convert.ToInt32(txtOrderID.Text);
                MyDeliveries.ThisDelivery.HouseNo = txtHouseNo.Text;
                MyDeliveries.ThisDelivery.Street = txtStreet.Text;
                MyDeliveries.ThisDelivery.Town = txtTown.Text;
                MyDeliveries.ThisDelivery.City = txtCity.Text;
                MyDeliveries.ThisDelivery.Postcode = txtPostcode.Text;
                MyDeliveries.ThisDelivery.DateAdded = Convert.ToDateTime(txtDateAdded.Text);
                MyDeliveries.ThisDelivery.DateAdded.ToString("dd/MM/yyyy");
                MyDeliveries.ThisDelivery.DateEstimated = Convert.ToDateTime(txtDateEstimated.Text);
                MyDeliveries.ThisDelivery.DateEstimated.ToString("dd/MM/yyyy");
                //add the record
                MyDeliveries.Add();
            }
            else
            {
                //report an error
                lblError.Text = "There were problems with the data entered: " + Error;
            }
        }

        //function for updating records
        void Update()
        {
            //create an instance of the deliveries
            clsDeliveryCollection Deliveries = new clsDeliveryCollection();
            //validate the data on the web form
            lblError.Text = "";
            String Error = Deliveries.ThisDelivery.Valid(txtOrderID.Text, txtHouseNo.Text, txtStreet.Text, txtTown.Text, txtCity.Text, txtPostcode.Text, txtDateAdded.Text, txtDateEstimated.Text);
            // if the data is ok then add it to the object
            if (Error == "")
            {
                //find the record to update
                Deliveries.ThisDelivery.Find(DeliveryID);
                //get the data entered by the user
                Deliveries.ThisDelivery.OrderID = Convert.ToInt32(txtOrderID.Text);
                Deliveries.ThisDelivery.HouseNo = txtHouseNo.Text;
                Deliveries.ThisDelivery.Street = txtStreet.Text;
                Deliveries.ThisDelivery.Town = txtTown.Text;
                Deliveries.ThisDelivery.City = txtCity.Text;
                Deliveries.ThisDelivery.Postcode = txtPostcode.Text;
                Deliveries.ThisDelivery.DateAdded = Convert.ToDateTime(txtDateAdded.Text);
                Deliveries.ThisDelivery.DateAdded.ToString("dd/MM/yyyy");
                Deliveries.ThisDelivery.DateEstimated = Convert.ToDateTime(txtDateEstimated.Text);
                Deliveries.ThisDelivery.DateEstimated.ToString("dd/MM/yyyy");
                //update the record
                Deliveries.Update();
                //all done so redirect back to the main page
                Response.Redirect("DeliveryList.aspx");
            }
            else
            {
                //report an error
                lblError.Text = "There were problems with the data entered: " + Error;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (DeliveryID == -1)
            {
                //add the new record
                Add();

            }
            else
            {
                //update the record
                Update();
            }
            //redirect to the viewer page
            if (lblError.Text == "")
            {
                Response.Redirect("DeliveryList.aspx");
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryList.aspx");
        }
    }
}