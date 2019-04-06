using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using MI3.Models;
using System.Configuration;

namespace MI3
{
    //Class used for accessing MongoDB database w/ methods for easy & convenienct loading/storing
    public class Database
    {
        //public const string AppName = "MI3";
        private static readonly string LOGINPATH = @"App_Data/";
        private static readonly string LOGINFILE = "login.pass";
        private static readonly string CONNECTIONSTRING = @"mongodb+srv://{0}:{1}@c54-lnh7c.mongodb.net/test?retryWrites=true";
        private static readonly string localconnect = "mongodb://localhost:27017";
        public static readonly string DefaultDatabase = "MI3";
        public MongoClient client { get; private set; }
        public MongoDatabaseBase DefaultDB { get { return (MongoDatabaseBase)client.GetDatabase(DefaultDatabase); } }
        public IMongoCollection<Repository> DataCollection { get { return DefaultDB.GetCollection<Repository>("Repository"); } }

        public Database() {
            //var path = getConnectio nString();
           
            client = new MongoClient(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
        }
        

        public MongoDatabaseBase GetDatabase(string name) {
            return (MongoDatabaseBase)client.GetDatabase(name);
        }
        public List<Repository> FindRepo(Expression<Func<Repository, bool>> filter) {
            return DataCollection.Find(filter).ToList();
        }
        public List<T> Find<T>(string collection, Expression<Func<T,bool>> filter) {
            return DefaultDB.GetCollection<T>(collection).Find(filter).ToList();
        }

        public void AddRepository(Repository repo) {
            DataCollection.InsertOne(repo);
        }

        public void Add<T>(string collection, T item) {
            var coll = DefaultDB.GetCollection<T>(collection);
            coll.InsertOne(item);
        }
        

       

        /// <summary>
        /// Reads the config file to load in user name and password
        /// </summary>
        /// <returns>returns string[2] ={ username, password }</returns>
        private static string[] loadUserConfig() {
            string filePath = HostingEnvironment.MapPath("~/bin/App_Data/");


            
            string[] config = File.ReadAllLines(filePath + LOGINFILE);
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
        public static string getConnectionString() {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] config = loadUserConfig();
            if (config == null) throw new Exception("Cannot load database login file");
            return String.Format(CONNECTIONSTRING, config[0], config[1]);
        }
       


    }
    
    public static class MongoDBExtensions
    {
        public static bool isDatabaseAvailable(this MongoClient client) {
            var probeTask =
                    Task.Run(() => {
                        var isAlive = false;


                        for (var k = 0; k < 6; k++) {
                            client.Cluster.Description.Servers.FirstOrDefault();
                            var server = client.Cluster.Description.Servers.FirstOrDefault();
                            isAlive = (server != null &&
                                   server.HeartbeatException == null &&
                                   server.State == MongoDB.Driver.Core.Servers.ServerState.Connected);
                            if (isAlive) {
                                break;
                            }
                            System.Threading.Thread.Sleep(300);
                        }
                        return isAlive;
                    });
            probeTask.Wait();
            return probeTask.Result;
        }

       

    }
  
}