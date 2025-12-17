using Confluent.Kafka;
using Shared;
using System.Text.Json;

namespace ProductApi.ProductService
{
    public class ProductService(IProducer<Null, string> producer) : IProductService
    {
        private List<Product> products = [];
        public async Task AddProduct(Product item)
        {
            products.Add(item);

            var result = await producer.ProduceAsync("add-product-topic", new Message<Null, string> { Value = JsonSerializer.Serialize(item) });
            if (result.Status != PersistenceStatus.Persisted)
            {

                var Lastproduct = products.Last();
                products.Remove(Lastproduct);

            }
        }

        public async Task DeleteProduct(int id)
        {
            products.Remove(products.FirstOrDefault(s => s.Id == id)!);
            await producer.ProduceAsync("delete-product-topic", new Message<Null, string> { Value = id.ToString() });
        }
    }
}
