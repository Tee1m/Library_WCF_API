namespace Domain
{
    public class CustomerTelephoneNumberUniqueRule : IBusinessRule
    {
        private readonly ICustomerUniquenessChecker _customerUniquenessChecker;

        private readonly string _telephoneNumber;

        public CustomerTelephoneNumberUniqueRule(
            ICustomerUniquenessChecker customerUniquenessChecker,
            string telephoneNumber)
        {
            _customerUniquenessChecker = customerUniquenessChecker;
            _telephoneNumber = telephoneNumber;
        }

        public bool NotValid() => _customerUniquenessChecker.IsUnique(_telephoneNumber);

        public string Message => "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
    }
}


