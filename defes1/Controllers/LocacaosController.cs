using defes1.Domain;
using defes1.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace defes1.Controllers
{
    public class LocacaosController : Controller
    {
        private contexto db = new contexto();        

        // GET: Locacaos
        public ActionResult Index()
        {
            if (Session["UsuarioLogin"] != null)
            {
                @ViewBag.Usuario = getUser();

                List<Locacao> objLocas = db.Locacoes.Include("Filme").Include("Cliente").Include("Usuario").ToList();

                if (objLocas != null && objLocas.Count > 0)
                {
                    List<LocacaoModel> objLM = new List<LocacaoModel>();

                    objLM = objLocas.Select(x => new LocacaoModel
                    {
                        LocacaoID = x.LocacaoID,
                        FilmeID = x.Filme != null ? x.Filme.FilmeID : 0,
                        FilmeNome = x.Filme != null ? x.Filme.FilmeNome : "INDISPONÍVEL",
                        ClienteID = x.Cliente != null ? x.Cliente.ClienteID : 0,
                        ClienteNome = x.Cliente != null ? x.Cliente.ClienteNome : "INDISPONÍVEL",
                        LocacaoData = x.LocacaoData,
                        UsuarioID = x.Usuario != null ? x.Usuario.UsuarioID : 0,
                        UsuarioLogin = x.Usuario != null ? x.Usuario.UsuarioLogin : "INDISPONÍVEL"
                    }).ToList();

                    return View(objLM);// db.Locacoes.ToList());
                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("Login"); //não logou, volta p/ página de acesso
            }            
        }

        // GET: Locacaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacoes.Find(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            return View(locacao);
        }

        // GET: Locacaos/Create
        public ActionResult Create()
        {
            @ViewBag.Usuario = getUser();
            //--------------------------
            List<Filme> objFilmes = db.Filmes.ToList();

            List<SelectListItem> objListaFilmes = new List<SelectListItem>();

            foreach (Filme item in objFilmes)
                objListaFilmes.Add(new SelectListItem { Text = item.FilmeNome, Value = item.FilmeID.ToString() });

            @ViewBag.Filmes = objListaFilmes;
            //--------------------------
            List<Cliente> objClientes = db.Clientes.ToList();

            List<SelectListItem> objListaClientes = new List<SelectListItem>();

            foreach (Cliente item in objClientes)
                objListaClientes.Add(new SelectListItem { Text = item.ClienteNome, Value = item.ClienteID.ToString() });

            @ViewBag.Clientes = objListaClientes;
            //--------------------------
            List<Usuario> objUsuarios = db.Usuarios.ToList();

            List<SelectListItem> objListaUsuarios = new List<SelectListItem>();

            foreach (Usuario item in objUsuarios)
                objListaUsuarios.Add(new SelectListItem { Text = item.UsuarioLogin, Value = item.UsuarioID.ToString() });

            @ViewBag.Usuarios = objListaUsuarios;
            //--------------------------

            return View();
        }

        // POST: Locacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocacaoID,LocacaoData")] Locacao locacao)
        {
            if (ModelState.IsValid)
            {
                locacao.Filme = db.Filmes.ToList().Find(p => p.FilmeID == int.Parse(Request.Form["Filmes"].ToString()));
                locacao.Cliente = db.Clientes.ToList().Find(p => p.ClienteID == int.Parse(Request.Form["Clientes"].ToString()));
                locacao.Usuario = db.Usuarios.ToList().Find(p => p.UsuarioID == int.Parse(Request.Form["Usuarios"].ToString()));

                db.Locacoes.Add(locacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locacao);
        }

        // GET: Locacaos/Edit/5
        public ActionResult Edit(int? id)
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacoes.Include("Filme").Include("Cliente").Include("Usuario").ToList().Find(p => p.LocacaoID == id);
            if (locacao == null)
            {
                return HttpNotFound();
            }

            //--------------------------
            List<Filme> objFilmes = db.Filmes.ToList();

            List<SelectListItem> objListaFilmes = new List<SelectListItem>();

            foreach (Filme item in objFilmes)
                objListaFilmes.Add(new SelectListItem { Text = item.FilmeNome, Value = item.FilmeID.ToString(), Selected = locacao.Filme.FilmeID == item.FilmeID });

            @ViewBag.Filmes = objListaFilmes;
            //--------------------------
            List<Cliente> objClientes = db.Clientes.ToList();

            List<SelectListItem> objListaClientes = new List<SelectListItem>();

            foreach (Cliente item in objClientes)
                objListaClientes.Add(new SelectListItem { Text = item.ClienteNome, Value = item.ClienteID.ToString(), Selected = locacao.Cliente.ClienteID == item.ClienteID });

            @ViewBag.Clientes = objListaClientes;
            //--------------------------
            List<Usuario> objUsuarios = db.Usuarios.ToList();

            List<SelectListItem> objListaUsuarios = new List<SelectListItem>();

            foreach (Usuario item in objUsuarios)
                objListaUsuarios.Add(new SelectListItem { Text = item.UsuarioLogin, Value = item.UsuarioID.ToString(), Selected = locacao.Usuario.UsuarioID == item.UsuarioID });

            @ViewBag.Usuarios = objListaUsuarios;
            //--------------------------

            return View(locacao);
        }

        // POST: Locacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocacaoID,LocacaoData")] Locacao locacao)
        {
            if (ModelState.IsValid)
            {


                db.Entry(locacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locacao);
        }

        // GET: Locacaos/Delete/5
        public ActionResult Delete(int? id)
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacoes.Include("Filme").Include("Cliente").Include("Usuario").ToList().Find(p => p.LocacaoID == id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            return View(locacao);
        }

        // POST: Locacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locacao locacao = db.Locacoes.Find(id);
            db.Locacoes.Remove(locacao);
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
