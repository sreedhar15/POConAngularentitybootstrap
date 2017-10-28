using System.ComponentModel.DataAnnotations;

namespace LRPBookDomain.Entities
{
    public class ProjectGroup: Auditable
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
