using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileQA.API.Domain;
using MobileQA.API.DataAccess;
using MongoDB.Driver.Builders;

namespace MobileQA.UserInterface.Controllers
{
    public class TrackingEventController : Controller
    {
        public ActionResult Index()
        {
            Models.TrackingEventList trackingEvents = new Models.TrackingEventList();
            Database database = new Database();
            trackingEvents.items = database.TrackingEvents.FindAll().OrderByDescending(i => i.dateTime).ToList();

            return View(trackingEvents);
        }

        [HttpPost]
        public ActionResult Index(Models.TrackingEventList trackingEvents)
        {
            return RedirectToAction("Details", "TrackingEvent", new { Id = trackingEvents.id });
        }

        public ActionResult Details(string Id)
        {
            Database database = new Database();
            TrackingEvent trackingEventDom = database.TrackingEvents.FindOne(Query.EQ("_id", Id));

            return View(trackingEventDom);
        }
    }
}
