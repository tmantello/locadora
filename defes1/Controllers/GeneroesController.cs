using defes1.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
                string usuarioLogado = string.Empty;
                usuarioLogado = Session["UsuarioLogin"].ToString().Trim().Length > 1 ? Session["UsuarioLogin"].ToString().Trim().ToUpper() : Session["UsuarioLogin"].ToString().Trim();

                @ViewBag.Usuario = usuarioLogado;

                return View(db.Generos.ToList());
            }
            else
            {
                return RedirectToAction("Login"); //não logou, volta p/ página de acesso
            }            
        }

        // ---------------------------------------------------

        //[HttpPost, ActionName("Index")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    Genero genero = db.Generos.Find(id);
        //    db.Generos.Remove(genero);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        [HttpPost, ActionName("Index")]
        public ActionResult IndexPost(int[] deleteInputs)
        {
            var model = new Genero(); //genero.AllIncluding(message => message.Product);

            if (deleteInputs == null)
            {
                ModelState.AddModelError("", "Nenhum item selecionado para exclusão!");
                return View(model);
            }

            foreach (var item in deleteInputs)
            {
                try
                {
                   // messageRepository.Delete(item);
                }
                catch (Exception err)
                {
                    ModelState.AddModelError("", "Erro no item " + item.ToString() + " : " + err.Message);
                    return View(model);
                }
            }

           // messageRepository.Save();
            ModelState.AddModelError("", deleteInputs.Count().ToString() + " itens excluídos com sucesso.");

            return View(model);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genero genero = db.Generos.Find(id);
            db.Generos.Remove(genero);
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
    }
}
