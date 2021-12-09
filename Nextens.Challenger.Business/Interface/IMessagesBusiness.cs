using Nextens.Challenger.Model;
using System;
using System.Collections.Generic;

namespace Nextens.Challenger.Business.Interface
{
    public interface IMessagesBusiness
    {
        public List<Client> GetMessagesFromClient(Guid clientId);
        public List<Client> GetMessageById(Guid messageId);
        
    }
}
