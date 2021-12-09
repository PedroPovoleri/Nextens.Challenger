using Nextens.Challenger.Business.Interface;
using Nextens.Challenger.Context.Interface;
using Nextens.Challenger.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nextens.Challenger.Business
{
    public class MessagesBusiness : IMessagesBusiness
    {
        private readonly ILoadData loadData;

        public MessagesBusiness(ILoadData loadData)
        {
            this.loadData = loadData;
        }
        public List<Client> GetMessageById(Guid messageId)
        {
            return this.loadData.LoadDataset().Where(x => x.Id == messageId).ToList();
        }

        public List<Client> GetMessagesFromClient(Guid clientId)
        {
            return this.loadData.LoadDataset().Where(x => x.ClientId == clientId).ToList();
        }
    }
}
