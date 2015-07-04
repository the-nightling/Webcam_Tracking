using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileQA.UserInterface.Models
{
    public class TrackingEvent
    {
        [Required(ErrorMessage = "Tracking no. is required")]
        public string parcelNum { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string accuracy { get; set; }
        public string comments { get; set; }
        public bool hasGetUserMedia { get; set; }

        public string videoFrameBase64 { get; set; }
        public string videoFrameBase64_2 { get; set; }
        public string videoFrameBase64_3 { get; set; }

        [Required(ErrorMessage = "At least two photos are required")]
        public HttpPostedFileBase File { get; set; }
        [Required(ErrorMessage = "At least two photos are required")]
        public HttpPostedFileBase File_2 { get; set; }
        public HttpPostedFileBase File_3 { get; set; }
    }
}