using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Contracts.Services.Data
{
    public interface ITchatDataService
    {
        Task<IEnumerable<User>> GetAllAdmin();
        Task<MessageDetail> SendMessage(MessageDetail messageDetail);
        Task<Message> GetMessages(string Userid, string idRecever);

    }
}
