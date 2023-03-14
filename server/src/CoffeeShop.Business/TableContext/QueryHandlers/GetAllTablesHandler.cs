using CoffeeShop.Core;
using CoffeeShop.Core.TableContext.Queries;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Domain.Views;

namespace CoffeeShop.Business.TableContext.QueryHandlers
{
    public class GetAllTablesHandler : IQueryHandler<GetAllTables, IList<TableView>>
    {
        private readonly ITableViewRepository _tableViewRepository;

        public GetAllTablesHandler(ITableViewRepository tableViewRepository)
        {
            _tableViewRepository = tableViewRepository;
        }

        public Task<IList<TableView>> Handle(GetAllTables request, CancellationToken cancellationToken) =>
            _tableViewRepository
                .GetAll();
    }
}