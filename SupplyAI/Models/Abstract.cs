using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Microsoft.AspNet.Identity;

namespace MI3.Models
{
    public class Abstract : IMongoDocument
    {
        [BsonIgnore]
        public string MongoCollectionName { get { return "Abstracts"; } }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement]
        public string UserName { get; set; }       
        // [BsonElement]
        // public string UserId { get; set; }
        [BsonElement]
        public string Url { get; set; }
        [BsonElement]
        public bool Rehost { get; set; }
        [BsonElement]
        public bool PublicAccess { get; set; }
        [BsonElement]
        public string Content { get; set; }
        [BsonElement]
        public bool Approved { get; set; }
        [BsonElement]
        public bool Reviewed { get; set; }
        [BsonElement]
        public string ReviewedBy { get; set; }  // Mongo GUID for admin who approved/denied request
        [BsonElement]
        public DateTime DateReviewed { get; set; }
        [BsonElement]
        public DateTime DateGenerated { get; set; }

        //[BsonConstructor]
        public Abstract(string userName, string url, bool rehost, bool publicAccess, string content)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName;
            Url = url;
            Rehost = rehost;
            PublicAccess = publicAccess;
            Content = content;
            DateGenerated = DateTime.Now;
        }

        public void Review(bool approved)
        {
            Approved = approved;
            Reviewed = true;
            //ReviewedBy = 
            DateReviewed = DateTime.Now;
            //Notification<Abstract> = new Notification<Abstract>(UserId, )
        }

        public void AddToDb()
        {
            Database db = new Database();
            db.Add<Abstract>(MongoCollectionName, this);
        }

        public Notification GenerateNewAbstractNotification()
        {
            NewAbstractNotification notification = new NewAbstractNotification();
            return new Notification(UserName, notification, this);
        }

        public Notification GenerateReviewedAbstractNotification()
        {
            ReviewedAbstractNotification notification = new ReviewedAbstractNotification();
            return new Notification(UserName, notification, this);
        }

        /*
        public NewAbstractNotification GenerateNewAbstractNotification()
        {
            return new NewAbstractNotification(UserId, new List<Abstract> { this });
        }

        public ReviewedAbstractNotification GenerateReviewedAbstractNotification()
        {
            return new ReviewedAbstractNotification(UserId, new List<Abstract> { this });
        }*/
    }
}