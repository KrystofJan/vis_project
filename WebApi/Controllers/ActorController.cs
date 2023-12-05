using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{

    private readonly ILogger<ActorController> _logger;

    public ActorController(ILogger<ActorController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetActor")]
    public IEnumerable<Actor> Get()
    {
        return ActorDAO.Select();
    }
    
    [HttpGet("{id:int}")]
    public Actor GetId(int id)
    {
        return ActorDAO.SelectById(id);
    }
    
    [HttpPost]
    public async Task<ActionResult<Actor>> CreateActor(Actor actor)
    {
        try
        {
            if (actor == null)
                return BadRequest();

            int id = ActorDAO.Insert(actor);

            return StatusCode(StatusCodes.Status201Created, $"Successfully created an actor with {id} id");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
        }
    }
}