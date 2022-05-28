using System.Runtime.Serialization;

namespace Application
{
    [DataContract]
    public class DeleteBook : ICommand
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public string AuthorSurname { get; set; }
    }
}
