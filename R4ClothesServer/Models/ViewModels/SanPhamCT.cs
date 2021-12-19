using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Models.ViewModels
{
    public class SanPhamCT
    {
        public string TenSanPham { get; set; }
        public string Hinh { get; set; }
        public double Gia { get; set; }
        public int SoLuongMua { get; set; }
    }
}
