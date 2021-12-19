using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace R4ClothesServer.Models
{
    public class YeuThich
    {
        [Key]
        public int Mayeuthich { get; set; }

        [ForeignKey("KhachHang")]
        public int Makhachhang { get; set; }

        [ForeignKey("SanPham")]
        public int Masanpham { get; set; }

        public KhachHang KhachHang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
