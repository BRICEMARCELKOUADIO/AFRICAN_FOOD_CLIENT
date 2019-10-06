﻿using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.ViewModels.Base;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _firstName;
        private string _lastName;
        private string _password;
        private string _email;
        private string _userPhone;
        private bool _canGo = false;
        private string _position = string.Empty;
        private double _longitude;
        private double _latitude;

        public RegistrationViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            IAuthenticationService authenticationService, ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
                CanExecute();

            }
        }

        private bool _onlocalisationLoad = false;
        public bool OnlocalisationLoad
        {
            get => _onlocalisationLoad;
            set
            {
                _onlocalisationLoad = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
                CanExecute();

            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                CanExecute();

            }
        }

        public bool CanGo
        {
            get
            {
                return _canGo;
            }
            set
            {
                _canGo = value;
                OnPropertyChanged();
            }
        }

        public string UserPhone
        {
            get => _userPhone;
            set
            {
                _userPhone = value;
                OnPropertyChanged();
                CanExecute();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                CanExecute();
            }
        }

        public ICommand RegisterCommand => new Command(OnRegister, () => CanGo);
        public ICommand LocaliserCommand => new Command(OnLocaliser);
        public ICommand LoginCommand => new Command(OnLogin);

        private async void OnRegister()
        {
            if (_connectionService.IsConnected)
            {
                //if (IsValidEmail(Email))
                //{
                    var userRegistered = await
                    _authenticationService.Register(_firstName, _lastName, _email, false, _userPhone,_longitude,_latitude,Position, _password); ;

                if (_authenticationService == null)
                {
                    await _dialogService.ShowDialog(
                            "Impossible de se connecter au serveur",
                            "Erreur",
                            "OK");
                    IsBusy = true;
                    return;
                }

                if (userRegistered.IsAuthenticated)
                    {
                        await _dialogService.ShowDialog("inscription réussi", "", "OK");
                        _settingsService.UserIdSetting = userRegistered.User.Id;
                        await _navigationService.NavigateToAsync<LoginViewModel>();
                    }
                    else
                    {
                        await _dialogService.ShowDialog("Ce utilisateur existe déjà", "", "OK");
                        return;
                    }
              }
                //else
                //{
                //    await _dialogService.ShowDialog("Votre mail n'est pas correcte", "", "OK");
                //    return;
                //}
                
            //}
            else
            {
                await _dialogService.ShowDialog("Vérifier votre connexion internet", "", "OK");
            }
        }


        private async void OnLocaliser()
        {
            OnlocalisationLoad = true;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync();
            var address = await locator.GetAddressesForPositionAsync(position);

            var correct = address.FirstOrDefault();
            if (correct != null)
            {
                Position = string.Concat(correct.CountryName + " " + correct.Locality);
                _longitude = correct.Longitude;
                _latitude = correct.Latitude;
            }
            OnlocalisationLoad = false;

        }

        bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }


        private void CanExecute()
        {
            CanGo = !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(UserPhone));
        }

        private void OnLogin()
        {
            _navigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}
