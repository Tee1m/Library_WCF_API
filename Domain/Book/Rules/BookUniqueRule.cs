namespace Domain
{
    public class BookUniqueRule : IBusinessRule
    {
        private readonly IBookUniquenessChecker _bookUniquenessChecker;

        private readonly string _title;
        private readonly string _name;
        private readonly string _surname;

        public BookUniqueRule(
            IBookUniquenessChecker bookUniquenessChecker,
            string title, string name, string surname)
        {
            this._bookUniquenessChecker = bookUniquenessChecker;
            this._title = title;
            this._name = name;
            this._surname = surname;
        }

        public bool NotValid() => _bookUniquenessChecker.IsUnique(_title, _name, _surname);

        public string Message => "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";
    }
}
