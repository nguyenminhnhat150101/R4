using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.loaisanpham
{
    public partial class LoaiSanPhamList
    {
        private string searchString = "";
        public IEnumerable<LoaiSanPham> LoaiSanPhams = new List<LoaiSanPham>();

        protected override async Task OnInitializedAsync()
        {
            var res = await _apiHelper.GetRequestAsync("LoaiSanPhams", null);
            if (res != "-1")
            {
                LoaiSanPhams = JsonConvert.DeserializeObject<List<LoaiSanPham>>(res);
            }
        }
        private bool FilterFunc1(LoaiSanPham loaiSanPham) => FilterFunc(loaiSanPham, searchString);
        private bool FilterFunc(LoaiSanPham loaiSanPham, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (loaiSanPham.Tenloai.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{loaiSanPham.Maloai}".Contains(searchString))
                return true;
            return false;
        }
    }
}
