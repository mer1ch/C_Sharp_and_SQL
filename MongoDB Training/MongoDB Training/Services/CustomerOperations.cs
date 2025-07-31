using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_Training.Entities;

namespace MongoDB_Training.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var document = new BsonDocument
            {
                {"CustomerName", customer.CustomerName},
                {"CustomerSurname", customer.CustomerSurname},
                {"CustomerCity", customer.CustomerCity},
                {"CustomerBalance", customer.CustomerBalance},
                {"CustomerShoppingCount", customer.CustomerShoppingCount}
            };
            customerCollection.InsertOne(document);
        }
        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = customer["_id"].ToString(),
                    CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                    CustomerCity = customer["CustomerCity"].ToString(),
                    CustomerName = customer["CustomerName"].ToString(),
                    CustomerSurname = customer["CustomerSurname"].ToString(),
                    CustomerShoppingCount = int.Parse(customer["CustomerShoppingCount"].ToString())
                });
            }
            return customerList;
        }
        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var deleteValue = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(deleteValue);
        }
        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",ObjectId.Parse(customer.CustomerId));
            var updateValue = Builders<BsonDocument>.Update.Set("CustomerName", customer.CustomerName).Set("CustomerSurname", customer.CustomerSurname).Set("CustomerCity", customer.CustomerCity).Set("CustomerBalance", customer.CustomerBalance).Set("CustomerShoppingCount", customer.CustomerShoppingCount);
            customerCollection.UpdateOne(filter, updateValue);
        }
        public Customer GetCustomerById(String id)
        {
            var connection = new MongoDbConnection();
            var customerCollection=connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = customerCollection.Find(filter).FirstOrDefault();
            return new Customer
            {
                CustomerBalance = decimal.Parse(result["CustomerBalance"].ToString()),
                CustomerName = result["CustomerName"].ToString(),
                CustomerSurname = result["CustomerSurname"].ToString(),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerShoppingCount = int.Parse(result["CustomerShoppingCount"].ToString()),
                CustomerId = id
            };
        }
    }
}
