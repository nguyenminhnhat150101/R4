using MudBlazor;
using Newtonsoft.Json;
using R4ClothesServer.Models;
using R4ClothesServer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages
{
    public partial class ThongKe
    {
        string from = DateTime.Now.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1 + "/MM/dd");
        string to = DateTime.Now.ToString("yyyy/MM/dd");
        string tu = DateTime.Now.ToString("yyyy/MM/dd");
        string den = DateTime.Now.ToString("yyyy/MM/dd");
        public double total = 0;
        public string dt;
        public bool flag = true;
        public bool flag2 = true;      
        public List<HoaDon> DoanhThu = new List<HoaDon>();
        public IEnumerable<HoaDon> DoanhThus = new List<HoaDon>();
        public IEnumerable<KhachHangTT> KhachHangs = new List<KhachHangTT>();
        public IEnumerable<SanPham> BanDuoc = new List<SanPham>();
        protected override async Task OnInitializedAsync()
        {
            flag = false;
            flag2 = true;
            var res = await _apiHelper.GetRequestAsync("thongke/doanhthu?from=" + from + "&to=" + to, null);
            if (res != "-1")
            {
                DoanhThus = JsonConvert.DeserializeObject<List<HoaDon>>(res);
            }
            var res1 = await _apiHelper.GetRequestAsync("thongke/specialcus", null);
            if (res1 != "-1")
            {
                KhachHangs = JsonConvert.DeserializeObject<List<KhachHangTT>>(res1);
            }
            var res2 = await _apiHelper.GetRequestAsync("thongke/banduoc", null);
            if (res2 != "-1")
            {
                BanDuoc = JsonConvert.DeserializeObject<List<SanPham>>(res2);
            }
            total = DoanhThus.Sum(item => item.Tongtien);
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            dt = total.ToString("#,###", cul.NumberFormat);
        }
        public async void tim()
        {
            flag = true;
            flag2 = false;           
            var res3 = await _apiHelper.GetRequestAsync("thongke/doanhthu?from=" + tu + "&to=" + den, null);
            if (res3 != "-1")
            {
                DoanhThus = JsonConvert.DeserializeObject<List<HoaDon>>(res3);
            }
            total = DoanhThus.Sum(item => item.Tongtien);
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            if (total != 0)
            {            
                dt = total.ToString("#,###", cul.NumberFormat);
            }
            else
            {
                dt = "0";
            }         
        }
    }
}
