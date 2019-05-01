using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MI3.Models
{
    public interface IMongoFindable
    {
        [BsonIgnore]
        string MongoCollectionName { get; }

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        string Id { get; set; }
    }
}
