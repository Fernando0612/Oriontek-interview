using OrionTek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrionTek.Controllers
{
    public class HomeController : Controller
    {

        Helpper.DataAccess dataAccess = new Helpper.DataAccess();

        public ActionResult Index()
        {
            return View(dataAccess.GetRecentClients());
        }

        public ActionResult Clients()
        {


            return View(dataAccess.GetClients());
        }

        public ActionResult CreateClients()
        {
         

            return View();
        }


        public ActionResult Create_Clients(Clients client)
        {

            return Json(new
            {
                result = dataAccess.CreateClient(client)
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ClientDetails(int ID)
        {
            ViewBag.Addresses = dataAccess.GetAddresses(ID);

            //dataAccess.GetClient()
            return View(dataAccess.GetClient(ID));
        }


        public ActionResult CreateClientAddress(int ID)
        {
            ViewBag.ClientID = ID;

            return View();
        }


        public ActionResult Create_ClientAdress(Address address)
        {

            return Json(new
            {
                result = dataAccess.CreateClientAddress(address)
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Update_Client(Clients client)
        {

            return Json(new
            {
                result = dataAccess.UpdateClient(client)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete_Client(Clients client)
        {

            return Json(new
            {
                result = dataAccess.DeleteClient(client)
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete_Address(Address address)
        {

            return Json(new
            {
                result = dataAccess.DeleteAddress(address)
            }, JsonRequestBehavior.AllowGet);
        }



    }
}