using System;
using System.Collections.Generic;
using System.Text;

namespace MongodbDemo.Documents
{
    public class Address
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    }
}
