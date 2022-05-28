using System.Linq;

namespace Application
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomer>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private IBusinessRule _rule;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string Handle(DeleteCustomer command)
        {
            var customerQuery = _unitOfWork.CustomersRepository.Get();
            var borrowsQuery = _unitOfWork.BorrowsRepository.Get();

            if (!customerQuery.Where(x => x.TelephoneNumber == command.TelephoneNumber).Any())
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }

            var customer = customerQuery.Where(x => x.TelephoneNumber == command.TelephoneNumber).First();

            if (borrowsQuery.Where(x => x.CustomerId == customer.Id).Any())
            {
                return "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.";
            }

            _unitOfWork.CustomersRepository.Remove(customer);
            _unitOfWork.Commit();

            return $"Usunięto Klienta, P. {customer.Name} {customer.Surname}.";
        }
    }
}
