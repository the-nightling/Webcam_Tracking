using MobileQA.API.DataAccess;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.IO;

namespace MobileQA.API.Domain
{
    public class TrackingEvent
    {
        public string parcelNum { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string accuracy { get; set; }
        public string comments { get; set; }
        public System.DateTime dateTime { get; set; }
        public byte[] imgData1 { get; set; }
        public byte[] imgData2 { get; set; }
        public byte[] imgData3 { get; set; }
        public string Id { get; set; }

        public TrackingEvent(
            string parcelNum,
            string address,
            string latitude,
            string longitude,
            string accuracy,
            string comments,
            byte[] imgData1,
            byte[] imgData2,
            byte[] imgData3)
        {
            if (string.IsNullOrWhiteSpace(parcelNum))
                throw new ArgumentNullException("parcelNum");
            else
                this.parcelNum = parcelNum;

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Value cannot be an empty string", "address");
            else
                this.address = address;

            if (imgData1 == null)
                throw new ArgumentNullException("imgData1");
            else if(imgData2 == null)
                throw new ArgumentNullException("imgData2");
            else
            {
                this.imgData1 = imgData1;
                this.imgData2 = imgData2;
                this.imgData3 = imgData3;
            }

            this.latitude = latitude;
            this.longitude = longitude;
            this.accuracy = accuracy;
            this.comments = comments;
        }

        public void save()
        {
            Database dataBase = new Database();
            WriteConcernResult result;

            if(string.IsNullOrWhiteSpace(Id))
            {
                this.dateTime = System.DateTime.Now;
                result = dataBase.TrackingEvents.Insert<Domain.TrackingEvent>(this);
                //writeImage(this.imgData1);
            }
            else
            {
                result = dataBase.TrackingEvents.Update(Query.EQ("_id", Id),
                        Update.Combine(
                            Update.Set("parcelNum", parcelNum),
                            Update.Set("address", address),
                            Update.Set("latitude", latitude),
                            Update.Set("longitude", longitude),
                            Update.Set("accuracy", accuracy),
                            Update.Set("comments", comments),
                            Update.Set("dateTime", dateTime),
                            Update.Set("imgData1", imgData1),
                            Update.Set("imgData2", imgData2),
                            Update.Set("imgData3", imgData3)));
            }

            if (!result.Ok)
            {
                throw new Exception(result.ErrorMessage);
            }
        }

        private void writeImage(byte[] imgAray)
        {
            File.WriteAllBytes("C:\\Temp\\image.jpg", imgAray);
        }
    }
}