namespace Domain
{
    public interface IBusinessRule
    {
        bool NotValid();
        string Message { get; }
    }
}


