using System;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

namespace AsyncTest
{
    public sealed class AsyncTest
    {
        private const int Iterations = 30;
        private readonly IMongoCollection<BsonDocument> _collection;

        public AsyncTest()
        {
            const string ConnectionString = "mongodb://db1/";
            _collection = new MongoClient(ConnectionString).GetDatabase("test").GetCollection<BsonDocument>("test");
        }

        public void Initialize()
        {
            _collection.DeleteMany(Builders<BsonDocument>.Filter.Empty);
            _collection.InsertOne(new BsonDocument
            {
                ["a"] = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789",
                ["b"] = long.MaxValue,
                ["c"] = int.MaxValue,
            });
        }

        public void TestSync()
        {
            for (var i = 0; i < Iterations; i++)
            {
                _collection.Find(Builders<BsonDocument>.Filter.Empty).SingleOrDefault();
            }
        }

        public async Task TestAsync()
        {
            for (var i = 0; i < Iterations; i++)
            {
                await _collection.Find(Builders<BsonDocument>.Filter.Empty).SingleOrDefaultAsync().ConfigureAwait(false);
            }
        }
    }
}
