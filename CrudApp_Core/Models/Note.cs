using System;
using System.ComponentModel.DataAnnotations;

namespace CrudApp_Core
{
    public class Note
    {

            [Key]
            public int Id { get; set; }
            [MaxLength(30)]
            [Required]
            public string Title { get; set; }
            [Required]
            public string Text { get; set; }
            [Required]
            [MaxLength(30)]
            [RegularExpression(@"^[A-Za-z' 'äöüÄÖÜ]+$", ErrorMessage = "Invalid Autor Name")]
            public string Autor { get; set; }
            [Required]
            public string Tag { get; set; }
            [MaxLength(40)]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            [Display(Name = "Email address")]
            public string Email { get; set; }
            public bool Archiv { get; set; }                                                                   
            public string UserId { get; set; }
            [Display(Name = "Letzte änderung")]
            public DateTime Created { get; set; }
            [Display(Name = "Start time")]
            //[DisplayFormat(DataFormatString = "", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime? start { get; set; }
            [DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
            [Display(Name = "End time")]
            public DateTime? end { get; set; }

        
    }

}
