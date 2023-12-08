using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RentalController : ControllerBase
{

    private readonly ILogger<RentalController> _logger;

    public RentalController(ILogger<RentalController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult<Rental>> Post(RentalPost s)
    {
        // TODO Important check the body
        
        try
        {
            if (s == null)
                return BadRequest();
            
            int id = RentalDAO.Insert(s);

            return StatusCode(StatusCodes.Status200OK, id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error posting rental");
        }
    }
}