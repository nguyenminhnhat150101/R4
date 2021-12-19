using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.sanpham
{
    public partial class SanPhamList
    {
        private string searchString = "";
        public IEnumerable<SanPham> sanPhams = new List<SanPham>();
        protected override async Task OnInitializedAsync()
        {
            var res = await _apiHelper.GetRequestAsync("Sanphams/dssanpham", null);
            if (res != "-1")
            {
                
                sanPhams = JsonConvert.DeserializeObject<List<SanPham>>(res);
            }
        }
        private bool FilterFunc1(SanPham element) => FilterFunc(element, searchString);
        private bool FilterFunc(SanPham element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Tensanpham.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Maloai}".Contains(searchString))
                return true;
            return false;
        }
    }
}
