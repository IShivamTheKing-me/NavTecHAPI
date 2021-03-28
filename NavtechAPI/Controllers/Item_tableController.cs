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
    public class Item_tableController : ApiController
    {
        private Product_ApiEntities db = new Product_ApiEntities();

        // GET: api/Item_table
        public IQueryable<Item_table> GetItem_table()
        {
            return db.Item_table;
        }

        // GET: api/Item_table/5
        [ResponseType(typeof(Item_table))]
        public IHttpActionResult GetItem_table(int id)
        {
            Item_table item_table = db.Item_table.Find(id);
            if (item_table == null)
            {
                return NotFound();
            }

            return Ok(item_table);
        }

        // PUT: api/Item_table/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem_table(int id, Item_table item_table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item_table.Id)
            {
                return BadRequest();
            }

            db.Entry(item_table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Item_tableExists(id))
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

        // POST: api/Item_table
        [ResponseType(typeof(Item_table))]
        public IHttpActionResult PostItem_table(Item_table item_table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Item_table.Add(item_table);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item_table.Id }, item_table);
        }

        // DELETE: api/Item_table/5
        [ResponseType(typeof(Item_table))]
        public IHttpActionResult DeleteItem_table(int id)
        {
            Item_table item_table = db.Item_table.Find(id);
            if (item_table == null)
            {
                return NotFound();
            }

            db.Item_table.Remove(item_table);
            db.SaveChanges();

            return Ok(item_table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Item_tableExists(int id)
        {
            return db.Item_table.Count(e => e.Id == id) > 0;
        }
    }
}