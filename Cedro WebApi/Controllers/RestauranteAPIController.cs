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
    public class RestauranteAPIController : ApiController
    {
        private DB_cedroapiEntities db = new DB_cedroapiEntities();

        // GET api/RestauranteAPI/Getrestaurante
        public IQueryable<restaurante> Getrestaurante()
        {
            return db.restaurante;
        }

        // GET api/RestauranteAPI/5
        [ResponseType(typeof(restaurante))]
        public IHttpActionResult Getrestaurante(int id)
        {
            restaurante restaurante = db.restaurante.Find(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return Ok(restaurante);
        }

        // PUT api/RestauranteAPI/5
        public IHttpActionResult Putrestaurante(int id, restaurante restaurante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurante.id)
            {
                return BadRequest();
            }

            db.Entry(restaurante).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!restauranteExists(id))
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

        // POST api/RestauranteAPI
        [ResponseType(typeof(restaurante))]
        public IHttpActionResult Postrestaurante(restaurante restaurante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.restaurante.Add(restaurante);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (restauranteExists(restaurante.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = restaurante.id }, restaurante);
        }

        // DELETE api/RestauranteAPI/5
        [ResponseType(typeof(restaurante))]
        public IHttpActionResult Deleterestaurante(int id)
        {
            restaurante restaurante = db.restaurante.Find(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            db.restaurante.Remove(restaurante);
            db.SaveChanges();

            return Ok(restaurante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool restauranteExists(int id)
        {
            return db.restaurante.Count(e => e.id == id) > 0;
        }
    }
}