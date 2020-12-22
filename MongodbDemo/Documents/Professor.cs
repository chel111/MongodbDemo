using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbDemo.Documents
{
    public class Professor
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ObjectId Department_id  { get; set; }

        public override string ToString()
        {
            return $"Professor {Name}";
        }
    }
}
