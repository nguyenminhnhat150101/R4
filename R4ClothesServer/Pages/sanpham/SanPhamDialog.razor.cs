using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.sanpham
{
    public partial class SanPhamDialog
    {
        [Parameter]
        public string id { get; set; }
        private string Tieude = "";
        public string url;
        public string path;
        IList<IBrowserFile> files = new List<IBrowserFile>();
        public SanPham sanPham = new SanPham();
        public IEnumerable<LoaiSanPham> LoaiSanPhams = new List<LoaiSanPham>();
        protected override async Task OnInitializedAsync()
        {
            var res = await _apiHelper.GetRequestAsync("LoaiSanPhams", null);
            if (res != "-1")
            {
                LoaiSanPhams = JsonConvert.DeserializeObject<List<LoaiSanPham>>(res);
            }

            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                Tieude = "Thêm sản phẩm";
                sanPham = new SanPham();
                sanPham.Ngaynhap = DateTime.Now;
            }
            else
            {
                Tieude = "Sửa sản phẩm";
                var res1 = await _apiHelper.GetRequestAsync("Sanphams/" + id, null);
                if (res1 != "-1")
                {
                    sanPham = JsonConvert.DeserializeObject<SanPham>(res1);
                    url = "https://res.cloudinary.com/r4clothes/image/upload/" + sanPham.Hinh;
                }
            }
        }

        public async void SubmitForm()
        {
            sanPham.Hinh = path;           
            if (sanPham.Masanpham == 0)
            {             
                var res = await _apiHelper.PostRequestAsync("quantris/sanpham/add", sanPham, null);
                if (res != "-1")
                {                   
                    Snackbar.Add("Thêm thành công", MudBlazor.Severity.Success);
                    await Task.Delay(3000);
                    navigation.NavigateTo("/sanphamlist");
                }
                else
                {
                    Snackbar.Add("Thêm thất bại", MudBlazor.Severity.Error);
                }
            }
            else
            {
                sanPham.Hinh = url.Substring(50);
                var res = await _apiHelper.PostRequestAsync("quantris/sanpham/edit?idsp=" + id, sanPham, null);
                if (res != "-1")
                {
                    Snackbar.Add("Sửa thành công", MudBlazor.Severity.Success);
                    await Task.Delay(3000);
                    navigation.NavigateTo("/sanphamlist");
                }
                else
                {
                    Snackbar.Add("Sửa thất bại", MudBlazor.Severity.Error);
                }
            }
        }
        private void Cancel()
        {
            navigation.NavigateTo("SanPhamList", true);
        }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var item in e.GetMultipleFiles(2))
            {
                files.Add(item);
                url = await _uploadImage.GetUrlImage(item);
                path = url.Substring(50);
            }
            var file = files.FirstOrDefault();
            //TODO upload the files to the server
        }
    }
}
