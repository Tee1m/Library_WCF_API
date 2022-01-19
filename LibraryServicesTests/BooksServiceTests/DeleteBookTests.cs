using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Moq;

namespace LibraryServicesTests.BooksServiceTests
{
    [TestClass]
    class DeleteBookTests
    {
        Book book = new BookBuilder()
            .SetId(1)
            .SetTitle("Test")
            .SetAuthorName("Test")
            .SetAuthorSurname("Test")
            .SetAvailability(1)
            .SetDescription("Test")
            .Build();

    }
}
