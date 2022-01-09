using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LibraryService
{
    public class DbObject
    {
        [Key]
        public int Id { get; set; }
    }
}
