using TigerStudio.Books.Data;
using TigerStudio.Books.Data.DomainModels;
using TigerStudio.Framework.Caching;
using TigerStudio.Framework.Data;
using System.Collections.Generic;
using System.Linq;

namespace TigerStudio.Books.Services
{
    public class BookService : IBookService
    {
        private const string BooksAllKey = "GF.book.all";

        private readonly IRepository<Book> _bookRepository;
        private readonly ICacheManager _cacheManager;

        public BookService(ICacheManager cacheManager,
            IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
            _cacheManager = cacheManager;
        }

        public List<Book> GetAll()
        {
            return _cacheManager.Get(BooksAllKey, () => _bookRepository.Table.ToList());
        }
    }
}
