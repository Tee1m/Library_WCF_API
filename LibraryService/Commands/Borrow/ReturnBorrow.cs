using System.Runtime.Serialization;

namespace Application
{
    [DataContract]
    public class ReturnBorrow : ICommand
    {
        [DataMember]
        public int BorrowId { get; set; }
    }
}
