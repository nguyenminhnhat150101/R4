using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.khachhang
{
    public partial class KhachHangList
    {
        private string searchString = "";
        public IEnumerable<KhachHang> khachHangs = new List<KhachHang>();
        protected override async Task OnInitializedAsync()
        {
            var res = await _apiHelper.GetRequestAsync("QuanTris/khachhang/ds", null);
            if (res != "-1")
            {
                khachHangs = JsonConvert.DeserializeObject<List<KhachHang>>(res);
            }
        }
        private bool FilterFunc1(KhachHang khachhang) => FilterFunc(khachhang, searchString);
        private bool FilterFunc(KhachHang khachhang, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (khachhang.Tenkhachhang.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (khachhang.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (khachhang.Diachi.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (khachhang.Sodienthoai.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{khachhang.Makhachhang}".Contains(searchString))
                return true;
            return false;
        }
    }
}
