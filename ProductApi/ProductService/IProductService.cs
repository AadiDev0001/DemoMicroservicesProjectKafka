using Confluent.Kafka;
using Shared;
using System.Text.Json;

namespace ProductApi.ProductService
{
    public interface IProductService
    {
        Task AddProduct(Product item);
        Task DeleteProduct(int id);

    }

}
