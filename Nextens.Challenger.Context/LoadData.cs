using Newtonsoft.Json;
using Nextens.Challenger.Context.Interface;
using Nextens.Challenger.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nextens.Challenger.Context
{
    public class LoadData : ILoadData
    {
        public List<Client> LoadDataset()
        {
            var lstClient = new List<Client>();


            try
            {
                string[] dirs = Directory.GetFiles(@"..\Nextens.Challenger.Context\blobs", "*.json");
                foreach (string dir in dirs)
                {
                    lstClient.Add(JsonConvert.DeserializeObject<Client>(File.ReadAllText(dir)));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstClient;

        }
    }
}
