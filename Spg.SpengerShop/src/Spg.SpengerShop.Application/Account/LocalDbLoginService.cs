﻿using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Account
{
    public class LocalDbLoginService : IAuthService
    {
        public UserInformationDto Login(string username, string password, string role)
        {
            // TODO: In DB nachsehen, ob es Username (+PWD) gibt + Datensatz zurückgeben.

            UserInformationDto dto = new UserInformationDto()
            {
                UserName = username,
                FirstName = "Hans",
                LastName = "Reinsch",
                EMail = "",
                Role = role
            };
            dto.Signature = HashHelper.CalculateHash($"{dto.FullName}-{dto.UserName}-{dto.Role}", "gI976UUn3/m59A==");
            return dto;
        }
    }
}
