using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbDemo.Documents
{
    public class Department
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ObjectId[] Professors { get; set; }
        public ObjectId[] Students { get; set; }
    }
}
