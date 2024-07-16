using Store.Domain;

namespace Store.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Product> ProductRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public Task<int> SaveAsync();
    }
}
