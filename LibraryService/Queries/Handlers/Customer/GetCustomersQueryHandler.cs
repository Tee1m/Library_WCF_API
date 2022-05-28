using System.Collections.Generic;
using System.Data;

namespace Application
{
    public class GetCustomersQueryHandler : IQueryHandler<GetCustomers>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public IEnumerable<IDTO> Handle(GetCustomers query)
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();

            var connection = this._sqlConnectionFactory.GetOpenConnection();
            var sqlQuery = "Select *" +
                           "From [Klienci]";

                IDbCommand command = connection.CreateCommand();
                command.CommandText = sqlQuery;
                command.CommandType = CommandType.Text;

                IDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    var customerDTO = new CustomerDTO
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Surname = reader.GetString(reader.GetOrdinal("Surname")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        TelephoneNumber = reader.GetString(reader.GetOrdinal("TelephoneNumber"))
                    };

                    customers.Add(customerDTO);
                }

                connection.Dispose();

            return customers;
        }
    }
}
