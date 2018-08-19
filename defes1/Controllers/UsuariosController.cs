using Dapper;
using defes1.Domain;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace defes1.Controllers
{
    public class UsuariosController : Controller
    {
        private contexto db = new contexto();        

        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session["UsuarioLogin"] != null)
            {
                @ViewBag.Usuario = getUser();

                string strConexao = ConfigurationManager.ConnectionStrings["contexto"].ConnectionString;

                SqlConnection conexaoBD = new SqlConnection(strConexao);
                conexaoBD.Open();

                var resultado = conexaoBD.Query("select * from Usuarios"); // dapper

                int i = 0;
                foreach (dynamic usuario in resultado)
                {
                    i++;    
                }

                if (i > 1)
                    @ViewBag.TotalUsuarios = "Existem " + i + " usuários na base.";
                else if (i == 1)
                    @ViewBag.TotalUsuarios = "Existe 1 usuário na base.";
                else
                    @ViewBag.TotalUsuarios = "Não existem usuários na base.";

                conexaoBD.Close();

                return View(db.Usuarios.ToList());
            }
            else
            {
                return RedirectToAction("Login"); //não logou, volta p/ página de acesso
            }            
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            @ViewBag.Usuario = getUser();

            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioID,UsuarioLogin,UsuarioSenha,UsuarioAtivo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                if (Session["UsuarioLogin"] != null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Login", "");
                //return Redirect("Home/MyIndex");
            }
            else
            {
                if (Session["UsuarioLogin"] != null)
                    return RedirectToAction("Login");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,UsuarioLogin,UsuarioSenha,UsuarioAtivo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string getUser()
        {
            if (Session["UsuarioLogin"] != null)
                return Session["UsuarioLogin"].ToString().Trim().Length > 1 ? Session["UsuarioLogin"].ToString().Trim().ToUpper() : Session["UsuarioLogin"].ToString().Trim();
            else
                return "";
        }
    }
}
