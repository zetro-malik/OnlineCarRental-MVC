using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCarRental.Models;
using System.Data.SqlClient;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace OnlineCarRental.Controllers
{
    public class CustomerController : Controller
    {
        static string q = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=rentalCar;Data Source=ZETRO\\MSSQLSERVER01";
        SqlConnection con = new SqlConnection(q);
        // GET: Shop
        public ActionResult Home()
        {
            if (Session["customerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "inup");
            }
            return View();
        }
        public ActionResult display()
        {

            List<Car> cars = new List<Car>();
            con.Open();
            String sql = "select * from car";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Car car = new Car();
                car.Id = int.Parse(sdr["carid"].ToString());
                car.year = int.Parse(sdr["year"].ToString());
                car.color = sdr["color"].ToString();
                car.conditon = sdr["codition"].ToString();
                car.carno = sdr["carno"].ToString();
                car.img = sdr["img"].ToString();

                cars.Add(car);
            }

            con.Close();
            return View(cars);
        }
        public ActionResult contact()
        {
            if (Session["customerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "inup");
            }
            return View();
        }

        public ActionResult FindCar(int? page)
        {
            if (Session["customerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "inup");
            }
            List<carNDshop> cars = new List<carNDshop>();
            con.Open();
            string query = "select * from car left join shop on car.sid=shop.sid";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    carNDshop car = new carNDshop();
                    car.Id = int.Parse(sdr["carid"].ToString());
                    car.year = int.Parse(sdr["year"].ToString());
                    car.color = sdr["color"].ToString();
                    car.conditon = sdr["codition"].ToString();
                    car.carno = sdr["carno"].ToString();
                    car.img = sdr["img"].ToString();
                    car.shopid = int.Parse(sdr["sid"].ToString());
                    car.shopname = sdr["name"].ToString();
                    car.address = sdr["address"].ToString();
                    car.phone = sdr["phone"].ToString();
                    car.email = sdr["email"].ToString();

                    cars.Add(car);
                }
            }
            else
            {
                ViewBag.NoData = "Sorry No Results Found...";

            }
            con.Close();



          
            return View(cars.ToPagedList(page ?? 1,3));
        }
        [HttpPost]
        public ActionResult FindCar(int startprice,int endprice)
        {
            if (Session["customerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "inup");
            }
            List<carNDshop> cars = new List<carNDshop>();
            con.Open();
            string query = "select * from car left join shop on car.sid=shop.sid where rent>"+startprice+"and rent<"+endprice+" and not car.carid in(select carid from rent)";
            SqlCommand cmd= new SqlCommand(query,con);

            SqlDataReader sdr = cmd.ExecuteReader();

            if(sdr.HasRows)
            {
                while(sdr.Read())
                {
                    carNDshop car = new carNDshop();
                    car.Id = int.Parse(sdr["carid"].ToString());
                    car.year = int.Parse(sdr["year"].ToString());
                    car.color = sdr["color"].ToString();
                    car.conditon = sdr["codition"].ToString();
                    car.carno = sdr["carno"].ToString();
                    car.img = sdr["img"].ToString();
                    car.shopid = int.Parse(sdr["sid"].ToString());
                    car.shopname=sdr["name"].ToString();
                    car.address=sdr["address"].ToString();
                    car.phone=sdr["phone"].ToString();
                    car.email=sdr["email"].ToString();

                    cars.Add(car);
                }
            }
            else
            {
            ViewBag.NoData = "Sorry No Results Found...";

            }
            con.Close();


          
            return View(cars);
        }

        public ActionResult Rent(int id, int shopid)
        {
            if (Session["customerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "inup");
            }
            con.Open();
            String sql = "insert into rent values(" + int.Parse(Session["customerID"].ToString()) + "," + shopid + "," + id+")";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("FindCar");
        }




        public ActionResult MyCars()
        {
            if (Session["customerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "inup");
            }

            List<carNDshop> cars = new List<carNDshop>();
            con.Open();
            string query = "select * from car left join shop on car.sid=shop.sid where  car.carid in(select carid from rent where cid="+Session["customerID"] +")";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    carNDshop car = new carNDshop();
                    car.Id = int.Parse(sdr["carid"].ToString());
                    car.year = int.Parse(sdr["year"].ToString());
                    car.color = sdr["color"].ToString();
                    car.conditon = sdr["codition"].ToString();
                    car.carno = sdr["carno"].ToString();
                    car.img = sdr["img"].ToString();
                    car.shopid = int.Parse(sdr["sid"].ToString());
                    car.shopname = sdr["name"].ToString();
                    car.address = sdr["address"].ToString();
                    car.phone = sdr["phone"].ToString();
                    car.email = sdr["email"].ToString();

                    cars.Add(car);
                }
            }
            else
            {
                ViewBag.NoData = "Sorry No Results Found...";

            }
            con.Close();



            return View(cars);

        }


        

        public ActionResult logout()
        {
          
            Session["customerID"] = null;

            return RedirectToAction("CustomerLogin", "inup");
        }
    }
}