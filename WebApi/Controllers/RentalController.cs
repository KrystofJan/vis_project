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
        if (s.movies.Count == 0)
        {
            return BadRequest("Neni zadany filmy!");
        }
        
        if (s.startDate > s.endDate)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Datum vraceni je pred datem vypujceni!");
        }

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