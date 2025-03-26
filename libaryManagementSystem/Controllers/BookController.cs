using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Implementation;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();
            return Ok(books);
        }
        [HttpGet("{id}/GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetById(id);
            if (book == null)
            {
                return NotFound("Book Not Found");
            }
            return Ok(book);
        }

        [HttpPost("Add")]

        public async Task<IActionResult> Add([FromForm] BookRequestDto bookRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.Create(bookRequestDto);
                    return Ok(new { message="Book Added Successfully. "});
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromForm] BookRequestDto bookRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            try
            {
                await _bookService.Update(id, bookRequestDto);
                return Ok(new { message = "Book updated successfully" });
            }

            catch (Exception)
            {


                return StatusCode(500, "An error occurred while updating the book");
            }
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _bookService.Delete(id);
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the book.", details = ex.Message });

            }}
            [HttpGet("Search-Books")]
        public async Task<IActionResult> GetFilteredBooks([FromQuery] BookSearchRequestDto searchRequestDto)
        {
            var (books, totalCount) = await _bookService.GetFilteredBooks(searchRequestDto);

            return Ok(new
            {
                TotalCount = totalCount,
                Books = books
            });
        }
    }
}
