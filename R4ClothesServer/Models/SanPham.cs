using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Models
{
    public class SanPham
    {
        [Key]
        public int Masanpham { get; set; }

        [ForeignKey("QuanTris")]
        public int Maquantri { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn cần nhập tên.")]
        [Display(Name = "Tên sản phẩm")]
        public string Tensanpham { get; set; }

        [Display(Name = "Mã loại")]
        [Required(ErrorMessage = "Bạn cần chọn mã loại."), Range(1, int.MaxValue, ErrorMessage = "Bạn cần chọn mã loại.")]
        [ForeignKey("LoaiSanPhams")]
        public int Maloai { get; set; }

        public int Soluong { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập giá."), Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập giá.")]
        [Display(Name = "Giá")]
        public double Gia { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình")]
        public string Hinh { get; set; }

        public int Soluotxem { get; set; }

        public DateTime Ngaynhap { get; set; }

        public int? Giamgia { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Mota { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Trangthai { get; set; }

        public bool Dacbiet { get; set; }

        public QuanTri QuanTris { get; set; }
        public LoaiSanPham LoaiSanPhams { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
