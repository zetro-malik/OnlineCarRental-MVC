using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using OnlineCarRental.Models;

namespace OnlineCarRental.Controllers
{
    public class inupController : Controller
    {
        static string q = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=rentalCar;Data Source=ZETRO\\MSSQLSERVER01";
        SqlConnection con=new SqlConnection(q);
        
        public ActionResult ShopSignup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShopSignup(Shop obj)
        {
            con.Open();
            String query = "insert into shop values('" + obj.Name + "','" + obj.address + "','" + obj.phone + "','" + obj.email + "','" + obj.password + "')";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ShopLogin");
        }


        public ActionResult ShopLogin()
        {
            Shop a = new Shop();
            HttpCookie ck = Request.Cookies["shopDATA"];
            if (ck != null)
            {

                a.email = ck["email"].ToString();
                a.password = ck["password"].ToString();
            }
            return View(a);
        }
        [HttpPost]

        public ActionResult ShopLogin(Shop obj)
        {
            con.Open();

            String query = "select * from shop where email='" + obj.email + "' and password='" + obj.password + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                Session["shopID"] = sdr["sid"].ToString();
                HttpCookie cook = new HttpCookie("shopDATA");
                cook["email"] = obj.email;
                cook["password"] = obj.password;
                Response.Cookies.Add(cook);
                cook.Expires = DateTime.Now.AddDays(2);


                return RedirectToAction("Home","Shop");
            }
            con.Close();
            return View();
        }







        public ActionResult CustomerSignup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerSignup(Customer obj)
        {
            con.Open();
            String query = "insert into customer values('" + obj.Name + "','" + obj.phone + "','" + obj.email + "','" + obj.password + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("CustomerLogin");
        }
        




        public ActionResult CustomerLogin()
        {
            Customer a = new Customer();
            HttpCookie ck = Request.Cookies["customerDATA"];
            if(ck!=null)
            {
                
                a.email = ck["email"].ToString() ;
                a.password = ck["password"].ToString() ;
            }
            return View(a);
        }
        [HttpPost]
        public ActionResult CustomerLogin(Customer obj)
        {
            con.Open();

            String query = "select * from customer where email='" + obj.email + "' and password='" + obj.password + "'";
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataReader sdr= cmd.ExecuteReader();
            if(sdr.HasRows)
            {
                sdr.Read();
                Session["customerID"] = sdr["cid"].ToString();
                HttpCookie cook = new HttpCookie("customerDATA");
                cook["email"]=obj.email;
                cook["password"]=obj.password;
                Response.Cookies.Add(cook);
                cook.Expires=DateTime.Now.AddDays(2);


                return RedirectToAction("Home","Customer");
            }
            con.Close();
            return View();
        }
    }
}