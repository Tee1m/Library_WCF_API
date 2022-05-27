using System.Linq;
using Domain;

namespace Application
{
    public class BookUniquenessChecker : IBookUniquenessChecker
    {
        private readonly IBooksRepository _booksRepository;

        public BookUniquenessChecker(IBooksRepository booksRepository)
        { 
            this._booksRepository = booksRepository;
        }

        public bool IsUnique(string title, string AuthorName, string AuthorSurname)
        {
            return _booksRepository.Get().Where(x => x.Title == title && x.AuthorName == AuthorName && x.AuthorSurname == AuthorSurname).Any();
        }
    }
}
