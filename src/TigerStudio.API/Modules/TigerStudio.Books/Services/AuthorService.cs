using TigerStudio.Books.Data.DomainModels;
using TigerStudio.Framework.Data;
using System.Collections.Generic;
using System.Linq;

namespace TigerStudio.Books.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<Author> GetAll()
        {
            return _authorRepository.Table.ToList();
        }

        public Author GetById(long authorId)
        {
            return _authorRepository.GetById(authorId);
        }
    }
}
