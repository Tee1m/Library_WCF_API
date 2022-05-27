namespace Domain
{
    public interface ICustomerUniquenessChecker
    {
        bool IsUnique(string telephoneNumber);
    }
}


