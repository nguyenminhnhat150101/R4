using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.loaisanpham
{
    public partial class LoaiSanPhamDialog
    {
        [Parameter]
        public string id { get; set; }
        private string Tieude = "";
        public LoaiSanPham loaiSanPham = new LoaiSanPham();
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                Tieude = "Thêm loại sản phẩm";
                loaiSanPham = new LoaiSanPham();
            }
        }
        public async void SubmitForm()
        {
            var res = await _apiHelper.PostRequestAsync("loaiSanPhams",loaiSanPham ,null);
            if (res != "-1")
            {
                Snackbar.Add("Thêm thành công", MudBlazor.Severity.Success);
                await Task.Delay(3000);
                navigation.NavigateTo("/loaisanphamlist");
            }
            else
            {
                Snackbar.Add("Thêm thất bại", MudBlazor.Severity.Error);
            }          
        }
    }
}
