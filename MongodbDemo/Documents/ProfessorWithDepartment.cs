using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbDemo.Documents
{
    public class ProfessorWithDepartment
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ObjectId Department_id { get; set; }
        public Department Department { get; set; }
       
    }
}
