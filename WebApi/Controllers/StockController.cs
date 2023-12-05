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
        return StockDAO.Select(); // TODO
        
    }
    

}