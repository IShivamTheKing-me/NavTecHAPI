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
    public class OrderDetailsController : ApiController
    {
        private Product_ApiEntities db = new Product_ApiEntities();

        // GET: api/OrderDetails
        public IQueryable<Order_details> GetOrder_details()
        {
            return db.Order_details;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(Order_details))]
        public IHttpActionResult GetOrder_details(int id)
        {
            Order_details order_details = db.Order_details.Find(id);
            if (order_details == null)
            {
                return NotFound();
            }

            return Ok(order_details);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_details(int id, Order_details order_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_details.Id)
            {
                return BadRequest();
            }

            db.Entry(order_details).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_detailsExists(id))
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

        // POST: api/OrderDetails
        [ResponseType(typeof(Order_details))]
        public IHttpActionResult PostOrder_details(Order_details order_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Order_details.Add(order_details);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order_details.Id }, order_details);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(Order_details))]
        public IHttpActionResult DeleteOrder_details(int id)
        {
            Order_details order_details = db.Order_details.Find(id);
            if (order_details == null)
            {
                return NotFound();
            }

            db.Order_details.Remove(order_details);
            db.SaveChanges();

            return Ok(order_details);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_detailsExists(int id)
        {
            return db.Order_details.Count(e => e.Id == id) > 0;
        }
    }
}