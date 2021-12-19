using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHoaDon { get; set; }

        [ForeignKey("HoaDon")]
        public int Mahoadon { get; set; }

        // [ForeignKey("SanPham")]
        public int Masanpham { get; set; }

        [StringLength(250)]
        [Required]
        public string Tensanpham { get; set; }
        [Required]
        public int Soluong { get; set; }
        [Required]
        public double Gia { get; set; }

        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }
    }
}
