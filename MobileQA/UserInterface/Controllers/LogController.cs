/*
 * To do:
 * - Get address location*
 * - Barcode scanner*
 * - picture resize
 * - cookie
 * - get timestamp from device
 * - gallery
 *
 */

using MobileQA.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileQA.UserInterface.Controllers
{
    public class LogController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("TrackingEvent");
        }

        public ActionResult TrackingEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TrackingEvent(TrackingEvent trackingEvent)
        {
            try
            {
                byte[] imgData1;
                byte[] imgData2;
                byte[] imgData3;
                if (trackingEvent.hasGetUserMedia)
                {
                    imgData1 = System.Convert.FromBase64String(trackingEvent.videoFrameBase64);
                    imgData2 = System.Convert.FromBase64String(trackingEvent.videoFrameBase64_2);
                    if (trackingEvent.videoFrameBase64_3 != null)
                        imgData3 = System.Convert.FromBase64String(trackingEvent.videoFrameBase64_3);
                    else
                        imgData3 = null;
                } else {
                    System.IO.BinaryReader byteReader1 = new System.IO.BinaryReader(trackingEvent.File.InputStream);
                    imgData1 = byteReader1.ReadBytes((int)trackingEvent.File.InputStream.Length);
                    System.IO.BinaryReader byteReader2 = new System.IO.BinaryReader(trackingEvent.File_2.InputStream);
                    imgData2 = byteReader2.ReadBytes((int)trackingEvent.File_2.InputStream.Length);
                    if (trackingEvent.File_3 != null){
                        System.IO.BinaryReader byteReader3 = new System.IO.BinaryReader(trackingEvent.File_3.InputStream);
                        imgData3 = byteReader3.ReadBytes((int)trackingEvent.File_3.InputStream.Length);
                    }
                    else{
                        imgData3 = null;
                    }
                }
                
                API.Domain.TrackingEvent trackingEventDom = new API.Domain.TrackingEvent(
                    trackingEvent.parcelNum,
                    trackingEvent.address,
                    trackingEvent.latitude,
                    trackingEvent.longitude,
                    trackingEvent.accuracy,
                    trackingEvent.comments,
                    imgData1,
                    imgData2,
                    imgData3);

                trackingEventDom.save();
                
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
