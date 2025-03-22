using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            else
            {
                return Ok(book);
            }
        }
        [HttpPost("Add")]
        
        public async Task<string> Add([FromForm]BookRequestDto bookRequestDto)
        {
             if(bookRequestDto == null)
            {
                return "Invalid Data";
            }
             var result = await _bookService.Create(bookRequestDto);
            if (result == "Created") ;
            return result;
        }
        [HttpPut("{id}/Update")]
        public async Task<string> Update(Guid id,[FromForm] BookRequestDto bookRequestDto )
        {
            if (bookRequestDto == null)
            {
                return "Invalid book data";
            }
            var existingBook = await _bookService.GetById(id);
            if (existingBook == null)
            {
                return "Book Not Found";
            }
            var book = await _bookService.Update(id, bookRequestDto);
            if (book == "Updated")
            {
                return "Book Updated Sucessfully";
            }
            return book;
        }
        [HttpDelete("{id}/Delete")]
        public async Task<string> Delete(Guid id)
        {
            var result = await _bookService.Delete(id);

            if (result == "Deleted")  
            {
                return ("Book Deleted Successfully");
            }

            return ("Book Not Found or Could Not Be Deleted");
        }
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
