using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using SupplyAI.Models;

namespace SupplyAI
{
    //Class used for accessing MongoDB database w/ methods ofr easy & convenienct loading/storing
    public class Database
    {
        //public const string AppName = "SupplyAI";
        private static readonly string CONFIGPATH = "App_Data/";
        private static readonly string CONNECTIONSTRING = "mongodb + srv://{0}:{1}@c54-lnh7c.mongodb.net/test?retryWrites=true";
        private static readonly string localconnect = "mongodb://localhost:27017";
        public static readonly string DefaultDatabase = "SupplyAI";
        public MongoClient client { get; private set; }
        public MongoDatabaseBase DefaultDB { get { return (MongoDatabaseBase)client.GetDatabase(DefaultDatabase); } }
        public IMongoCollection<Repository> DataCollection { get { return DefaultDB.GetCollection<Repository>("DataSet"); } }


        public Database() {


            client = new MongoClient(localconnect);
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
        /// <summary>
        /// Reads the config file to load in user name and password
        /// </summary>
        /// <returns>returns string[2] ={ username, password }</returns>
        private static string[] loadUserConfig() {
            string[] config = File.ReadAllLines(CONFIGPATH + "config");
            string username = "";
            string password = "";
            string[] split;
            foreach(var s in config) {
                split = s.Split(' '); //get config line
                if(split.Length ==2) { //if config line has config specification
                    if (split[0] == "username")
                        username = split[1];
                    else if (split[0] == "password")
                        password = split[1];
                }

            }
            return new string[] { username, password };
        }
        private static string getConnectionString() {
            string[] config = loadUserConfig();
            return String.Format(CONNECTIONSTRING, config[0], config[1]);
        }

    }
    
    public static class MongoDBExtensions
    {
        public static bool isDatabaseAvailable(this IMongoDatabase database) {
            return database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
        }

    }
  
}