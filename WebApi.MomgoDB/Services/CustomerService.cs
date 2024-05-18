using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.MomgoDB.Models;

namespace WebApi.MomgoDB.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _customers = database.GetCollection<Customer>("customers");
        }

        public async Task<List<Customer>> GetAsync() =>
            await _customers.Find(c => true).ToListAsync();

        public async Task<Customer> GetAsync(Guid id) =>
            await _customers.Find<Customer>(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer customer) =>
            await _customers.InsertOneAsync(customer);

        public async Task UpdateAsync(Guid id, Customer customerIn) =>
            await _customers.ReplaceOneAsync(c => c.Id == id, customerIn);

        public async Task RemoveAsync(Guid id) =>
            await _customers.DeleteOneAsync(c => c.Id == id);
    }
}
