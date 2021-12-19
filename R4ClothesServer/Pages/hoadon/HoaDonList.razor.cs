using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.hoadon
{
    public partial class HoaDonList
    {
        private int tt;
        private string searchString1 = "";
        private string searchString2 = "";
        private HoaDon selectedItem1 = null;
        private HashSet<HoaDon> selectedItems = new HashSet<HoaDon>();

        public IEnumerable<HoaDon> HoaDons = new List<HoaDon>();

        protected override async Task OnInitializedAsync()
        {
            var res = await _apiHelper.GetRequestAsync("QuanTris/hoadon/getall", null);
            if (res != "-1")
            {
                HoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(res);
            }
        }
        public async void hoadontheott()
        {
            var res3 = await _apiHelper.GetRequestAsync("QuanTris/dshdtheott?tt=" + tt, null);
            if (res3 != "-1")
            {
                HoaDons = JsonConvert.DeserializeObject<List<HoaDon>>(res3);
            }
        }
        private bool FilterFunc1(HoaDon HoaDon) => FilterFunc(HoaDon, searchString1);
        private bool FilterFunc2(HoaDon HoaDon) => FilterFunc(HoaDon, searchString2);

        private bool FilterFunc(HoaDon HoaDon, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (Convert.ToString(HoaDon.Ngaydat).Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{HoaDon.Mahoadon}{HoaDon.Makhachhang}{HoaDon.Nguoiquantri}".Contains(searchString))
                return true;
            return false;
        }
    }
}
