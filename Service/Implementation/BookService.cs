using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Branch> _branchRepository;

        public BookService(IGenericRepository<Book> bookrepository, IMapper mapper, IGenericRepository<Branch> branchRepository)
        {
            _repository = bookrepository;
            _mapper = mapper;
            _branchRepository = branchRepository;
        }

        public IQueryable<BookResponseDto> GetBookQuery()
        {
            var bookQuery = _repository.GetQueryable().Include(b => b.Branch);
            var branchQuery = _branchRepository.GetQueryable();

            return from book in bookQuery
                   join branch in branchQuery on book.BranchId equals branch.Id into br
                   from branch in br.DefaultIfEmpty() // Left Join
                   select new BookResponseDto
                   {
                       Id = book.Id,
                       Title = book.Title,
                       Author = book.Author,
                       Publisher = book.Publisher,
                       Genre = book.Genre,
                       ISBN = book.ISBN,
                       YearPublished = book.YearPublished,
                       CopiesAvailable = book.CopiesAvailable,
                       BookShelfNumber = book.BookShelfNumber,
                       Picture = book.Picture,
                       BranchName = book.Branch != null ? book.Branch.Name : "N/A"
                   };
        }




        public async Task<IEnumerable<BookResponseDto>> GetAll()
        {
            return await GetBookQuery().ToListAsync();

        }


        public async Task<BookResponseDto> GetById(Guid id)
        {
            return await GetBookQuery().FirstOrDefaultAsync(b => b.Id == id);

        }
        public async Task<string> Create(BookRequestDto bookRequestDto)
        {           
            var newBook = _mapper.Map<Book>(bookRequestDto);    
            newBook.Id = Guid.NewGuid();

            if(bookRequestDto.Picture != null && bookRequestDto.Picture.Length > 0 )
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BookImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                var uniqueFileName = $"{Guid.NewGuid()}_{bookRequestDto.Picture.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await bookRequestDto.Picture.CopyToAsync(fileStream);
                }
                newBook.Picture = $"/BookImages/{uniqueFileName}"; // Relative path

            }

            var books = await _repository.AddAsync(newBook);
            return "Book Created Sucessfully";

        }
        public async Task<string> Update(Guid id, BookRequestDto bookRequestDto)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return "Book Not Found";
            }

            _mapper.Map(bookRequestDto, existingBook);
            if (bookRequestDto.Picture != null && bookRequestDto.Picture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BookImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = $"{Guid.NewGuid()}_{bookRequestDto.Picture.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await bookRequestDto.Picture.CopyToAsync(fileStream);
                }
                if (!string.IsNullOrEmpty(existingBook.Picture))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBook.Picture.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                existingBook.Picture = $"/BookImages/{uniqueFileName}"; // Update new picture path
            }

            await _repository.UpdateAsync(existingBook);
            return "Updated";
        }
        public async Task<string> Delete(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return "Book does not Exist";

            }
            if (!string.IsNullOrEmpty(book.Picture))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.Picture.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

           var books =  await _repository.DeleteAsync(id);
            return "Deleted";       
    }
        public async Task<(IEnumerable<BookSearchResponseDto> Books, int TotalCount)> GetFilteredBooks(BookSearchRequestDto filterRequest)
        {
            var query = _repository.GetQueryable();

            // Search Filter - Case Insensitive

            if (!string.IsNullOrEmpty(filterRequest.Search))
            {
                string searchLower = filterRequest.Search.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchLower) ||
                    b.Author.ToLower().Contains(searchLower) ||
                    b.Publisher.ToLower().Contains(searchLower));
            }
            // Genre Filter - multiple

            if (filterRequest.Genre != null && filterRequest.Genre.Any())
            {
                query = query.Where(b => filterRequest.Genre.Contains(b.Genre));
            }
            // YearPublished Filter


            if (filterRequest.YearPublished.HasValue)
            {
                query = query.Where(b => b.YearPublished == filterRequest.YearPublished);
            }
            // Branch Filter - 
            if (!string.IsNullOrEmpty(filterRequest.BranchName))
            {
                query = query.Where(b => b.Branch != null && b.Branch.Name.Contains(filterRequest.BranchName));
            }


            //Apply Sorting
            query = filterRequest.SortBy?.ToLower() switch
            {
                "title" => filterRequest.IsAscending ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title),
                "author" => filterRequest.IsAscending ? query.OrderBy(b => b.Author) : query.OrderByDescending(b => b.Author),
                "publisher" => filterRequest.IsAscending ? query.OrderBy(b => b.Publisher) : query.OrderByDescending(b => b.Publisher),
                _ => query.OrderBy(b => b.Title) // Default sorting by Title
            };

            // Apply Pagination
            int totalCount = await query.CountAsync();
            var books = await query
            .Skip((filterRequest.PageNumber - 1) * filterRequest.PageSize)
            .Take(filterRequest.PageSize)
                .ToListAsync();

            if (books == null || !books.Any())
            {
                return (null, 0); // No books found
            }
            var bookDtos = _mapper.Map<IEnumerable<BookSearchResponseDto>>(books);

            return (bookDtos, totalCount);
        }

    }
}
