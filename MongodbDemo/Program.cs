using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbDemo.Documents;
using System.Linq;

namespace MongodbDemo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
           
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false");

            var database = dbClient.GetDatabase("university");
            var professors = database.GetCollection<Professor>("professors");
            var departments = database.GetCollection<Department>("departments");


            var allProfessors = professors.Find(_ => true).ToList();

            foreach(var professor in allProfessors)
            {
                Console.WriteLine(professor);
            }

            Console.WriteLine("Filter");

            await professors.Find(pr => pr.Address.Country == "Ukraine" && (pr.Name.Contains("2") || pr.Name.Contains("4")))
                .ForEachAsync(pr => Console.WriteLine(pr));

            Console.WriteLine("Aggregation");

            var aggregationResult = professors.Aggregate()
                                              .Match(pr => pr.Address.Country == "Ukraine")
                                              .Lookup(
                                                    foreignCollection: departments,
                                                    localField: prof => prof.Department_id,
                                                    foreignField: dep => dep.Id,
                                                    @as: (ProfessorWithDepartment pwd) => pwd.Department)
                                              .Group(pwd => pwd.Department,
                                                     group => new { Department = group.Key, Count = group.Count() })
                                              .Limit(5);

            await aggregationResult.ForEachAsync(doc => Console.WriteLine(doc.ToJson()));
        }

    }
}
