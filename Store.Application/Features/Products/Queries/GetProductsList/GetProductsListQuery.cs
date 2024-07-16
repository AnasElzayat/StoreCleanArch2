using MediatR;
using Store.Domain;
using System.Linq.Expressions;

namespace Store.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<IEnumerable<GetProductsListViewModel>>
    {
        public Expression<Func<Product, bool>> Filter { get; set; }

        public GetProductsListQuery(Expression<Func<Product, bool>> filter = null)
        {
            Filter = filter;
        }
    }
}
