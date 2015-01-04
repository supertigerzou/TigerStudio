using System.Collections.Generic;
using TigerStudio.Books.Data.DomainModels;

namespace TigerStudio.Books.Services
{
    public interface IBookService
    {
        List<Book> GetAll();
    }
}