using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Helpers
{
    public interface IAPIHelper
    {
        public Task<string> PostRequestAsync(string url, object postData, string token);
        public Task<string> PuttRequestAsync(string url, object postData, string token);
        public Task<string> GetRequestAsync(string url, string token);
    }
}
