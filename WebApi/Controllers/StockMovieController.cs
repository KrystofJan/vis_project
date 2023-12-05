using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("/Stock/Movie/")]
public class StockMovieController : ControllerBase
{

    private readonly ILogger<StockMovieController> _logger;

    public StockMovieController(ILogger<StockMovieController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("{id:int}")]
    public IEnumerable<Stock> GetByMovieId(int id)
    {
        return StockDAO.SelectByMovieId(id);
    }
}