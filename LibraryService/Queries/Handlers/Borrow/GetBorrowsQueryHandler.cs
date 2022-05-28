using System.Collections.Generic;
using System.Data;

namespace Application
{
    public class GetBorrowsQueryHandler : IQueryHandler<GetBorrows>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBorrowsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public IEnumerable<IDTO> Handle(GetBorrows query)
        {
            List<BorrowDTO> borrows = new List<BorrowDTO>();

            var connection = this._sqlConnectionFactory.GetOpenConnection();
            var sqlQuery = "Select *" +
                           "From [Wypozyczenia]";

            IDbCommand command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;

            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var borrowDTO = new BorrowDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    DateOfBorrow = reader.GetDateTime(reader.GetOrdinal("DateOfBorrow")),
                };

                if (!reader.IsDBNull(reader.GetOrdinal("Return")))
                    borrowDTO.Return = reader.GetDateTime(reader.GetOrdinal("Return"));

                borrows.Add(borrowDTO);
            }

            connection.Dispose();

            return borrows;
        }
    }
}
