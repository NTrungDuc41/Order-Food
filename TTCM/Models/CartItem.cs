using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTCM.Models
{
    public class CartItem
    {
        public string MaDa { get; set; }
        public string TenDa { get; set; }
        public string Anh { get; set; }
        public decimal? DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => (double)(SoLuong * DonGia);

        
    }
}