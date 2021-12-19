using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;


namespace R4ClothesServer.Pages.hoadon
{
    public partial class HoaDonDialog
    {
        [Parameter]
        public string id { get; set; }
        public string nguoiql { get; set; }
        private string Tieude = "";
        public HoaDon hoaDon = new HoaDon();

        protected override async Task OnInitializedAsync()
        {
            Tieude = "Sửa hóa đơn";
            var res = await _apiHelper.GetRequestAsync("HoaDons/" + id, null);
            if (res != "-1")
            {
                hoaDon = JsonConvert.DeserializeObject<HoaDon>(res);
            }          
        }
        public async Task SubmitForm()
        {
            nguoiql = await _localStorageService.GetItemAsync<string>("quantriid");
            var res = await _apiHelper.PostRequestAsync("QuanTris/hoadon/suahd?idhd=" + id + "&nguoiql=" + nguoiql + "&tt=" + ((int)hoaDon.Trangthai), hoaDon, null);
            if (res != "-1")
            {
                Snackbar.Add("Sửa thành công", MudBlazor.Severity.Success);
                await Task.Delay(3000);
                navigation.NavigateTo("/hoadonlist");
            }
            else
            {
                Snackbar.Add("Sửa thất bại", MudBlazor.Severity.Error);
            }
        }
    }
}
