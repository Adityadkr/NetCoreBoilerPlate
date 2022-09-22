using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels
{
    public class Alien
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string name { get; set; }

        public string pass { get; set; }

        public string tech { get; set; }

        public bool sub { get; set; }
        public int __v { get; set; }
    }
}
