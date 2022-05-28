using System.Runtime.Serialization;

namespace Application
{
    [DataContract]
    public class CreateBorrow : ICommand
    {
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int BookId { get; set; }
    }
}
