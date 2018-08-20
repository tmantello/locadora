using defes1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace defes1.Controllers
{
    public class HomeController : Controller
    {
        private contexto db = new contexto();

        public ActionResult Index()
        {            
            if (Session["UsuarioLogin"] != null)
            {
                string usuarioLogado = string.Empty;
                usuarioLogado = Session["UsuarioLogin"].ToString().Trim().Length > 1 ? Session["UsuarioLogin"].ToString().Trim().ToUpper() : Session["UsuarioLogin"].ToString().Trim();

                @ViewBag.Usuario = usuarioLogado;//MvcApplication.GetLoggedUser();

                return View();
            }
            else
            {
                return RedirectToAction("Login"); //não logou, volta p/ página de acesso
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            @ViewBag.URL = @Request.Url.AbsoluteUri + "Usuarios/Create";
            return View();
        }

        public ActionResult Logout()
        {
            Session["UsuarioID"] = null;
            Session["UsuarioLogin"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario u)
        {
            // post do login
            if (ModelState.IsValid) // verifica estado
            {
                List<Usuario> objUsuarios = db.Usuarios.ToList();
                if (objUsuarios != null)
                {
                    if (objUsuarios.Find(p => p.UsuarioLogin == u.UsuarioLogin && p.UsuarioSenha == u.UsuarioSenha && p.UsuarioAtivo) != null)
                    {
                        Usuario objUsuario = objUsuarios.Find(p => p.UsuarioLogin == u.UsuarioLogin && p.UsuarioSenha == u.UsuarioSenha && p.UsuarioAtivo);

                        if (objUsuario != null)
                        {
                            Session["UsuarioID"] = objUsuario.UsuarioID.ToString(); // salva dados do usuário em sessão
                            Session["UsuarioLogin"] = objUsuario.UsuarioLogin;
                       
                            FormsAuthentication.SetAuthCookie(objUsuario.UsuarioLogin, false); // autentica usuário e determina tempo de sessão
                            var authTicket = new FormsAuthenticationTicket(1, objUsuario.UsuarioLogin, DateTime.Now, DateTime.Now.AddMinutes(20), false, "admin");
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            HttpContext.Response.Cookies.Add(authCookie);
                            return RedirectToAction("Index", "Home");
                        }                        
                    }
                }
            }
            return RedirectToAction("Login"); // caso não consiga logar, volta p/ página de acesso
        }
    }
}