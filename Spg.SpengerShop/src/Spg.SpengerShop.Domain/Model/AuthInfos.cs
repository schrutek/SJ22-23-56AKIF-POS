using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public record AuthInfos(string UserName, string Role, string FirstName, string LastName);
}
