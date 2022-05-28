using System.Collections.Generic;
using System.Data;

namespace Application
{
    public class GetBooksQueryHandler : IQueryHandler<GetBooks>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBooksQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public IEnumerable<IDTO> Handle(GetBooks query)
        {
            List<BookDTO> books = new List<BookDTO>();

            var connection = this._sqlConnectionFactory.GetOpenConnection();
            var sqlQuery = "Select *" +
                           "From [Ksiazki]";

            IDbCommand command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;

            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var bookDTO = new BookDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                    AuthorSurname = reader.GetString(reader.GetOrdinal("AuthorSurname")),
                    Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                };

                books.Add(bookDTO);
            }

            connection.Dispose();

            return books;
        }
    }
}
