using System;
using NUnit.Framework;
using MobileQA.API.Domain;
using MongoDB.Driver.Builders;
using System.Linq;

namespace MobileQA.API.Tests
{
	[TestFixture]
	public class TestTrackingEvent
	{
		[TestFixtureSetUp] public void SetupFixture() {}
		[TestFixtureTearDown] public void TeardownFixture() {}
		[SetUp] public void SetupTest() {}
		[TearDown] public void TeardownTest() {}

        [Test]
        public void TestAddNewTrackingEvent()
        {
            TrackingEvent newTrackingEvent = new TrackingEvent("parcel1", "36 long", "0", "0", "0", "", new byte[] { 0 }, new byte[] { 0 }, new byte[] { 0 });
            newTrackingEvent.save();
            Assert.IsFalse(string.IsNullOrWhiteSpace(newTrackingEvent.Id));
            Assert.AreEqual("parcel1", newTrackingEvent.parcelNum);
        }

        [Test]
        public void TestAddNewTrackingEventAndDatabaseQuery()
        {
            var random = new Random();
            var randomString = new string(
                Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            TrackingEvent newTrackingEvent = new TrackingEvent(randomString, "36 long", "0", "0", "0", "", new byte[] { 0 }, new byte[] { 0 }, new byte[] { 0 });
            newTrackingEvent.save();
            MobileQA.API.DataAccess.Database database = new MobileQA.API.DataAccess.Database();
            TrackingEvent returnedTrackingEvent = database.TrackingEvents.FindOne(Query.EQ("parcelNum", newTrackingEvent.parcelNum));

            Assert.AreEqual(newTrackingEvent.Id, returnedTrackingEvent.Id);
            Assert.AreEqual(newTrackingEvent.parcelNum, returnedTrackingEvent.parcelNum);
            Assert.AreEqual(newTrackingEvent.address, returnedTrackingEvent.address);
            Assert.AreEqual(newTrackingEvent.latitude, returnedTrackingEvent.latitude);
            Assert.AreEqual(newTrackingEvent.longitude, returnedTrackingEvent.longitude);
            Assert.AreEqual(newTrackingEvent.accuracy, returnedTrackingEvent.accuracy);
            Assert.AreEqual(newTrackingEvent.imgData1, returnedTrackingEvent.imgData1);
            Assert.AreEqual(newTrackingEvent.imgData1, returnedTrackingEvent.imgData1);
            Assert.AreEqual(newTrackingEvent.imgData1, returnedTrackingEvent.imgData1);
            Assert.AreEqual(newTrackingEvent.comments, returnedTrackingEvent.comments);
            //Assert.AreEqual(newTrackingEvent.dateTime, returnedTrackingEvent.dateTime);
        }

		[Test]
        [ExpectedException(ExpectedMessage = "Value cannot be null.\r\nParameter name: parcelNum", ExpectedException=typeof(ArgumentNullException))]
		public void TestAddNewTrackingEventWithoutParcelNumber()
		{
            new TrackingEvent(null, "36 long", "0", "0", "0", "", null, null, null);
		}

        [Test]
        [ExpectedException(ExpectedMessage = "Value cannot be an empty string\r\nParameter name: address", ExpectedException=typeof(ArgumentException))]
        public void TestAddNewTrackingEventWithoutAddress()
        {
            new TrackingEvent("parcel1", "", "0", "0", "0", "", null, null, null);
        }

        [Test]
        [ExpectedException(ExpectedMessage = "Value cannot be null.\r\nParameter name: imgData1", ExpectedException = typeof(ArgumentNullException))]
        public void TestAddNewTrackingEventWithoutImages()
        {
            new TrackingEvent("parcel1", "36 long", "0", "0", "0", "", null, null, null);
        }

        [Test]
        [ExpectedException(ExpectedMessage = "Value cannot be null.\r\nParameter name: imgData2", ExpectedException = typeof(ArgumentNullException))]
        public void TestAddNewTrackingEventWithoutImage2AndImage3()
        {
            new TrackingEvent("parcel1", "36 long", "0", "0", "0", "", new byte[] { 0 }, null, null);
        }

	}
}
