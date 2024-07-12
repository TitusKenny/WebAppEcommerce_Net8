using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;
using System.Collections.Generic;
using System.Security.Claims;
using WebAppEcommerce_Net8.Helpers;
using WebAppEcommerce_Net8.Data;
using WebAppEcommerce_Net8.ViewModel;

namespace WebAppEcommerce_Net8.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly WebAppShopNet8Context db;
        private readonly IMapper _mapper;

        public KhachHangController(WebAppShopNet8Context context, IMapper mapper) 
        { 
            db=context;
            _mapper = mapper;
        }
        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var KhachHang = _mapper.Map<KhachHang>(model);
                    KhachHang.RandomKey = MyUtil.GenerateRanDomKey();
                    KhachHang.MatKhau = model.MatKhau.ToMd5Hash(KhachHang.RandomKey);
                    KhachHang.HieuLuc = true; // sẽ xử lý khi dùng Mail để active
                    KhachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        KhachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }
                    db.Add(KhachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "HangHoa");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";   
                }           
            }
            return View();
        }
        #endregion

        #region Login

        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl) 
        { 
            ViewBag.ReturnUrl = ReturnUrl;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Tài khoản đã bị khoá. Vui lòng liên hệ Admin.");
                    }
                    else
                    {
                        if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim> {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.HoTen),
                                new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.MaKh),
                                //claim - role động
                                new Claim(ClaimTypes.Role, "Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if(Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                        
                    }
                }
            }
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult QuenMatKhau()
        {
            return View();
        }
        
    }
}
