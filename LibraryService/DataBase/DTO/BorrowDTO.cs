using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class BorrowDTO : DbObject
    {
        [DataMember]
        public string Customer { get; set; }

        [DataMember]
        public string Book { get; set; }

        [DataMember]
        public DateTime DateOfBorrow { get; set; }

        [DataMember]
        public DateTime? Return { get; set; }
    }
}
