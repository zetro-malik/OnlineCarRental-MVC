using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCarRental.Models;
using System.Data.SqlClient;
using System.IO;

namespace OnlineCarRental.Controllers
{

    public class ShopController : Controller
    {
        static string q = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=rentalCar;Data Source=ZETRO\\MSSQLSERVER01";
        SqlConnection con = new SqlConnection(q);
        // GET: Shop


        public ActionResult testing()
        {
            return View();
        }
        public ActionResult Home()
        {
            if(Session["shopID"]==null)
            {
                return RedirectToAction("ShopLogin","inup");
            }
            return View();
        }
        public ActionResult display()
        {
           
            List<Car> cars = new List<Car>();   
            con.Open();
            String sql = "select * from car";
            SqlCommand cmd= new SqlCommand(sql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                Car car = new Car();
                car.Id = int.Parse(sdr["carid"].ToString());
                car.year = int.Parse(sdr["year"].ToString());
                car.color=sdr["color"].ToString();
                car.conditon=sdr["codition"].ToString();
                car.carno = sdr["carno"].ToString();
                car.img =sdr["img"].ToString();

                cars.Add(car);
            }

            con.Close();
            return View(cars);
        }
        public ActionResult contact()
        {
            if (Session["shopID"] == null)
            {
                return RedirectToAction("ShopLogin", "inup");
            }
            return View();
        }

        public ActionResult AddCar()
        {
            if (Session["shopID"] == null)
            {
                return RedirectToAction("ShopLogin", "inup");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddCar(Car obj, HttpPostedFileBase file )
        {
            if (Session["shopID"] == null)
            {
                return RedirectToAction("ShopLogin", "inup");
            }
            if (file!=null)
            {
                var fullPath = Path.Combine(Server.MapPath("~/Images"), file.FileName);

                file.SaveAs(fullPath);
                obj.img = file.FileName;

                con.Open();
                String query = "insert into car values('" +Session["shopID"] +"',"+ obj.year + ",'" + obj.color + "','" + obj.conditon + "'," + obj.rent + ",'" + obj.carno + "','" + obj.img + "')";
                SqlCommand cmd=new SqlCommand(query,con);
                cmd.ExecuteNonQuery();

                con.Close();

            }
            return View() ;
        }
        public ActionResult ViewCustomer(string email,string password)
        {
            if (Session["shopID"] == null)
            {
                return RedirectToAction("ShopLogin", "inup");
            }
            List<Customer> customers = new List<Customer>();
            con.Open();
            string query = "select * from customer where cid in (select cid from rent where sid=" + Session["shopID"] + ")";
            string q = "select * customer where email=" + email + "and password=" + password +"";
            SqlCommand cmd =new SqlCommand(query,con);
            SqlDataReader sdr=cmd.ExecuteReader();
        
            while(sdr.Read())
            {
                Customer C = new Customer();
                C.id = int.Parse(sdr["cid"].ToString());
                C.Name=sdr["Name"].ToString();
                C.phone = sdr["phone"].ToString();
                C.email=sdr ["email"].ToString();
                customers.Add(C);
            }
            con.Close();

            return View(customers);
        }

        public ActionResult logout()
        {
            Session["shopID"] = null;

           return RedirectToAction("ShopLogin", "inup");
        }
    }
}