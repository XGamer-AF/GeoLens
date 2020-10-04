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
   
    public class AEventTypesController : ApiController
    {

        private readonly IEventTypesRepository _rep;


        public AEventTypesController(IEventTypesRepository rep)
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
        public async Task<IHttpActionResult> Index(string search)
        {
            try
            {
                var result = await _rep.Index(search);

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
        public async Task<IHttpActionResult> Post(EventTypes obj)
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
       public async Task<IHttpActionResult> Put(EventTypes obj)
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
