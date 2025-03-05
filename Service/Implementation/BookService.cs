using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using Service.Interface;

namespace Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> bookrepository, IMapper mapper)
        {
            _repository = bookrepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAll()
        {
            var book = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponseDto>>(book);
        }
            
        public async Task<BookResponseDto> GetById(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book != null)
            {
                return _mapper.Map<BookResponseDto>(book);
            }
            else
            {
                return null;
            }
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

    }
}
