using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{

    private readonly ILogger<StockController> _logger;

    public StockController(ILogger<StockController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetStock")]
    public IEnumerable<Stock> Get()
    {
        return StockDAO.Select();
        
    }
    
    [HttpPut]
    public async Task<ActionResult<Stock>> ReduceAmount(int movie_id, int storage_id, int amount)
    {
        try
        {
            Movie m = MovieDAO.SelectById(movie_id);           
            Storage s = StorageDAO.SelectById(storage_id);

            if (m == null || s == null)
                return NotFound();
            
            StockDAO.Reduce(movie_id, storage_id, amount);

            return StatusCode(StatusCodes.Status200OK, $"Successfully updated movie amount");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating movie amount");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Stock>> Post(Stock s)
    {
        try
        {
            if (s == null)
                return BadRequest();
            
            StockDAO.Insert(s);

            return StatusCode(StatusCodes.Status200OK, $"Successfully updated movie amount");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating movie amount");
        }
    }
}