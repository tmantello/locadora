using defes1.Domain;
using defes1.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace defes1.Controllers
{
    public class FilmesController : Controller
    {
        private contexto db = new contexto();        

        // GET: Filmes
        public ActionResult Index()
        {
            if (Session["UsuarioLogin"] != null)
            {                
                @ViewBag.Usuario = getUser();

                List<Filme> objFilmes = db.Filmes.Include("Genero").ToList();
                List<FilmeModel> objFM = objFilmes.Select(x => new FilmeModel
                {
                  FilmeID = x.FilmeID,
                  FilmeNome = x.FilmeNome,
                  FilmeCriacao = x.FilmeCriacao,
                  FilmeAtivo = x.FilmeAtivo,
                  Genero = x.Genero != null ? x.Genero.GeneroNome : "INDISPONÍVEL"
                }).ToList();

                return View(objFM);//db.Filmes.ToList());
            }
            else
            {
                return RedirectToAction("Login"); //não logou, volta p/ página de acesso
            }            
        }

        // GET: Filmes/Details/5
        public ActionResult Details(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = db.Filmes.Find(id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // GET: Filmes/Create
        public ActionResult Create() // carregar p/ criar novo filme
        {
            @ViewBag.Usuario = getUser();
            List<Genero> objGeneros = db.Generos.ToList();

            List<SelectListItem> objListaGeneros = new List<SelectListItem>();

            foreach (Genero item in objGeneros)            
                objListaGeneros.Add(new SelectListItem {Text = item.GeneroNome, Value = item.GeneroID.ToString()});            

            ViewBag.Generos = objListaGeneros;
            
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FilmeID,FilmeNome,FilmeCriacao,FilmeAtivo")] Filme filme) // salvando novo filme
        {
            if (ModelState.IsValid)
            {
                filme.Genero = db.Generos.ToList().Find(p => p.GeneroID == int.Parse(Request.Form["Generos"].ToString()));

                db.Filmes.Add(filme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(filme);
        }

        // GET: Filmes/Edit/5
        public ActionResult Edit(int? id) // editando
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = db.Filmes.Include("Genero").ToList().Find(p => p.FilmeID == id);
            if (filme == null)
            {
                return HttpNotFound();
            }

            List<Genero> objGeneros = db.Generos.ToList();

            List<SelectListItem> objListaGeneros = new List<SelectListItem>();

            foreach (Genero item in objGeneros)
                objListaGeneros.Add(new SelectListItem { Text = item.GeneroNome, Value = item.GeneroID.ToString(), Selected = filme.Genero.GeneroID == item.GeneroID });

            ViewBag.Generos = objListaGeneros;

            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FilmeID,FilmeNome,FilmeCriacao,FilmeAtivo")] Filme filme) // atualizando após edição
        {
            if (ModelState.IsValid)
            {
                db.Entry(filme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filme);
        }

        // GET: Filmes/Delete/5
        public ActionResult Delete(int? id) // excluindo
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = db.Filmes.Include("Genero").ToList().Find(p => p.FilmeID == id);            
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) // executando exclusão
        {
            Filme filme = db.Filmes.Find(id);
            db.Filmes.Remove(filme);
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
