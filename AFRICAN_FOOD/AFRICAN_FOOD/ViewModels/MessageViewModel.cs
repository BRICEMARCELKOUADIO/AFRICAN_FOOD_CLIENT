﻿using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationContext _applicationContext;
        private readonly INavigationService _navigationService;
        private readonly ITchatDataService _tchatDataService;


        private User _userRecever = new User();
        public User UserRecever
        {
            get { return _userRecever; }
            set
            {
                _userRecever = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<MessageDetail> _messageDetails = new ObservableCollection<MessageDetail>();
        public ObservableCollection<MessageDetail> MessageDetails
        {
            get => _messageDetails;
            set
            {
                _messageDetails = value;
                OnPropertyChanged();
            }
        }

        private Message _messages = new Message();
        public Message Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }


        private string _outGoingText;
        public string OutGoingText
        {
            get { return _outGoingText; }
            set
            {
                _outGoingText = value;
                OnPropertyChanged();

            }
        }
        public ICommand SendCommand => new Command(OnsendMessage);
        public MessageViewModel(IConnectionService connectionService,IApplicationContext applicationContext, ITchatDataService tchatDataService, INavigationService navigationService, ISettingsService settingsService, IAuthenticationService authenticationService, IDialogService dialogService) : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            _tchatDataService = tchatDataService;
            _applicationContext = applicationContext;
            Messages = new Message();
            MessageDetails = new ObservableCollection<MessageDetail>();
        }

        public override async Task InitializeAsync(object user)
        {
            //_applicationContext.HandleNotification += HandleNotification;
            IsBusy = true;
            UserRecever = (User)user;
            GetMessages();
        }

        private async void GetMessages()
        {
            IsBusy = true;
            Messages = await _tchatDataService.GetMessages(UserRecever.Id, _settingsService.UserIdSetting);
            if (Messages != null && Messages.Details != null)
            {
                foreach (var item in Messages.Details)
                {
                    MessageDetails.Add(item);
                }
            }
            IsBusy = false;
        }

        private async void OnsendMessage()
        {
            var messagd = new MessageDetail()
            {
                MessageId = Messages.MessageId.ToString(),
                Author = _settingsService.UserNameSetting,
                IsIncoming = false,
                Text = OutGoingText,
                MessageDateTime = DateTime.Now,

            };
            MessageDetails.Add(messagd);
            OutGoingText = "";
            await _tchatDataService.SendMessage(messagd);

        }

        //public void OnAppearing()
        //{
        //    _applicationContext.HandleNotification += HandleNotification;
        //}

        //public void OnDisappearing()
        //{
        //    _applicationContext.HandleNotification -= HandleNotification;
        //}

        //private void HandleNotification(object sender, EventArgs e)
        //{
        //    MessageDetails.Clear();
        //    GetMessages();

        //}
    }
}
