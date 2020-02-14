using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.ViewModels.Base;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class UserProfilViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _firstName;
        private string _lastName;
        private string _password;
        private string _newPassword;
        private string _confirmPassword;
        private string _email;
        private string _userPhone;
        private bool _startmodify = false;
        private bool _canGo = false;
        private string _position = string.Empty;
        private double _longitude;
        private double _latitude;

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

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
                CanExecute();
            }
        }

        private bool _isDelete = false;
        public bool IsDelete
        {
            get => _isDelete;
            set
            {
                _isDelete = value;
                OnPropertyChanged();
            }
        }

        //public string CommerceName
        //{
        //    get => _commerceName;
        //    set
        //    {
        //        _commerceName = value;
        //        OnPropertyChanged();
        //        CanExecute();
        //    }
        //}

        //public string CommerceLocate
        //{
        //    get => _commerceLocate;
        //    set
        //    {
        //        _commerceLocate = value;
        //        OnPropertyChanged();
        //        CanExecute();
        //    }
        //}

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

        public bool Startmodify
        {
            get => _startmodify;
            set
            {
                _startmodify = value;
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

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand LocaliserCommand => new Command(OnLocaliser);
        public ICommand RegisterCommand => new Command(OnRegister, () => CanGo);
        public ICommand ModifyCommand => new Command(onModifyCommand);
        public ICommand deleteCommand => new Command(OndeleteCommand);

        public UserProfilViewModel(IConnectionService connectionService, INavigationService navigationService, IAuthenticationService authenticationService, ISettingsService settingsService, IDialogService dialogService) : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;

            FirstName = _settingsService.UserNameSetting;
            LastName = _settingsService.UserLastName;
            Email = _settingsService.Email;
            UserPhone = _settingsService.ClientNumberSetting;
            _longitude = _settingsService.Longitude;
            _latitude = _settingsService.Latitude;
            Position = _settingsService.Position;

        }

        private async void OnRegister()
        {
            if (_connectionService.IsConnected)
            {
                var authenticationResponse = await _authenticationService.Authenticate(_settingsService.Email, Password);

                if (authenticationResponse == null)
                {
                    await _dialogService.ShowDialog(
                            "Impossible de se connecter au serveur",
                            "Erreur",
                            "OK");
                    IsBusy = true;
                    return;
                }

                //if (authenticationResponse.IsAuthenticated)
                //{


                //if (IsValidEmail(Email))
                //{
                var userRegistered = await
            _authenticationService.Register(_firstName, _lastName, _email, false, _userPhone, _longitude, _latitude, Position, _password, false, string.Empty);
                if (userRegistered == null)
                {
                    await _dialogService.ShowDialog(
                            "Impossible de se connecté au serveur",
                            "Erreur",
                            "OK");
                    IsBusy = true;
                    return;
                }

                if (userRegistered.IsAuthenticated)
                {
                    await _dialogService.ShowDialog("Modification réussi", "", "OK");
                    _settingsService.UserIdSetting = userRegistered.User.Id;
                    _settingsService.ClientNumberSetting = userRegistered.User.UserPhone;
                    _settingsService.UserNameSetting = userRegistered.User.FirstName;
                    _settingsService.Email = userRegistered.User.Email;
                    _settingsService.UserLastName = userRegistered.User.LastName;
                    _settingsService.Latitude = userRegistered.User.Latitude;
                    _settingsService.Longitude = userRegistered.User.Longitude;
                    _settingsService.Position = userRegistered.User.PositionGeo;
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                }
                else
                {
                    await _dialogService.ShowDialog("Verifiez les information saisir", "Erreur", "OK");
                    return;
                }
                //}
                //else
                //{
                //    await _dialogService.ShowDialog("Votre mail n'est pas correcte ", "", "OK");
                //    return;
                //}



            }

            else
            {
                await _dialogService.ShowDialog("Vérifier votre connexion internet", "", "OK");
            }
        }

        private void onModifyCommand()
        {
            Startmodify = true;
        }

        private async void OnLocaliser()
        {
            OnlocalisationLoad = true;

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync();
                var address = await locator.GetAddressesForPositionAsync(position);
                var correct = address.FirstOrDefault();
                if (correct != null)
                {
                    Position = string.Concat(correct.AdminArea + ", " + correct.CountryName + ", " + correct.Locality + ", " + correct.SubLocality);
                    _longitude = correct.Longitude;
                    _latitude = correct.Latitude;
                    CanExecute();
                }
                else
                {
                    await _dialogService.ShowDialog("Les informations n'ont pas pu être chargés, veuillez réessayer", "Erreur", "OK");
                }
            }
            catch (Exception)
            {

                await _dialogService.ShowDialog("Les informations n'ont pas pu être chargés, veuillez réessayer", "Erreur", "OK");
                OnlocalisationLoad = false;
            }


            OnlocalisationLoad = false;

        }

        private async void OndeleteCommand()
        {
            IsDelete = true;
            var id = _settingsService.UserIdSetting;
            try
            {

                var response = _authenticationService.DeleteMyCompte(id);
                if (response == null)
                {
                    await _dialogService.ShowDialog("Erreur survenu lors de la suppression de l'utilisateur,", "Erreur", "OK");
                    IsDelete = false;
                    return;
                }

                _settingsService.UserIdSetting = "";
                _settingsService.ClientNumberSetting = "";
                _settingsService.UserNameSetting = "";
                _settingsService.Email = "";
                _settingsService.UserLastName = "";
                _settingsService.Latitude = default;
                _settingsService.Longitude = default;
                _settingsService.Position = default;

                IsDelete = false;
                await _dialogService.ShowDialog("Suppression reussie", "", "OK");
                await _navigationService.NavigateToAsync<LoginViewModel>();
            }
            catch (Exception)
            {

                await _dialogService.ShowDialog("Erreur survenu lors de la suppression de l'utilisateur,", "Erreur", "OK");
                IsDelete = false;
                return;
            }
        }

        private void CanExecute()
        {
            CanGo = !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(UserPhone));
        }
    }
}
