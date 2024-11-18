using Gymly.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymly.Infrastructure.Abstractions;

public interface IIdentityManager
{
    Identity GetCurrentUser();
}
