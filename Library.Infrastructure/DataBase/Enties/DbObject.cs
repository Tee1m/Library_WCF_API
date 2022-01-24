using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace LibraryService
{
    [DataContract]
    public class DbObject
    {
        [DataMember]
        public int Id { get; set; }
    }
}
