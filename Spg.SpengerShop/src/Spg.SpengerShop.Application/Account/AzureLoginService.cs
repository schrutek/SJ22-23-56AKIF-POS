using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Account
{
    public class AzureLoginService : IAuthService
    {
        public UserInformationDto Login(string username, string password, string role)
        {
            throw new NotImplementedException();
        }
    }
}
