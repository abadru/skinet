using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController: BaseController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        
        [HttpGet("notfound")]
        public ActionResult NotFoundRequest()
        {
            var thing = _context.Products.Find(100);

            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            
            return Ok();
        }
        
        [HttpGet("servererror")]
        public ActionResult ServerErrorRequest()
        {
            var thing = _context.Products.Find(100);
            var thingToReturn = thing.ToString();
            return Ok();
        }
        
        
        [HttpGet("Bad")]
        public ActionResult BadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        
        [HttpGet("Bad/{id}")]
        public ActionResult NotFoundRequest(int id)
        {
            return Ok();
        }
        
        
    }
}