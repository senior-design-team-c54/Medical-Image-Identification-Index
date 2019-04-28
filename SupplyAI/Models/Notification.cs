using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace MI3.Models
{
    public class Notification
    {
        [BsonIgnore]
        public static readonly string MongoCollectionName = "Notifications";
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement]
        public string UserName { get; set; }
        // [BsonElement]
        // public string UserId { get; set; }
        [BsonElement]
        public string ForUserType { get; set; }
        [BsonElement]
        public string NotificationType { get; set; }
        [BsonElement]
        public string Content { get; set; }
        [BsonIgnore]
        public IMongoDocument Attachment { get; set; }
        [BsonElement("Attachment")]
        public string AttachmentIdString { get; set; }
        [BsonElement]
        public string AttachmentType { get; set; }
        [BsonElement]
        public bool Resolved { get; set; }
        [BsonElement]
        public string ResolvedBy { get; set; }  // Mongo GUID for admin who resolved notification
        [BsonElement]
        public DateTime DateResolved { get; set; }
        [BsonElement]
        public DateTime DateGenerated { get; set; }

        //[BsonConstructor]
        public Notification(string userName)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName;
            Resolved = false;
            DateGenerated = DateTime.Now;
        }

        //[BsonConstructor]
        public Notification(string userName, INotifiable notificationType)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName;
            Resolved = false;
            ForUserType = notificationType.ForUserType;
            NotificationType = notificationType.GetType().FullName; //.ToString();
            DateGenerated = DateTime.Now;
        }

        //[BsonConstructor]
        public Notification(string userName, INotifiable notificationType, IMongoDocument attachment)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName;
            Resolved = false;
            ForUserType = notificationType.ForUserType;
            NotificationType = notificationType.GetType().FullName; //.ToString();
            Attachment = attachment;
            AttachmentIdString = attachment.Id;
            AttachmentType = attachment.GetType().Name;
            DateGenerated = DateTime.Now;
        }

        [BsonConstructor]
        public Notification() { }

        [Authorize(Roles = "admin")]
        public void Resolve()
        {
            Resolved = true;
            //ResolvedBy =
            DateResolved = DateTime.Now;
        }

        public virtual void AddToDb()
        {
            Database db = new Database();
            db.Add<Notification>(MongoCollectionName, this);
        }
    }

    public class NewAbstractNotification : INotifiable
    {
        public string Title { get { return "Upload Request: New Abstract Submission"; } }
        public string ForUserType { get { return "admin"; } }
        public NewAbstractNotification() { }
    }

    public class ReviewedAbstractNotification : INotifiable
    {
        public string Title { get { return "Your abstract has been reviewed."; } }
        public string ForUserType { get { return "user"; } }
        public ReviewedAbstractNotification() { }
    }

    /*
    public class Notification : MongoDocument
    {
        [BsonIgnore]
        public new static readonly string MongoCollectionName = "Notifications";

        [BsonElement]
        public string UserId { get; set; }

        //[BsonElement]
        //public virtual string ForUserType { get; }

        [BsonElement]
        public string Content { get; set; }

        [BsonElement]
        public bool Resolved { get; set; }

        [BsonElement]
        public string ResolvedBy { get; set; }  // Mongo GUID for admin who resolved notification

        [BsonElement]
        public DateTime DateResolved { get; set; }

        [BsonElement]
        public DateTime DateGenerated { get; set; }

        [BsonConstructor]
        public Notification(string userId, string content)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserId = userId;
            Content = content;
            Resolved = false;
            DateGenerated = DateTime.Now;
        }

        [BsonConstructor]
        public Notification(string userId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserId = userId;
            Resolved = false;
            DateGenerated = DateTime.Now;
        }

        [Authorize(Roles = "admin")]
        public void Resolve()
        {
            Resolved = true;
            //ResolvedBy =
            DateResolved = DateTime.Now;
        }

        public virtual void AddToDb()
        {
            Database db = new Database();
            db.Add<Notification>(MongoCollectionName, this);
        }
    }

    public class Notification<T> : Notification where T : MongoDocument, new()
    {
        [BsonElement]
        public string AttachmentType { get { return typeof(T).Name.ToString(); } }

        [BsonElement]
        public INotifiable NotificationType { get; set; }    // was virtual with only a getter before

        [BsonIgnore]
        public List<T> Attachments { get; set; }

        [BsonElement("Attachments")]
        public List<string> AttachmentIdStrings { get; set; }

        public Notification(string userId, string content, List<T> attachments) : base(userId, content)
        {
            Attachments = attachments;
            AttachmentIdStrings = new List<string>();
            foreach (T attachment in attachments)
            {
                AttachmentIdStrings.Add(attachment.Id);
            }
        }

        public Notification(string userId, List<T> attachments) : base(userId)
        {
            Attachments = attachments;
            AttachmentIdStrings = new List<string>();
            foreach (T attachment in attachments)
            {
                AttachmentIdStrings.Add(attachment.Id);
            }
        }

        public Notification(string userId, List<string> attachmentIdStrings) : base(userId)
        {
            AttachmentIdStrings = attachmentIdStrings;
            Database db = new Database();
            Attachments = new List<T>();
            T DynamicTObject = new T();
            string collection = DynamicTObject.GetMongoCollectionName();
            foreach (string id in attachmentIdStrings)
            {
                Attachments.Add(db.FindOne<T>(DynamicTObject.GetMongoCollectionName(), doc => doc.Id == id));
            }
        }

        public new void AddToDb()
        {
            Database db = new Database();
            db.Add<Notification<T>>(MongoCollectionName, this);
        }
    }
    */

    /*
    public class NewAbstractNotification : Notification<Abstract>, INotifiable
    {
        public override string ForUserType { get { return "admin"; } }

        public override string NotificationType { get { return typeof(NewAbstractNotification).Name.ToString(); } }

        [BsonConstructor]
        public NewAbstractNotification(string userId, List<string> attachmentIdStrings) : base(userId, attachmentIdStrings) { }

        public NewAbstractNotification(string userId, List<Abstract> attachments) : base(userId, attachments) { }
    }

    public class ReviewedAbstractNotification : Notification<Abstract>, INotifiable
    {
        public override string ForUserType { get { return "user"; } }

        public override string NotificationType { get { return typeof(ReviewedAbstractNotification).Name.ToString(); } }

        [BsonConstructor]
        public ReviewedAbstractNotification(string userId, List<string> attachmentIdStrings) : base(userId, attachmentIdStrings) { }

        public ReviewedAbstractNotification(string userId, List<Abstract> abstracts) : base(userId, abstracts) { }
    }*/
}