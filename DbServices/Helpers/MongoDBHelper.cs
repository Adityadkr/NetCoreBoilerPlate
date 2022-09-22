using AutoMapper;
using DbEntities.Models.MongoModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace DbServices.Helpers
{
    public class MongoDBHelper
    {
        private readonly IConfiguration _config;
        private static string conStng;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase DB;
        public MongoDBHelper(string dbName, IConfiguration config)
        {
            _config = config;
            conStng = _config.GetConnectionString("mongoDbConnection").ToString();
            _mongoClient = new MongoClient(conStng);
            DB = _mongoClient.GetDatabase(dbName);
        }

        public List<T> Get<T>(string collectionName)
        {
            var collection = DB.GetCollection<T>(collectionName);
            var data = collection.Find(FilterDefinition<T>.Empty).ToList();
            return data;
        }

        public T GetByKey<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            var collection = DB.GetCollection<T>(collectionName);
            var data = collection.Find(predicate).FirstOrDefault();
            return data;


        }

        public bool InsertOne<T>(string collectionName, T data)
        {
            try
            {
                if (data != null)
                {
                    var collection = DB.GetCollection<T>(collectionName);
                    collection.InsertOne(data);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertMany<T>(string collectionName, List<T> data)
        {
            if (data != null)
            {
                var collection = DB.GetCollection<T>(collectionName);
                collection.InsertMany(data);
                return true;
            }
            return false;
        }
        public bool UpdateOne<T>(string collectionName, T data, Expression<Func<T, bool>> predicate)
        {
            if (data != null)
            {
                var collection = DB.GetCollection<T>(collectionName);
                var ogValue = collection.Find(predicate).FirstOrDefault();
                if (ogValue != null)
                {
                    collection.ReplaceOne(predicate, data);
                    return true;
                }
                return false;

            }
            return false;
        }

        public bool UpdateMany<T>(string collectionName, T data, Expression<Func<T, bool>> predicate)
        {
            if (data != null)
            {
                var collection = DB.GetCollection<T>(collectionName);
                var ogValue = collection.Find(predicate).FirstOrDefault();
                if (ogValue != null)
                {
                    collection.ReplaceOne(predicate, data);
                    return true;
                }
                return false;

            }
            return false;
        }
        public bool DeleteOne<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {

            var collection = DB.GetCollection<T>(collectionName);
            collection.DeleteOne(predicate);
            return true;

        }

        public bool DeleteMany<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            var collection = DB.GetCollection<T>(collectionName);
            collection.DeleteMany(predicate);
            return true;
        }
    }
}
