﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Contracts.Services.General
{
    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);
        void AddItem2(string key, double value);
        double GetItem2(string key);
        string UserNameSetting { get; set; }
        double Longitude { get; set; }
        string UserLastName { get; set; }
        double Latitude { get; set; }
        string Position { get; set; }
        string Email { get; set; }
        string UserIdSetting { get; set; }
        string ClientNumberSetting { get; set; }
    }
}
