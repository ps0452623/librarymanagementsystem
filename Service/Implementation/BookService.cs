using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Service.Interface;

namespace Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IHostEnvironment _hostEnvironment;
        public BookService(IGenericRepository<Book> bookrepository, IMapper mapper, IGenericRepository<Branch> branchRepository , IHostEnvironment hostEnvironment)
        {
            _repository = bookrepository;
            _mapper = mapper;
            _branchRepository = branchRepository;
            _hostEnvironment = hostEnvironment;
        }

        public string SaveBookPicture(IFormFile picture)
        {
            if (picture == null || picture.Length == 0) return null;
            var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "BookImages");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(picture.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                picture.CopyTo(fileStream);
            }
            return $"/BookImages/{uniqueFileName}";
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
        public async Task Create(BookRequestDto bookRequestDto)
        {           
            var newBook = _mapper.Map<Book>(bookRequestDto);    
            newBook.Id = Guid.NewGuid();
            if(bookRequestDto.Picture != null && bookRequestDto.Picture.Length > 0 )
            {
                newBook.Picture = SaveBookPicture(bookRequestDto.Picture);
            }
            try { 
            var books = await _repository.AddAsync(newBook);
               

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task Update(Guid id, BookRequestDto bookRequestDto)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            _mapper.Map(bookRequestDto, existingBook);
            if (bookRequestDto.Picture != null && bookRequestDto.Picture.Length > 0)
            {
                // Delete old picture if it exists
                if (!string.IsNullOrEmpty(existingBook.Picture))
                {
                    var oldFilePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", existingBook.Picture.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                existingBook.Picture = SaveBookPicture(bookRequestDto.Picture); // Update new picture
            }
          

            await _repository.UpdateAsync(existingBook);

            return;
        }
        public async Task Delete(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");


            }
            if (!string.IsNullOrEmpty(book.Picture))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.Picture.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _repository.DeleteAsync(id);
            return;
    }
        public async Task<(IEnumerable<BookSearchResponseDto> Books, int TotalCount)> GetFilteredBooks(BookSearchRequestDto filterRequest)
        {
            var query = _repository.GetQueryable().Include(x=>x.Branch).AsQueryable();

            // Search Filter - Case Insensitive

            if (!string.IsNullOrEmpty(filterRequest.Title))
            {
                string searchLower = filterRequest.Title.ToLower();
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


            if (!string.IsNullOrEmpty(filterRequest.YearPublished))
            {
                query = query.Where(b => b.YearPublished == Convert.ToInt32(filterRequest.YearPublished));
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
            var books = await query.Skip((filterRequest.PageNumber - 1) * 2)
                             .Take(2)
                             .ToListAsync();

            if (query == null || !query.Any())
            {
                return (null, 0); // No books found
            }
            var bookDtos = _mapper.Map<IEnumerable<BookSearchResponseDto>>(books);

            return (bookDtos, query.Count());
        }

    }
}
