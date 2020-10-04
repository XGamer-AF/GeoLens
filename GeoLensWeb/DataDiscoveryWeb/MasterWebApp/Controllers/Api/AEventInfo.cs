using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DataLayer;
using Data;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Data.ViewModel;

namespace MasterWebApp.Controllers.Api
{
   
    public class AEventInfoController : ApiController
    {

        private readonly IEventInfoRepository _rep;


        public AEventInfoController(IEventInfoRepository rep)
        {
            _rep = rep;
        }
        
         [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var result = await _rep.GetAllAsync();

                if (result.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Index(string search, string countryID, string eventTypeID, string dateFrom, string dateTo)
        {
            try
            {
                var result = await _rep.Index(search, countryID, eventTypeID, dateFrom, dateTo);

                if (result.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetEvent(string number, string eventTypeID, string countryID, string dateFrom, string dateTo)
        {
            try
            {
                var result = await _rep.GetEvent(number, eventTypeID, countryID, dateFrom, dateTo);

                if (result.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            try
            {
                var result = await _rep.GetByIDAsync(id);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

       [HttpPost]
        public async Task<IHttpActionResult> Post(EventInfo obj)
        {
            try
            {
                var result = await _rep.AddAsync(obj);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

     [HttpPut]
       public async Task<IHttpActionResult> Put(EventInfo obj)
        {
            try
            {
                var result = await _rep.UpdateAsync(obj);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id )
        {
            try
            {
                var result = await _rep.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> changeStatus(int id, int statusID)
        {
            try
            {
                var result = await _rep.ChangeStatusAsync(id, statusID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
