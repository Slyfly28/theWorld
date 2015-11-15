using System;
using System.ComponentModel.DataAnnotations;

namespace theWorld.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)] // Check to see if the string validates the Name qualities
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(1024, MinimumLength = 5)]
        public string Message { get; set; }
    }

}