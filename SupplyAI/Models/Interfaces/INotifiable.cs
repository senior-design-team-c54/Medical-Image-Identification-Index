using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace MI3.Models
{
    //[BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<INotifiable, Notification<>>))]
    public interface INotifiable
    {
        string Title { get; }
        string ForUserType { get; }
        /*
        //Type NotificationType { get; }
        String AttachmentType { get; }
        string ForUserType { get; }
        string Content { get; }
        string UserId { get; set; }
        bool Resolved { get; set; }
        string ResolvedBy { get; set; }  // Mongo GUID for admin who resolved notification
        DateTime DateResolved { get; set; }
        DateTime DateGenerated { get; set; }
        */
    }
}
