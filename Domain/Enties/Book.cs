using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string AuthorName { get; set; }

        [DataMember]
        public string AuthorSurname { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
