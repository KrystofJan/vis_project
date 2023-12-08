using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{

    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IEnumerable<Customer> Get()
    {
        return CustomerDAO.Select();
    }
}