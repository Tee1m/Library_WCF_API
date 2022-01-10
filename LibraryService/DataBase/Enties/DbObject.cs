using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LibraryService
{
    [DataContract]
    public class DbObject
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
    }
}
