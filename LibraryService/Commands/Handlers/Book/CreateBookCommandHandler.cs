using Domain;

namespace Application
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBook>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookUniquenessChecker _bookUniquenessChecker;
        private IBusinessRule _rule;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IBookUniquenessChecker uniquenessChecker)
        {
            this._unitOfWork = unitOfWork;
            this._bookUniquenessChecker = uniquenessChecker;
        }

        public string Handle(CreateBook command)
        {
            Book book = new Book();

            book.AuthorName = command.AuthorName;
            book.AuthorSurname = command.AuthorSurname;
            book.Title = command.Title;
            book.Amount = command.Amount;
            book.Description = command.Description;

            _rule = book.HasAllValues();

            if (_rule.NotValid())
                return _rule.Message;

            _rule = book.IsUnique(_bookUniquenessChecker);

            if (_rule.NotValid())
                return _rule.Message;

            _unitOfWork.BooksRepository.Add(book);
            _unitOfWork.Commit();

            return "Dodano Książkę do bazy danych.";
        }
    }
}
