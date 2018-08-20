using defes1.Domain;
using defes1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web;

namespace defes1.Controllers
{
    public class GeneroesController : Controller
    {
        private contexto db = new contexto();        

        // GET: Generoes
        public ActionResult Index()
        {
            if (Session["UsuarioLogin"] != null)
            {
                @ViewBag.Usuario = getUser();

                List<Genero> objLista = db.Generos.ToList();
                if (objLista != null && objLista.Count > 0)
                {
                    List<GeneroModel> generoModels = new List<GeneroModel>();
                    foreach (Genero item in objLista)
                    {
                        generoModels.Add(new GeneroModel
                        { GeneroAtivo = item.GeneroAtivo,
                          GeneroCriacao = item.GeneroCriacao,
                          GeneroID = item.GeneroID,
                          GeneroNome = item.GeneroNome                         
                        });
                    }

                    return View(generoModels);
                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("Login"); //não logou, volta p/ página de acesso
            }            
        }

        // ---------------------------------------------------

        //[HttpPost]//, ActionName("Index")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(IEnumerable<GeneroModel> GeneroModel)
        //{
        //    if (GeneroModel.Count(x => x.GeneroSelecionado) == 0)
        //        return View(db.Generos.ToList()); // não faz nada
        //    else
        //    {
        //        foreach (GeneroModel item in GeneroModel)
        //        {
        //            if (item.GeneroSelecionado) // excluir selecionados
        //            {
        //                Genero genero = db.Generos.Find(item.GeneroID);
        //                db.Generos.Remove(genero);
        //                db.SaveChanges();
        //            }
        //        }
        //    }

        //    return View(db.Generos.ToList());
        //}

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> generoIdsToDelete)
        {
            List<Genero> objLista = db.Generos.ToList();
            foreach (int item in generoIdsToDelete)
            {
                foreach (Genero gen in objLista)
                {
                    if (item == gen.GeneroID)
                    {
                        Genero genero = db.Generos.Find(item);
                        db.Generos.Remove(genero);
                        db.SaveChanges();
                        break;
                    }
                }
            }
          
            return RedirectToAction("Index");
        }


        // ---------------------------------------------------

        // GET: Generoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Generos.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // GET: Generoes/Create
        public ActionResult Create()
        {
            @ViewBag.Usuario = getUser();

            return View();
        }

        // POST: Generoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GeneroID,GeneroNome,GeneroCriacao,GeneroAtivo")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Generos.Add(genero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genero);
        }

        // GET: Generoes/Edit/5
        public ActionResult Edit(int? id)
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Generos.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Generoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GeneroID,GeneroNome,GeneroCriacao,GeneroAtivo")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genero);
        }

        // GET: Generoes/Delete/5
        public ActionResult Delete(int? id)
        {
            @ViewBag.Usuario = getUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Generos.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Generoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Genero genero = db.Generos.Find(id);
        //    db.Generos.Remove(genero);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
