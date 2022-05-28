using System.Runtime.Serialization;

namespace Application
{
    [DataContract]
    public class CreateBook : ICommand
    {
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
