using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using R4ClothesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace R4ClothesServer.Pages.quantri
{
    public partial class QuanTriDialog
    {
        [Parameter]
        public string id { get; set; }
        bool flag = true;
        MudTextField<string> pwField1;
        private string Tieude = "";
        public QuanTri quantri = new QuanTri();
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                flag = false;
                Tieude = "Thêm quản trị";
                quantri = new QuanTri();
            }
            else
            {
                
                Tieude = "Sửa thông tin quản trị";
                var res = await _apiHelper.GetRequestAsync("QuanTris/get/" + id, null);
                if (res != "-1")
                {
                    quantri = JsonConvert.DeserializeObject<QuanTri>(res);
                }
            }
        }
        public async void SubmitForm()
        {
            if (quantri.Maquantri == 0)
            {              
                var res = await _apiHelper.PostRequestAsync("QuanTris/add",quantri, null);
                if (res != "-1")
                {
                    Snackbar.Add("Thêm thành công", MudBlazor.Severity.Success);
                    await Task.Delay(3000);
                    navigation.NavigateTo("/quantrilist");
                }
                else
                {
                    Snackbar.Add("Thêm thất bại", MudBlazor.Severity.Error);
                }
            }
            else
            {               
                var res = await _apiHelper.PostRequestAsync("QuanTris/edit/" + id, quantri, null);

                if (res != "-1")
                {
                    Snackbar.Add("Sửa thành công", MudBlazor.Severity.Success);
                    await Task.Delay(3000);
                    navigation.NavigateTo("/quantrilist");
                }
                else
                {
                    Snackbar.Add("Sửa thất bại", MudBlazor.Severity.Error);
                }
            }            
        }
        private string PasswordMatch(string arg)
        {
            if (pwField1.Value != arg)
                return "mật khẩu không trùng";
            return null;
        }
    }
}
