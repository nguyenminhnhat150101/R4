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

namespace R4ClothesServer.Pages.khachhang
{
    public partial class KhachHangDialog
    {
        [Parameter]
        public string id { get; set; }
        public string _gioitinh { get; set; }
        public string url;
        public string path;
        IList<IBrowserFile> files = new List<IBrowserFile>();
        private string Tieude = "";
        public KhachHang khachHang = new KhachHang();
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                Tieude = "Thêm khách hàng";
                khachHang = new KhachHang();
            }
            else
            {
                Tieude = "Sửa thông tin khách hàng";
                
                var res = await _apiHelper.GetRequestAsync("khachhangs/" + id, null);
                if (res != "-1")
                {
                    khachHang = JsonConvert.DeserializeObject<KhachHang>(res);
                    url = "https://res.cloudinary.com/r4clothes/image/upload/" + khachHang.Hinh;
                }
            }
        }
        public async void SubmitForm()
        {
            khachHang.Hinh = path;
            if (khachHang.Makhachhang == 0)
            {
                var res = await _apiHelper.PostRequestAsync("KhachHangs", khachHang, null);
                if (res != "-1")
                {
                    Snackbar.Add("Thêm thành công", MudBlazor.Severity.Success);
                    await Task.Delay(3000);
                    navigation.NavigateTo("/khachhanglist");
                }
                else
                {
                    Snackbar.Add("Thêm thất bại", MudBlazor.Severity.Error);
                }
            }
            else
            {
                khachHang.Hinh = url.Substring(50);
                var res = await _apiHelper.PuttRequestAsync("KhachHangs/" + id, khachHang, null);
                if (res != "-1")
                {
                    Snackbar.Add("Sửa thành công", MudBlazor.Severity.Success);
                    await Task.Delay(3000);
                    navigation.NavigateTo("/khachhanglist");
                }
                else
                {
                    Snackbar.Add("Sửa thất bại", MudBlazor.Severity.Error);
                }
            }
        }
        private void Cancel()
        {
            navigation.NavigateTo("KhachHangList", true);
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
