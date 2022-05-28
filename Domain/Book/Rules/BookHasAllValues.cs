namespace Domain
{
    public class BookHasAllValues : IBusinessRule
    {
        private readonly Book _book;
        public string Message => "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
        
        public BookHasAllValues(Book book)
        {
            this._book = book;
        }

        public bool NotValid()
        {
            return !(_book.Title != null && _book.AuthorSurname != null && _book.AuthorName != null && _book.Description != null && _book.Amount > 0);
        }
    }
}
