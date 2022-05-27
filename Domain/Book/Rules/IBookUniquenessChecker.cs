namespace Domain
{
    public interface IBookUniquenessChecker
    {
        bool IsUnique(string title, string name, string surname);
    }
}
