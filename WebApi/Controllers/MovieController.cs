using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetMovie")]
    public IEnumerable<Movie> Get()
    {
        return MovieDAO.Select();
    }
    
    [HttpGet("{id:int}")]
    public Movie GetId(int id)
    {
        return MovieDAO.SelectById(id);
    }
    
    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
    {
        try
        {
            if (movie == null)
                return BadRequest();

            int id = MovieDAO.Insert(movie);

            return StatusCode(StatusCodes.Status201Created, $"Successfully created an actor with {id} id");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
        }
    }
}
