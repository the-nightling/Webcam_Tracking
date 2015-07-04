using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileQA.UserInterface.Models
{
    public class TrackingEventList
    {
        public List<API.Domain.TrackingEvent> items { get; set; }
        public string id { get; set; }
    }
}