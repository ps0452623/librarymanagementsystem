using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using static System.Reflection.Metadata.BlobBuilder;

namespace Service.Implementation
{
    public class BookFilterService : IBookFilterService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly  IMapper _mapper;
        private readonly IGenericRepository<Branch> _branchRepository;

        public BookFilterService(IGenericRepository<Book> bookFilterService, IMapper mapper, IGenericRepository<Branch> branchRepository)
        {
            _repository = bookFilterService;
            _mapper = mapper;
            _branchRepository = branchRepository;
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
