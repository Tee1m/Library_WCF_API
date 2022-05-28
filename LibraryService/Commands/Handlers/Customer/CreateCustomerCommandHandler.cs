using Domain;

namespace Application
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerUniquenessChecker _uniquenessChecker;
        private IBusinessRule _rule;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerUniquenessChecker uniquenessChecker)
        {
            this._unitOfWork = unitOfWork;
            this._uniquenessChecker = uniquenessChecker;
        }

        public string Handle(CreateCustomer command)
        {
            Customer customer = new Customer();

            customer.Name = command.Name;
            customer.Surname = command.Surname;
            customer.TelephoneNumber = command.TelephoneNumber;
            customer.Address = command.Address;

            _rule = customer.HasAllValues();

            if (_rule.NotValid())
                return _rule.Message;

            _rule = customer.IsUnique(_uniquenessChecker);

            if (_rule.NotValid())
                return _rule.Message;

            _unitOfWork.CustomersRepository.Add(customer);
            _unitOfWork.Commit();

            return $"Dodano Klienta, P. {customer.Name} {customer.Surname}";
        }
    }
}
