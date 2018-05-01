using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Cedro_WebApi.Models;

namespace Cedro_WebApi.Controllers
{
    public class PratoAPIController : ApiController
    {
        private DB_cedroapiEntities db = new DB_cedroapiEntities();

        // GET api/PratoAPI/Getprato
        public IQueryable<prato> Getprato()
        {
            return db.prato;
        }

        // GET api/PratoAPI/5
        [ResponseType(typeof(prato))]
        public IHttpActionResult Getprato(string id)
        {
            prato prato = db.prato.Find(id);
            if (prato == null)
            {
                return NotFound();
            }

            return Ok(prato);
        }

        // PUT api/PratoAPI/5
        public IHttpActionResult Putprato(string id, prato prato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prato.nome)
            {
                return BadRequest();
            }

            db.Entry(prato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pratoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/PratoAPI
        [ResponseType(typeof(prato))]
        public IHttpActionResult Postprato(prato prato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.prato.Add(prato);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (pratoExists(prato.nome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = prato.nome }, prato);
        }

        // DELETE api/PratoAPI/5
        [ResponseType(typeof(prato))]
        public IHttpActionResult Deleteprato(string id)
        {
            prato prato = db.prato.Find(id);
            if (prato == null)
            {
                return NotFound();
            }

            db.prato.Remove(prato);
            db.SaveChanges();

            return Ok(prato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool pratoExists(string id)
        {
            return db.prato.Count(e => e.nome == id) > 0;
        }
    }
}