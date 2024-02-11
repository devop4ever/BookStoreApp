using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Data.DTOs.Author;
using BookStoreApp.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(BookStoreContext context, IMapper mapper, ILogger<AuthorsController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorGetDto>>> GetAuthors()
    {
        try
        {
            var authors = await _context.Authors.ToListAsync();
            var authorsGetDto = _mapper.Map<List<AuthorGetDto>>(authors);

            return Ok(authorsGetDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthors)}");
            return StatusCode(500, ex.Message);
        }
    
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorGetDto>> GetAuthor(int id)
    {

        try
        {
            var authorGetDto = _mapper.Map<AuthorGetDto>(await _context.Authors.FindAsync(id));

            if (authorGetDto == null)
            {
                return NotFound();
            }

            return Ok(authorGetDto);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, $"Error performing GET in {nameof(GetAuthors)}");
            return StatusCode(500, ex.Message);

        }
        
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, AuthorPutDto authorDto)
    {

        if (id != authorDto.Id)
        {
            return BadRequest();
        }

        var author = await _context.Authors.FindAsync(id);


        if (author == null)
        {

            return BadRequest();
        }

        _mapper.Map(authorDto, author);
        _context.Entry(author).State = EntityState.Modified;


        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await AuthorExists(id))
            {
                _logger.LogError(ex, $"Could not find author {id}");
                return NotFound();
            }
            else
            {
                _logger.LogError(ex, $"Could not perform update of id: {id}");
                return BadRequest();
            }
        }

        return NoContent();
    }

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorPostDto>> PostAuthor(AuthorPostDto authorDto)
    {

        try
        {
            var author = _mapper.Map<Author>(authorDto);

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There's and error while adding occured");
            return BadRequest();
        }
        
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        try
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "There is an error while deleting the author");
            return StatusCode(500, ex.Message);
        }
        
    }

    private async Task<bool> AuthorExists(int id)
    {
        return await _context.Authors.AnyAsync(e => e.Id == id);
    }
}
