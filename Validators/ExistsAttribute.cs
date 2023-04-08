using CRM_MongoDB.Configurations.Mongo;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.Validators
{
    public class ExistsAttribute : ValidationAttribute
    {
        public string CollectionName { get; set; }
        public string Key { get; set; }

        private readonly IMongoDatabase database = new MongoClient(MongoDBSettings.CONNECTION).GetDatabase(MongoDBSettings.DATABASENAME);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string filter = "{ key : value }";

            filter = filter.Replace("key", Key);
            
            if(Key == "_id")
            {
                filter = filter.Replace("value" , $"ObjectId('{value.ToString()}')");
            }
            else
            {
                filter = filter.Replace("value", @"""" + value.ToString() + @"""");
            }

            Console.WriteLine(filter);
            try
            {
                long count = database.GetCollection<BsonDocument>(CollectionName).CountDocuments(filter);
                if (count == 0)
                {
                    return new ValidationResult("Not exists preoperty");
                }
                return ValidationResult.Success;
            }
            catch(Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }
    }
}
