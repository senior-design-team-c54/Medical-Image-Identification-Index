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
    //Class used for accessing MongoDB database w/ methods ofr easy & convenienct loading/storing
    public class Database
    {
        public const string AppName = "MI3";
        // private static readonly string localconnect = "mongodb://localhost:27017";
        public static readonly string DefaultDatabase = "MI3";
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public MongoClient client { get; private set; }
        public IMongoDatabase DefaultDB { get { return (new MongoClient(connectionString)).GetDatabase((new MongoUrl(connectionString)).DatabaseName); } }
        public IMongoCollection<Repository> DataCollection { get { return DefaultDB.GetCollection<Repository>("Repository"); } }


        public Database() {
            var path = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            client = new MongoClient(path);
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

        public T FindOne<T>(string collection, Expression<Func<T,bool>> filter)
        {
            return DefaultDB.GetCollection<T>(collection).Find(filter).FirstOrDefault();
        }

        //public T QueryFindOne<T>(string collection, Expression<Func<T>>, )

        public void AddRepository(Repository repo) {
            DataCollection.InsertOne(repo);
        }

        public void Add<T>(string collection, T item) {
            
            var coll = DefaultDB.GetCollection<T>(collection);
            coll.InsertOne(item);
        }

        public int Count<T>(string collection, Expression<Func<T,bool>> filter)
        {
            var coll = DefaultDB.GetCollection<T>(collection);
            return (int) coll.CountDocuments<T>(filter);
        }

        public void Update<T,U>(string collection, Expression<Func<T,bool>> filter, Expression<Func<T,U>> update, U value)
        {
            var coll = DefaultDB.GetCollection<T>(collection);
            var mongoUpdate = Builders<T>.Update.Set(update, value);
            coll.UpdateOne(filter, mongoUpdate);
        }

        public void Replace<T>(string collection, Expression<Func<T, bool>> filter, T replacement)
        {
            var coll = DefaultDB.GetCollection<T>(collection);
            coll.ReplaceOne<T>(filter, replacement);
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