using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using WebAppEcommerce_Net8.Data;
using WebAppEcommerce_Net8.ViewModel;

namespace WebAppEcommerce_Net8.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly WebAppShopNet8Context db;
        public HangHoaController(WebAppShopNet8Context context) {
            db = context;        
        }
        public IActionResult Index(int? loai)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MotaNgan = p.MoTaDonVi ?? "",
                Tenloai = p.MaLoaiNavigation.TenLoai

            });
            return View(result);
        }
        public IActionResult Search (string query)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (query != null )
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MotaNgan = p.MoTaDonVi ?? "",
                Tenloai = p.MaLoaiNavigation.TenLoai

            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(hh => hh.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                MaHh = data.MaHh,
                TenHH = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? "",
                Hinh = data.Hinh ?? "",
                MotaNgan = data.MoTaDonVi ?? "",
                Tenloai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10, // tinh sau
                DiemDanhGia = 5//check sau

            };
            return View(result);
        }
    }
}
