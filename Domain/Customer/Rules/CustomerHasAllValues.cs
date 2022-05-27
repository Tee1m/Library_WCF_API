namespace Domain
{
    public class CustomerHasAllValues : IBusinessRule
    {
        private readonly Customer _customer;
        public string Message => "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";

        public CustomerHasAllValues(Customer customer)
        {
            this._customer = customer;
        }

        public bool NotValid()
        {
            return !(_customer.Name != null && _customer.Surname != null && _customer.Address != null && _customer.TelephoneNumber != null);
        }
    }
}


