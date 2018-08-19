using defes1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace defes1.Controllers
{
    public class LocacaoController : Controller
    {
        contexto db = new contexto();

        // GET: Locacao
        public ActionResult Index()
        {
            return View(db.Locacoes.ToList());
        }

        // GET: Locacao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Locacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locacao/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locacao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
