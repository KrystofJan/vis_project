using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{

    private readonly ILogger<StorageController> _logger;

    public StorageController(ILogger<StorageController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("{id:int}")]
    public Storage GetByStorageId(int id)
    {
        return StorageDAO.SelectById(id);
    }
    
    [HttpGet]
    public IEnumerable<Storage> Get()
    {   
        return StorageDAO.Select();
    }
}