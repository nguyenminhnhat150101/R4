using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using R4ClothesServer.Models;
using R4ClothesServer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.hoadon
{
    public partial class HoaDonDetails
    {
        [Parameter]
        public string id { get; set; }
        public HoaDon hoaDon = new HoaDon();
        public IEnumerable<SanPhamCT> sanPhamCTs = new List<SanPhamCT>();
        protected override async Task OnInitializedAsync()
        {
            //chitiethoadons
            var res1 = await _apiHelper.GetRequestAsync("chitiethoadons/hoadon/" + id, null);
            if (res1 != "-1")
            {
                sanPhamCTs = JsonConvert.DeserializeObject< List<SanPhamCT>>(res1);
            }
            //HoaDons
            var res2 = await _apiHelper.GetRequestAsync("HoaDons/" + id, null);
            if (res2 != "-1")
            {
                hoaDon = JsonConvert.DeserializeObject<HoaDon>(res2);
            }
        }
    }
}
