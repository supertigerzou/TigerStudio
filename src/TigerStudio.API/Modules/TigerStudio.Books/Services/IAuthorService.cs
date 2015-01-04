using System.Collections.Generic;
using TigerStudio.Books.Data.DomainModels;

namespace TigerStudio.Books.Services
{
    public interface IAuthorService
    {
        List<Author> GetAll();
        Author GetById(long authorId);
    }
}