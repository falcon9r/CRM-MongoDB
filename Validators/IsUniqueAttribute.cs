using CRM_MongoDB.Configurations.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CRM_MongoDB.Validators
{
    public class IsUniqueAttribute : ValidationAttribute
    {
        public string CollectionName { get; set; }
        public string Key { get; set; }

        private readonly IMongoDatabase database = new MongoClient(MongoDBSettings.CONNECTION).GetDatabase(MongoDBSettings.DATABASENAME);
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string filter = "{ key : \"value\"}";
            
            filter = filter.Replace("key", Key);
            filter = filter.Replace("value", value.ToString());

            Console.WriteLine(filter);
            long count = database.GetCollection<BsonDocument>(CollectionName).CountDocuments(filter);
            if(count > 0)
            {
                return new ValidationResult("Must be unique");
            }
            return ValidationResult.Success;
        }
    }
}
