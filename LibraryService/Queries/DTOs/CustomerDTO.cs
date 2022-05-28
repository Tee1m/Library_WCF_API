using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    [DataContract]
    public class CustomerDTO : IDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string TelephoneNumber { get; set; }
    }
}
