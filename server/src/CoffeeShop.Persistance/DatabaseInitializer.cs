namespace CoffeeShop.Persistance
{
    using CoffeeShop.Core.AuthContext.Commands;
    using CoffeeShop.Core.AuthContext;
    using CoffeeShop.Core.WaiterContext.Commands;
    using CoffeeShop.Domain.Entities;
    using CoffeeShop.Persistance.EntityFramework;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private static readonly List<Barista> _baristas = new List<Barista>
        {
            new Barista
            {
                ShortName = "John"
            },
            new Barista
            {
                ShortName = "James"
            }
        };

        private static readonly List<Cashier> _cashiers = new List<Cashier>
        {
            new Cashier
            {
                ShortName = "Steve"
            }
        };

        private static readonly List<Manager> _managers = new List<Manager>
        {
            new Manager
            {
                ShortName = "Jamie"
            }
        };

        private static readonly List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem
            {
                Description = "Coffee",
                Number = 1,
                Price = 1
            },
            new MenuItem
            {
                Description = "Tea",
                Number = 2,
                Price = 1
            },
            new MenuItem
            {
                Description = "Coke",
                Number = 3,
                Price = 2
            },
            new MenuItem
            {
                Description = "Fanta",
                Number = 4,
                Price = 2
            },
            new MenuItem
            {
                Description = "Latte",
                Number = 5,
                Price = 2.5m
            }
        };

        private static readonly List<Table> _tables = new List<Table>
        {
            new Table
            {

                Number = 1
            },
            new Table
            {

                Number = 2
            },
            new Table
            {

                Number = 3
            },
            new Table
            {

                Number = 4
            }
        };

        private static readonly List<Waiter> _waiters = new List<Waiter>
        {
            new Waiter
            {

                ShortName = "Pete"
            },
            new Waiter
            {

                ShortName = "James"
            },
            new Waiter
            {

                ShortName = "Steve"
            }
        };

        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public DatabaseInitializer(IMediator mediator, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task SeedDatabase()
        {
            await _dbContext.Database.MigrateAsync().ConfigureAwait(false);

            if (!DatabaseIsEmpty())
               return;

             _dbContext.AddRange(_menuItems);
             _dbContext.AddRange(_baristas);
             _dbContext.AddRange(_waiters);
             _dbContext.AddRange(_tables);
             _dbContext.AddRange(_managers);

            await _dbContext.SaveChangesAsync();

            await AssignWaitersToTables(_waiters, _tables);

            await RegisterAccount("waiter@cafe.org", "Password123$", new Claim(AuthConstants.ClaimTypes.WaiterId, _waiters[0].Id.ToString()));
            await RegisterAccount("cashier@cafe.org", "Password123$", new Claim(AuthConstants.ClaimTypes.CashierId, _cashiers[0].Id.ToString()));
            await RegisterAccount("barista@cafe.org", "Password123$", new Claim(AuthConstants.ClaimTypes.BaristaId, _baristas[0].Id.ToString()));
            await RegisterAccount("manager@cafe.org", "Password123$", new Claim(AuthConstants.ClaimTypes.ManagerId, _managers[0].Id.ToString()));
        }

        private async Task AssignWaitersToTables(List<Waiter> waiters, List<Table> tables)
        {
            if (tables.Count < waiters.Count)
                throw new InvalidOperationException("You need at least as much tables as waiters.");

            for (int i = 0; i < waiters.Count; i++)
            {
                var waiter = waiters[i];
                var table = tables[i];

                var assignCommand = new AssignTable
                {
                    WaiterId = waiter.Id,
                    TableNumber = table.Number
                };

                var result = await _mediator.Send(assignCommand);
            }
        }

        private bool DatabaseIsEmpty() =>
            !_dbContext.MenuItems.Any() &&
            !_dbContext.Waiters.Any() &&
            !_dbContext.Cashiers.Any();

        private async Task RegisterAccount(string email, string password, params Claim[] claims)
        {
            var registerCommand = new Register
            {
               // Id = Guid.NewGuid(),
                Email = email,
                Password = password,
                FirstName = "Test",
                LastName = "Account"
            };

            await _mediator.Send(registerCommand);

            var user = await _userManager
                .FindByEmailAsync(registerCommand.Email);

            await _userManager.AddClaimsAsync(user, claims);
        }


    }
}
