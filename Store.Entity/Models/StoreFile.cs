using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.Models
{
    public class StoreFile
    {
        [Key]
        public int FileId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string FilePath { get; set; } = null!;
    }
}
