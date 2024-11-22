using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Core.Models.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Gymly.Persistence.Seeds;

public class DapperSeeder
{
    private readonly IServiceProvider _serviceProvider;
    public DapperSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Start(CancellationToken ct = default)
    {
        await SeedMembers(ct);
        await SeedSportClasses(ct);
    }

    private async Task SeedMembers(CancellationToken ct)
    {
        var membersRepository = _serviceProvider.GetRequiredService<IIdentityRepository>();

        var members = new List<Identity>
        {
            new Identity
            {
                Name = "John Doe",
                Email = "json@exmapl",
                Phone = "123456789",
                Password = "123456",
            },
            new Identity
            {
                Name = "Jane Doe 2",
                Email = "jane2@exmapl",
                Phone = "987654321",
                Password = "654321",
            },
            new Identity
            {
                Name = "Jane Doe 3",
                Email = "jane3@exmapl",
                Phone = "987654321",
                Password = "654321",
            }
        };

        foreach (var member in members)
        {
            var result = await membersRepository.Create(member, ct);
            if (!result.IsSuccessful)
            {
                throw new Exception("Failed to seed members");
            }
        }
    }

    private async Task SeedSportClasses(CancellationToken ct)
    {
        var sportClassRepository = _serviceProvider.GetRequiredService<ISportClassRepository>();

        var sportClasses = new List<SportClass>
        {
            new SportClass
            {
                Name = "Yoga",
                Price = 50,
                Date = DateTime.Now,
                InstructorName = "John Doe",
            },
            new SportClass
            {
                Name = "Pilates",
                Price = 40,
                Date = DateTime.Now.AddDays(5),
                InstructorName = "Jane Doe 2",
            },
            new SportClass
            {
                Name = "Crossfit",
                Price = 15,
                Date = DateTime.Now.AddDays(10),
                InstructorName = "John Doe 2",
            }
        };

        foreach (var sportClass in sportClasses)
        {
            var result = await sportClassRepository.Create(sportClass, ct);
            if (!result.IsSuccessful)
            {
                throw new Exception("Failed to seed sport classes");
            }
        }
    }
}
