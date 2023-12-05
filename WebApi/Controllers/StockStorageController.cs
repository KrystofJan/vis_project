using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("/Stock/Storage/")]
public class StockStorageController : ControllerBase
{

    private readonly ILogger<StockStorageController> _logger;

    public StockStorageController(ILogger<StockStorageController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("{id:int}")]
    public IEnumerable<Stock> GetByStorageId(int id)
    {
        return StockDAO.SelectByStorageId(id);
    }
}