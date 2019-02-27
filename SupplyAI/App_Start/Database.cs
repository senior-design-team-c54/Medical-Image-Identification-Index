using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using SupplyAI.Models;

namespace SupplyAI
{
    public class Database
    {
        //public const string AppName = "SupplyAI";
        private static readonly string connectionString = "mongodb://localhost:27017";
        public static readonly string DefaultDatabase = "SupplyAI";
        public MongoClient client { get; private set; }
        public MongoDatabaseBase DefaultDB { get { return (MongoDatabaseBase)client.GetDatabase(DefaultDatabase); } }
        public IMongoCollection<Repository> DataCollection { get { return DefaultDB.GetCollection<Repository>("DataSet"); } }


        public Database() {
            client = new MongoClient(connectionString);
        }

        public MongoDatabaseBase GetDatabase(string name) {
            return (MongoDatabaseBase)client.GetDatabase(name);
        }
        
        public async System.Threading.Tasks.Task<string[]> listDatabasesAsync() {
            List<string> dbs = new List<string>();
            using (var cursor = await client.ListDatabasesAsync()) {
                await cursor.ForEachAsync(d => dbs.Add(d["name"].AsString));
            }
            return dbs.ToArray();
        }


    }
    public static class MongoDBExtensions
    {
        public static bool isDatabaseAvailable(this IMongoDatabase database) {
            return database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
        }

    }
}