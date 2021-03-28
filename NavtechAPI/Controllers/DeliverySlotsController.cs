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
    public class DeliverySlotsController : ApiController
    {
        private Product_ApiEntities db = new Product_ApiEntities();

        // GET: api/DeliverySlots
        public IQueryable<Delivery_Slots> GetDelivery_Slots()
        {
            return db.Delivery_Slots;
        }

        // GET: api/DeliverySlots/5
        [ResponseType(typeof(Delivery_Slots))]
        public IHttpActionResult GetDelivery_Slots(int id)
        {
            Delivery_Slots delivery_Slots = db.Delivery_Slots.Find(id);
            if (delivery_Slots == null)
            {
                return NotFound();
            }

            return Ok(delivery_Slots);
        }

        // PUT: api/DeliverySlots/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDelivery_Slots(int id, Delivery_Slots delivery_Slots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != delivery_Slots.Id)
            {
                return BadRequest();
            }

            db.Entry(delivery_Slots).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Delivery_SlotsExists(id))
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

        // POST: api/DeliverySlots
        [ResponseType(typeof(Delivery_Slots))]
        public IHttpActionResult PostDelivery_Slots(Delivery_Slots delivery_Slots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Delivery_Slots.Add(delivery_Slots);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = delivery_Slots.Id }, delivery_Slots);
        }

        // DELETE: api/DeliverySlots/5
        [ResponseType(typeof(Delivery_Slots))]
        public IHttpActionResult DeleteDelivery_Slots(int id)
        {
            Delivery_Slots delivery_Slots = db.Delivery_Slots.Find(id);
            if (delivery_Slots == null)
            {
                return NotFound();
            }

            db.Delivery_Slots.Remove(delivery_Slots);
            db.SaveChanges();

            return Ok(delivery_Slots);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Delivery_SlotsExists(int id)
        {
            return db.Delivery_Slots.Count(e => e.Id == id) > 0;
        }
    }
}