﻿using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(string firstName, string lastName, string email, bool typeuser, string userName, double longitude, double latitude, string position, string password, bool modify, string oldEmail);

        Task<AuthenticationResponse> Authenticate(string Email, string password);
        Task<AuthenticationResponse> DeleteMyCompte(string id);


        bool IsUserAuthenticated();
    }
}
