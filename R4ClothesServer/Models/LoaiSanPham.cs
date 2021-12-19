using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int Maloai { get; set; }
        [Display(Name = "Tên loại"), Required(ErrorMessage = "Bạn cần nhập tên."), StringLength(250)]
        public string Tenloai { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}
