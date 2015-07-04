using MobileQA.API.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace MobileQA.API.DataAccess
{
	public class Database
	{
		private MongoDatabase database;

		public Database()
		{   
			string connectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
			InitSerialization();

			if (String.IsNullOrWhiteSpace(connectionString))
				throw new Exception("Database connectionstring is empty.");

			MongoUrl mongoUrl = new MongoUrl(connectionString);
			this.database = new MongoClient(mongoUrl).GetServer().GetDatabase(mongoUrl.DatabaseName);
		}

		public MongoCollection<TrackingEvent> TrackingEvents { get { return database.GetCollection<TrackingEvent>("TrackingEvents", WriteConcern.Acknowledged); } }

		private void InitSerialization()
		{
			var dateTimeSerializer = new DateTimeSerializer(DateTimeSerializationOptions.LocalInstance);

			try
			{
				BsonSerializer.RegisterSerializer(typeof(DateTime), dateTimeSerializer);
			}
			catch (BsonSerializationException exception)
			{
				if (exception.Message != "There is already a serializer registered for type System.DateTime.")
				{
					throw exception;
				}
			}

			BsonSerializer.RegisterIdGenerator(typeof(string), StringObjectIdGenerator.Instance);
		}
	}
}
