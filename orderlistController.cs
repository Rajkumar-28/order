using OrderList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderList.Controllers
{
    public class orderlistController : ApiController
    {

        // customer // CreateCustomer
        [HttpPost]
        [Route("api/CreateCustomer")]
        public string CreateCustomer(customer obj)
        {


            Item Item = new Item();

            SqlConnection con = null;
            try
            {
               con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("proc_Create_Customer", con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@FirtsName", obj.FirtsName));

                sqlCommand.Parameters.Add(new SqlParameter("@LastName", obj.LastName));

                sqlCommand.Parameters.Add(new SqlParameter("@Email", obj.Email));

                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNo", obj.PhoneNo));

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("please try again" + ex.Message);
                return "Alreedy exist";
            }

            return "inserted";

        }

                     //GetCustomers
        [HttpGet]
        [Route("api/GetCustomers")]
        public List<customer> Customers()
        {
            List<customer> customerlist = new List<customer>();
            customer customer = new customer();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from CustomerDetails;", con);


                SqlDataReader DataReader = sqlCommand.ExecuteReader();

                while (DataReader.Read())
                {
                    customer = new customer();

                    customer.FirtsName = DataReader["FirtsName"].ToString();
                    customer.LastName = DataReader["LastName"].ToString();
                    customer.Email = (string)DataReader["Email"];
                    customer.PhoneNo = (long)DataReader["PhoneNo"];
                    customerlist.Add(customer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("please try again" + ex.Message);
            }
            return customerlist;
        }

                         //LoginCheckCustomers
        [HttpGet]
        [Route("api/LoginCheckCustomers")]
        public HttpResponseMessage LoginCheckCustomers(string password, string email)
        {
            List<customer> customerlist = new List<customer>();
            customer customer = new customer();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("proc_login_check", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@password", password));

                sqlCommand.Parameters.Add(new SqlParameter("@Email", email));
                SqlDataReader DataReader = sqlCommand.ExecuteReader();

                while (DataReader.Read())
                {


                    customer.FirtsName = DataReader["FirtsName"].ToString();
                    customer.LastName = DataReader["LastName"].ToString();
                    customer.Email = (string)DataReader["Email"];
                    customerlist.Add(customer);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("please try again" + ex.Message);

            }

            if (customerlist.Contains(customer))
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Login success");
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.Unauthorized, "incorrect credentials");
            }
        }


        // Items   //

        [HttpGet]
        [Route("api/AllItems")]
        public List<Items> AllItems()
        {
            List<Items> Itemlist = new List<Items>();
            Items Item = new Items();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("proc_Get_ItemDetails", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader DataReader = sqlCommand.ExecuteReader();

                while (DataReader.Read())
                {
                    Item = new Items();
                    Item.ItemId = (int)DataReader["ItemId"];
                    Item.ItemName = DataReader["ItemName"].ToString();
                    Item.ItemInformation = DataReader["ItemInformation"].ToString();
                    Item.CostOfItem =(int)DataReader["CostOfItem"];
                    Itemlist.Add(Item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("please try again" + ex.Message);
            }
            return Itemlist;
        }

               //Item
        [HttpGet]
        [Route("api/Item")]
        public List<Items> Item( int id)
        {
            List<Items> Itemlist = new List<Items>();
            Items Item;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("proc_single_item", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@ItemId", id));
                SqlDataReader DataReader = sqlCommand.ExecuteReader();

                while (DataReader.Read())
                {
                    Item = new Items();
                    Item.ItemId = (int)DataReader["ItemId"];
                    Item.ItemName = DataReader["ItemName"].ToString();
                    Item.ItemInformation = DataReader["ItemInformation"].ToString();
                    Item.CostOfItem = (int)DataReader["CostOfItem"];
                    Itemlist.Add(Item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("please try again" + ex.Message);
            }
            return Itemlist;
        }

        //OrderList

        [HttpPost]
        [Route("api/CreateOrder")]
        public HttpResponseMessage CreateOrder(Order obj)
        {


            Item Item = new Item();

            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("proc_Create_Order", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerId", obj.CustomerId));

                sqlCommand.Parameters.Add(new SqlParameter("@CustomerName", obj.CustomerName));

                sqlCommand.Parameters.Add(new SqlParameter("@ItemName", obj.ItemName));

                sqlCommand.Parameters.Add(new SqlParameter("@ItemInformation", obj.ItemInformation));

                sqlCommand.Parameters.Add(new SqlParameter("@CostOfItem", obj.CostOfItem));

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("please try again" + ex.Message);

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Order Not Placed");

            }


            return Request.CreateResponse(HttpStatusCode.OK, "Order Submitted"); 

        }

        //MyOrder

        [HttpGet]
        [Route("api/MyOrder")]
        public List<Order> MyOrder(int id)
        {
            List<Order> Itemlist = new List<Order>();
            Order order = new Order();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=OrderList; integrated security=SSPI");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("pro_MyOrder", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@CustomerId", id));

                SqlDataReader DataReader = sqlCommand.ExecuteReader();
                while (DataReader.Read())
                {
                    order = new Order();
                    order.CustomerId = (int)DataReader["CustomerId"];
                    order.FirtsName = DataReader["FirtsName"].ToString();
                    order.ItemName = DataReader["ItemName"].ToString();
                    order.ItemInformation = (string)DataReader["ItemInformation"];
                    Itemlist.Add(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No orders" + ex.Message);
                
            }
            return Itemlist;
        }






        // GET: api/orderlist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/orderlist/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/orderlist
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/orderlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/orderlist/5
        public void Delete(int id)
        {
        }
    }
}
