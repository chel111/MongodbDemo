using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongodbDemo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
           
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false");

            var database = dbClient.GetDatabase("university");
            var subjectCollection = database.GetCollection<BsonDocument>("subjects");
            var addressCollection = database.GetCollection<BsonDocument>("address");

            var allSubjects = subjectCollection.Find(_ => true).ToList();

            foreach(var subject in allSubjects)
            {
                Console.WriteLine(subject);
            }

            Console.WriteLine("Filter");

            var titleFilter = Builders<BsonDocument>.Filter.Eq("Title", "title1");
            var descFilter = Builders<BsonDocument>.Filter.Eq("Description", "desc1");

            await subjectCollection.Find(titleFilter & descFilter).ForEachAsync(document => Console.WriteLine(document));

            Console.WriteLine("Aggregation");


            var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"Country", "Ukraine"}
                            }
                    }
                };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { 
                                    "Country", new BsonDocument
                                    {
                                        { "Country","$Country" },
                                    }
                                },
                                {
                                    "Count", new BsonDocument
                                    {
                                        { "$sum", "$Count" }
                                    }
                                }
                            }
                  }
                };
            
            var limit = new BsonDocument
                {
                    { "$limit",
                        new BsonDocument
                            {
                                "5"
                            }
                  }
                };


           


        }

    }
}
