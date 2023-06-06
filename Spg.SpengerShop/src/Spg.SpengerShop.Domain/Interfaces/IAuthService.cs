using Spg.SpengerShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IAuthService
    {
        (bool isLoggedIn, UserInformationDto? dto, string message) Login(string username, ReadOnlySpan<char> password, string role);
    }
}
