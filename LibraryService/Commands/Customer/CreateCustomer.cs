using System.Runtime.Serialization;

namespace Application
{
    [DataContract]
    public class CreateCustomer : ICommand
    {
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
