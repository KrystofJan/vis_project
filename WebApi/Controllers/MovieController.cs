using System.Collections.ObjectModel;
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
    
    [HttpGet("search/{name}/{storage}")]
    public Collection<Movie> GetName(string name, int storage)
    {
        return MovieDAO.SelectByName(name, storage);
    }
    
    [HttpGet("search/{name}")]
    public Collection<Movie> GetName(string name)
    {
        return MovieDAO.Search(name);
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
