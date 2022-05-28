using System.Runtime.Serialization;

namespace Application
{
    [DataContract]
    public class DeleteCustomer : ICommand
    {
        [DataMember]
        public string TelephoneNumber { get; set; }
    }
}
