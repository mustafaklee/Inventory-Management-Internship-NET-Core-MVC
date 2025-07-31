using LoginEkrani.Models;
using LoginEkrani.Models.Login;
using LoginEkrani.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Diagnostics;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using LoginEkrani.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace LoginEkrani.Controllers.LoginController
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly Database _db;
        private readonly LogService _logService;
        private readonly IConfiguration _configuration;

        public LoginController(
            ILogger<LoginController> logger, 
            Database db, 
            LogService logService,
            IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _logService = logService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Register(string userName)
        {
            try
            {
                LoginPageModel model = new LoginPageModel();
                ViewBag.Roles = await _db.kpsft_roller.ToListAsync();

                if (userName != null)
                {
                    var user = await _db.kpsft_boskay
                        .Include(s => s.UserRoles)
                        .ThenInclude(r => r.Role)
                        .FirstOrDefaultAsync(s => s.kpsft_kullaniciAdi == userName);
                    
                    if (user != null)
                    {
                        user.kpsft_sifre = null;
                        return View(user);
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı kayıt sayfası yüklenirken hata oluştu");
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginPageModel model, List<int> selectedRoles)
        {
            try
            {
                var user = await _db.kpsft_boskay
                    .FirstOrDefaultAsync(s => s.kpsft_kullaniciAdi == model.kpsft_kullaniciAdi);

                if (user == null)
                {
                    // Yeni kullanıcı kaydı
                    var sifrele = new Sifrele();
                    model.kpsft_sifre = sifrele.HashPassword(model.kpsft_sifre);

                    _db.kpsft_boskay.Add(model);
                    await _db.SaveChangesAsync();

                    var newUser = await _db.kpsft_boskay
                        .FirstOrDefaultAsync(s => s.kpsft_kullaniciAdi == model.kpsft_kullaniciAdi);

                    if (selectedRoles != null)
                    {
                        foreach (var roleId in selectedRoles)
                        {
                            var userRole = new UserRole
                            {
                                id_kpsft = newUser.id_kpsft,
                                kpsft_rol_id = roleId
                            };
                            _db.kpsft_kullanıcı_rolleri.Add(userRole);
                        }
                        await _db.SaveChangesAsync();
                    }

                    return RedirectToAction("UserList");
                }
                else
                {
                    // Mevcut kullanıcı güncelleme
                    if (selectedRoles != null)
                    {
                        var existingRoles = await _db.kpsft_kullanıcı_rolleri
                            .Where(e => e.id_kpsft == user.id_kpsft)
                            .ToListAsync();

                        _db.kpsft_kullanıcı_rolleri.RemoveRange(existingRoles);

                        foreach (var roleId in selectedRoles)
                        {
                            var userRole = new UserRole
                            {
                                id_kpsft = user.id_kpsft,
                                kpsft_rol_id = roleId
                            };
                            _db.kpsft_kullanıcı_rolleri.Add(userRole);
                        }
                        await _db.SaveChangesAsync();
                    }

                    user.kpsft_kullaniciAdi = model.kpsft_kullaniciAdi;
                    user.kpsft_ad = model.kpsft_ad;
                    user.kpsft_soyad = model.kpsft_soyad;
                    user.kpsft_mailAdrress = model.kpsft_mailAdrress;
                    user.kpsft_tcKimlik = model.kpsft_tcKimlik;

                    var sifrele = new Sifrele();
                    user.kpsft_sifre = sifrele.HashPassword(model.kpsft_sifre);

                    _db.kpsft_boskay.Update(user);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("UserList");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı kaydı/güncelleme işlemi sırasında hata oluştu");
                ModelState.AddModelError("", "İşlem sırasında bir hata oluştu. Lütfen tekrar deneyiniz.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            try
            {
                var model = await _db.kpsft_boskay
                    .Include(s => s.UserRoles)
                    .ThenInclude(r => r.Role)
                    .ToListAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı listesi yüklenirken hata oluştu");
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserList(LoginPageModel model)
        {
            try
            {
                var sifrele = new Sifrele();
                model.kpsft_sifre = sifrele.HashPassword(model.kpsft_sifre);
                _db.kpsft_boskay.Add(model);
                await _db.SaveChangesAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ekleme işlemi sırasında hata oluştu");
                ModelState.AddModelError("", "Kullanıcı eklenirken bir hata oluştu. Lütfen tekrar deneyiniz.");
                return View();
            }
        }

        public async Task<IActionResult> Sil(string userName)
        {
            try
            {
                var user = await _db.kpsft_boskay.FirstOrDefaultAsync(e => e.kpsft_kullaniciAdi == userName);
                if (user != null)
                {
                    _db.kpsft_boskay.Remove(user);
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı silme işlemi sırasında hata oluştu");
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCode(string kpsft_mailAdrress)
        {
            try
            {
                var user = await _db.kpsft_boskay
                    .FirstOrDefaultAsync(u => u.kpsft_mailAdrress == kpsft_mailAdrress);

                if (user == null)
                {
                    ViewBag.Hata = "Bu e-posta adresi doğrulanamadı.";
                    return View("InputMail", "Login");
                }

                var verificationCode = GenerateVerificationCode();
                HttpContext.Session.SetString("VerificationCode", verificationCode);
                HttpContext.Session.SetString("EmailAddress", kpsft_mailAdrress);

                await SendVerificationCodeAsync(kpsft_mailAdrress, verificationCode);
                return View("ResetPassword");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Doğrulama kodu gönderme işlemi sırasında hata oluştu");
                ViewBag.Hata = "Doğrulama kodu gönderilirken bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View("InputMail", "Login");
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpGet]
        public IActionResult InputMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string girilenKod, string newPassword)
        {
            try
            {
                var expectedCode = HttpContext.Session.GetString("VerificationCode");
                var email = HttpContext.Session.GetString("EmailAddress");

                if (girilenKod == expectedCode)
                {
                    var sifrele = new Sifrele();
                    var user = await _db.kpsft_boskay
                        .FirstOrDefaultAsync(u => u.kpsft_mailAdrress == email);

                    if (user != null)
                    {
                        user.kpsft_sifre = sifrele.HashPassword(newPassword);
                        await _db.SaveChangesAsync();

                        await _logService.LogEkle(
                            user.kpsft_kullaniciAdi,
                            user.kpsft_sifre,
                            "Başarılı Şifre Değişikliği"
                        );
                        return RedirectToAction("Index", "Login", new { email });
                    }
                }

                ViewBag.Hata = "Doğrulama kodu hatalı.";
                await _logService.LogEkle(
                    email,
                    newPassword,
                    "Başarısız Şifre Değişikliği"
                );
                return View("ResetPassword");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şifre sıfırlama işlemi sırasında hata oluştu");
                ViewBag.Hata = "Şifre sıfırlama işlemi sırasında bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View("ResetPassword");
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private async Task SendVerificationCodeAsync(string kpsft_mailAdrress, string code)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                var mailboxAddressFrom = new MailboxAddress("Admin", _configuration["EmailSettings:From"]);
                mimeMessage.From.Add(mailboxAddressFrom);

                var mailboxAddressTo = new MailboxAddress("User", kpsft_mailAdrress);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = code;
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Doğrulama Kodu";

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(
                        _configuration["EmailSettings:SmtpServer"],
                        int.Parse(_configuration["EmailSettings:Port"]),
                        false
                    );
                    await client.AuthenticateAsync(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]
                    );
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "E-posta gönderme işlemi sırasında hata oluştu");
                throw;
            }
        }

        public IActionResult Index()
        {
            var model = new LoginPageModel();
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPageModel model)
        {
            try
            {
                var sifrele = new Sifrele();
                var user = await _db.kpsft_boskay
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.kpsft_kullaniciAdi == model.kpsft_kullaniciAdi);

                if (user != null && sifrele.VerifyPassword(model.kpsft_sifre, user.kpsft_sifre))
                {
                    await _logService.LogEkle(
                        model.kpsft_kullaniciAdi,
                        model.kpsft_sifre,
                        "Başarılı Giriş İşlemi"
                    );

                    var claims = new List<Claim>();
                    foreach (var userRole in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.kpsft_rol));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2),
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    return RedirectToAction("AdminIndex", "Admin");
                }

                await _logService.LogEkle(
                    model.kpsft_kullaniciAdi,
                    model.kpsft_sifre,
                    "Başarısız Giriş İşlemi"
                );

                ViewBag.Hata = "Kullanıcı bilgileri bulunamadı.";
                model.kpsft_kullaniciAdi = "";
                model.kpsft_sifre = "";

                return View("Index", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Giriş işlemi sırasında hata oluştu");
                ViewBag.Hata = "Giriş işlemi sırasında bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View("Index", model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
