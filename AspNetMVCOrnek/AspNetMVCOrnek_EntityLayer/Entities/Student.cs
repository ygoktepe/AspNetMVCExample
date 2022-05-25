using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMVCOrnek_EntityLayer.Entities
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisteredDate { get; set; }
        [Required(ErrorMessage ="Zorunludur!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "betül alanı en az 2 en çok 50 karakter olmalıdır!")]
        // [MinLength(2,ErrorMessage ="")]
        [Display(Name="İsminiz")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisim alanı en az 2 en çok 50 karakter olmalıdır!")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }
        public bool IsDeleted { get; set; }

    }
}
