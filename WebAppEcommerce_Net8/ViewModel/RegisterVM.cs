﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebAppEcommerce_Net8.ViewModel
{
    public class RegisterVM
    {
        [Key]
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 kí tự")]
        public string MaKh { get; set; }

        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name ="Họ Tên")]
        [Required(ErrorMessage ="*")]
        [MaxLength(50, ErrorMessage ="Tối đa 50 kí tự")]
        public string HoTen { get; set; }

        [Display(Name ="Giới tính")]
        public bool GioiTinh { get; set; }

        [Display(Name ="Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Display(Name ="Địa chỉ")]
        [MaxLength(60, ErrorMessage ="Tối đa 60 kí tự")]
        public string DiaChi { get; set; }

        [Display(Name ="Điện thoại")]
        [MaxLength(24, ErrorMessage ="Tối đa 24 kí tự")]
        [RegularExpression(@"0[987532]\d{8}",ErrorMessage ="Chưa đúng định dạng di động Việt Nam")]
        public string DienThoai { get; set; }

        [Display(Name ="Email")]
        [EmailAddress(ErrorMessage ="Chưa đúng định dạng email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Hình")]
        public string? Hinh { get; set; }
    }
}
