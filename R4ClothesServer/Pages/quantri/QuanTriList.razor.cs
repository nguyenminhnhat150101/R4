using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.quantri
{
    public partial class QuanTriList
    {
        protected string imgUrl = "";
        private string searchString1 = "";
        private string searchString2 = "";
        private QuanTri selectedItem1 = null;
        public HashSet<QuanTri> selectedItems = new HashSet<QuanTri>();
        public IEnumerable<QuanTri> quanTris = new List<QuanTri>();
        protected override async Task OnInitializedAsync()
        {
            var res = await _apiHelper.GetRequestAsync("QuanTris/dsqt", null);
            if (res != "-1")
            {
                quanTris = JsonConvert.DeserializeObject<List<QuanTri>>(res);
            }
        }
        private bool FilterFunc1(QuanTri quanTri) => FilterFunc(quanTri, searchString1);
        private bool FilterFunc2(QuanTri quanTri) => FilterFunc(quanTri, searchString2);
        private bool FilterFunc(QuanTri quanTri, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (quanTri.Hoten.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (quanTri.Taikhoan.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{quanTri.Maquantri}".Contains(searchString))
                return true;
            return false;
        }
    }
}
