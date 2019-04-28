using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI3.Models
{
    public interface IMongoDocument
    {
        // [BsonIgnore]
        string MongoCollectionName { get; }

        // [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        string Id { get; set; }
    }
}
