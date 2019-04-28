using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models
{
    public class NewAbstractViewModel
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string MongoCollectionName { get { return "Abstracts"; } }
        public string AttachmentId { get; set; }
        public string NotificationId { get; set; }
        public bool Rehost { get; set; }
        public bool PublicAccess { get; set; }
        public string Content { get; set; }
        public bool Approved { get; set; }
        public bool Reviewed { get; set; }
        public string ReviewedBy { get; set; }  // Mongo GUID for admin who approved/denied request
        public DateTime DateReviewed { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}