using Microsoft.AspNetCore.Mvc;
using WebAppEcommerce_Net8.Data;
using WebAppEcommerce_Net8.ViewModel;
namespace WebAppEcommerce_Net8.ViewComponemt
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly WebAppShopNet8Context db;
        public MenuLoaiViewComponent(WebAppShopNet8Context context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
               MaLoai = lo.MaLoai,
               TenLoai = lo.TenLoai,
               SoLuong = lo.HangHoas.Count
            }).OrderBy(p=>p.TenLoai);
            return View(data); //Default.cshtml
            //return View("Default",data);
        }
    }
}
