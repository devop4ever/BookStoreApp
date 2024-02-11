using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Data.Entities;
using BookStoreApp.API.Data.DTOs.Author;
using AutoMapper;

namespace BookStoreApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreContext _context;
    private readonly IMapper _mapper;

    public AuthorsController(BookStoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorGetDto>>> GetAuthors()
    {
        var authors = await _context.Authors.ToListAsync();
        var authorsGetDto = _mapper.Map<List<AuthorGetDto>>(authors);

        return Ok(authorsGetDto);
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorGetDto>> GetAuthor(int id)
    {
        var authorGetDto = _mapper.Map<AuthorGetDto>(await _context.Authors.FindAsync(id));

        if (authorGetDto == null)
        {
            return NotFound();
        }

        return Ok(authorGetDto);
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
        catch (DbUpdateConcurrencyException)
        {
            if (!await AuthorExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorPostDto>> PostAuthor(AuthorPostDto authorDto)
    {

        var author = _mapper.Map<Author>(authorDto);

        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
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

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> AuthorExists(int id)
    {
        return await _context.Authors.AnyAsync(e => e.Id == id);
    }
}
