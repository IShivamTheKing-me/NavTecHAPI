using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using NavtechAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NavtechAPI.Controllers
{
    public class HomeController : ApiController
    {
        private Product_ApiEntities db = new Product_ApiEntities();
        [HttpGet]
        public IQueryable<Order_details> GetOrders()
        {
           return new OrderDetailsController().GetOrder_details();
        }
        [HttpGet]
        [Route("api/Home/{PageNumber}/{PageSize}")]
        public IEnumerable<Order_details> GetOrdersByPageIndex(int PageNumber,int PageSize)
        {
            var result = db.Order_details;
            int count = result.Count();
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = result.OrderBy(resultSet => resultSet.Id).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = PageNumber > 1 ? "Yes" : "No";
            var nextPage = PageNumber < TotalPages ? "Yes" : "No";
            var paginationMetadata = new
            {
                totalCount = count,
                pageSize = PageSize,
                currentPage = PageNumber,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            return items;
        }
        [HttpPost]
        [Route("api/Home/Shivam")]
        public IHttpActionResult CreateOrders(HttpRequestMessage request)
        {
            var test = request.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<object>(test.Result);
            //var resultdataM = data.Values();
            //JObject jdataObj = JObject.Parse(test);
            Order_details order_details = new Order_details();
            order_details.Cart_Value = 25;
            order_details.Customer_Id = "CUST002";
            order_details.Delivery_Slot = "2";
            order_details.Delivery_Date = "2021-03-23";
            order_details.Order_Time = DateTime.Now;
           // var jObject = await request.Content.ReadAsAsync<JObject>();
            //var data = JsonConvert.DeserializeObject<object>(test);
            //Order_details resultOrder = jObject;
            return new OrderDetailsController().PostOrder_details(order_details);
            //return null;
            //return Request.CreateResponse(HttpStatusCode.OK, test);
        }
    }
}
