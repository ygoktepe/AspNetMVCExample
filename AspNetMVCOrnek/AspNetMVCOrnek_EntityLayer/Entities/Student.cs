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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // byte olursa gerek var int se gerek yok. 
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisteredDate { get; set; }
        [Required] //Boş geçilmesin.
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İsim alanı en az 2 en çok 50 karakter olmalıdır.")]
        public string Name { get; set; }

        [Required] //Boş geçilmesin.
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisim alanı en az 2 en çok 50 karakter olmalıdır.")]
        public string Surname { get; set; }

        public bool IsDeleted { get; set; }

    }
}
