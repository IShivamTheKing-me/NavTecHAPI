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
using NavtechAPI.Models;

namespace NavtechAPI.Controllers
{
    public class ProductDetailsController : ApiController
    {
        private Product_ApiEntities db = new Product_ApiEntities();

        // GET: api/ProductDetails
        public IQueryable<Product_details> GetProduct_details()
        {
            return db.Product_details;
        }

        // GET: api/ProductDetails/5
        [ResponseType(typeof(Product_details))]
        public IHttpActionResult GetProduct_details(int id)
        {
            Product_details product_details = db.Product_details.Find(id);
            if (product_details == null)
            {
                return NotFound();
            }

            return Ok(product_details);
        }

        // PUT: api/ProductDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_details(int id, Product_details product_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_details.Id)
            {
                return BadRequest();
            }

            db.Entry(product_details).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_detailsExists(id))
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

        // POST: api/ProductDetails
        [ResponseType(typeof(Product_details))]
        public IHttpActionResult PostProduct_details(Product_details product_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product_details.Add(product_details);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_details.Id }, product_details);
        }

        // DELETE: api/ProductDetails/5
        [ResponseType(typeof(Product_details))]
        public IHttpActionResult DeleteProduct_details(int id)
        {
            Product_details product_details = db.Product_details.Find(id);
            if (product_details == null)
            {
                return NotFound();
            }

            db.Product_details.Remove(product_details);
            db.SaveChanges();

            return Ok(product_details);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Product_detailsExists(int id)
        {
            return db.Product_details.Count(e => e.Id == id) > 0;
        }
    }
}