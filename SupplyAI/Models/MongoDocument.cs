using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MI3.Models
{
    public class MongoDocument
    {
        [BsonIgnore]
        public static string MongoCollectionName;

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public MongoDocument() { }

        public string GetMongoCollectionName()
        {
            return MongoCollectionName;
        }
    }
}