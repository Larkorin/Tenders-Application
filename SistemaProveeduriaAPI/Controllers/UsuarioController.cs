using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;


namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var usuarioManager = new UsuarioManager();

            var usuario = usuarioManager.RetrieveById(id);

            return Ok(usuario);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveByAll()
        {
            var usuarioManager = new UsuarioManager();

            List<Usuario> usuarios = usuarioManager.RetrieveAll();

            return Ok(usuarios);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Usuario usuario, string password)
        {
            var usuarioManager = new UsuarioManager();

            usuarioManager.Create(usuario, password);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Usuario usuario)
        {
            var usuarioManager = new UsuarioManager();

            usuarioManager.Update(usuario);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioManager = new UsuarioManager();

            usuarioManager.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("RetrieveByLogin")]
        public async Task<IActionResult> RetrieveByLogin(string email, string password)
        {
            var usuarioManager = new UsuarioManager();

            var usuario = usuarioManager.RetrieveByLogin(email, password);

            return Ok(usuario);
        }

        [HttpPost]
        [Route("OtpRegistration")]
        public IActionResult OtpRegistration(string mail, string phonenumber)
        {
            NotificationManager notificationManager = new NotificationManager();
            Random random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            // Obtener una instancia de IDataProtectionProvider del contenedor de dependencias.
            IDataProtectionProvider provider = HttpContext.RequestServices.GetDataProtectionProvider();

            // Obtener una instancia de IDataProtector a partir del proveedor.
            IDataProtector protector = provider.CreateProtector("MiApp.Cifrado");

            // Encripta el valor del OTP
            var otpValue = protector.Protect(otp);

            // Guarda el valor en una cookie con un nombre específico
            HttpContext.Response.Cookies.Append("MyAppOtp", otpValue, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Domain = "localhost",
                Expires = DateTime.UtcNow.AddMinutes(5) // Activa durante 5 minutos
            });

            notificationManager.NotifyUserBySMS(otp, phonenumber);
            notificationManager.NotifyUserByEmail(otp, mail);

            return Ok(otp);
        }

        [HttpPost]
        [Route("OtpRegistrationMail")]
        public IActionResult OtpRegistrationMail(string mail)
        {
            NotificationManager notificationManager = new NotificationManager();
            Random random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            // Obtener una instancia de IDataProtectionProvider del contenedor de dependencias.
            IDataProtectionProvider provider = HttpContext.RequestServices.GetDataProtectionProvider();

            // Obtener una instancia de IDataProtector a partir del proveedor.
            IDataProtector protector = provider.CreateProtector("MiApp.Cifrado");

            // Encripta el valor del OTP
            var otpValue = protector.Protect(otp);

            // Guarda el valor en una cookie con un nombre específico
            HttpContext.Response.Cookies.Append("MyAppOtp", otpValue, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Domain = "localhost",
                Expires = DateTime.UtcNow.AddMinutes(5) // Activa durante 5 minutos
            });

            notificationManager.NotifyUserByEmail(otp, mail);

            return Ok(otp);
        }

        [HttpPost]
        [Route("OtpRegistrationPhone")]
        public IActionResult OtpRegistrationPhone(string phonenumber)
        {
            NotificationManager notificationManager = new NotificationManager();
            Random random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            // Obtener una instancia de IDataProtectionProvider del contenedor de dependencias.
            IDataProtectionProvider provider = HttpContext.RequestServices.GetDataProtectionProvider();

            // Obtener una instancia de IDataProtector a partir del proveedor.
            IDataProtector protector = provider.CreateProtector("MiApp.Cifrado");

            // Encripta el valor del OTP
            var otpValue = protector.Protect(otp);

            // Guarda el valor en una cookie con un nombre específico
            HttpContext.Response.Cookies.Append("MyAppOtp", otpValue, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Domain = "localhost",
                Expires = DateTime.UtcNow.AddMinutes(5) // Activa durante 5 minutos
            });

            notificationManager.NotifyUserBySMS(otp, phonenumber);

            return Ok(otp);
        }

        [HttpPost]
        [Route("Verify")]
        public IActionResult VerifyOtp(string otp, Usuario usuario, string password, string cookie)
        {
            // Obtener una instancia de IDataProtectionProvider del contenedor de dependencias.
            //IDataProtectionProvider provider = HttpContext.RequestServices.GetDataProtectionProvider();

            // Obtener una instancia de IDataProtector a partir del proveedor.
            //IDataProtector protector = provider.CreateProtector("MiApp.Cifrado");

            // Obtiene el valor cifrado de la cookie de OTP


            // if (string.IsNullOrEmpty(otpValue))
            // {
            // La cookie de OTP no existe, lo que significa que el usuario no ha solicitado un OTP aún
            //     return BadRequest("No se ha solicitado un OTP para esta sesión.");
            // }

            // Desencripta el valor de la cookie de OTP
            //otpValue = protector.Unprotect(otpValue);

            if (cookie != otp)
            {
                return BadRequest("El valor del OTP es incorrecto.");
            }

            // Elimina la cookie de OTP
            // Response.Cookies.Delete("MyAppOtp");

            var usuarioManager = new UsuarioManager();
            usuarioManager.Create(usuario, password);

            return Ok("El valor del OTP es correcto.");
        }

        [HttpPost]
        [Route("VerifyRecover")]
        public IActionResult VerifyRecoverOtp(string otp, string cookie)
        {
            // Obtener una instancia de IDataProtectionProvider del contenedor de dependencias.
            //IDataProtectionProvider provider = HttpContext.RequestServices.GetDataProtectionProvider();

            // Obtener una instancia de IDataProtector a partir del proveedor.
            //IDataProtector protector = provider.CreateProtector("MiApp.Cifrado");

            // Obtiene el valor cifrado de la cookie de OTP


            // if (string.IsNullOrEmpty(otpValue))
            // {
            // La cookie de OTP no existe, lo que significa que el usuario no ha solicitado un OTP aún
            //     return BadRequest("No se ha solicitado un OTP para esta sesión.");
            // }

            // Desencripta el valor de la cookie de OTP
            //otpValue = protector.Unprotect(otpValue);

            if (cookie != otp)
            {
                return BadRequest("El valor del OTP es incorrecto.");
            }

            // Elimina la cookie de OTP
            // Response.Cookies.Delete("MyAppOtp");


            return Ok("El valor del OTP es correcto.");
        }


        [HttpGet]
        [Route("RetrieveByPhone")]
        public async Task<IActionResult> RetrieveByPhone(string phone)
        {
            var usuarioManager = new UsuarioManager();

            var usuario = usuarioManager.RetrieveByPhone(phone);

            return Ok(usuario);
        }

        [HttpPost]
        [Route("AssignPermissions")]
        public async Task<IActionResult> AssignPermissions(List<int> idsPermisos, int UserId)
        {
            var usuarioManager = new UsuarioManager();

            usuarioManager.AssignPermissions(idsPermisos, UserId);

            return Ok();
        }

        [HttpPost]
        [Route("RemovePermissions")]
        public async Task<IActionResult> RemovePermissions(List<int> idsPermisos, int userId)
        {
            var usuarioManager = new UsuarioManager();

            usuarioManager.RemovePermissions(idsPermisos, userId);

            return Ok();
        }

        [HttpGet]
        [Route("GetUserPermissions")]
        public async Task<IActionResult> GetUserPermissions(int userId)
        {
            var usuarioManager = new UsuarioManager();

            List<Permiso> permissions = usuarioManager.GetUserPermissions(userId);

            return Ok(permissions);
        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public async Task<IActionResult> RetrieveByEmail(string email)
        {
            var usuarioManager = new UsuarioManager();

            var usuario = usuarioManager.RetrieveByEmail(email);
            return Ok(usuario);
        }
    }
}
