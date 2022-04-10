using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EEMG.Pages
{
    public class EventSignUpModel : PageModel
    {
        public int EventId { get; set; }
        [Required(ErrorMessage = "You must enter a First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must enter a Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You must enter an Organization")]
        public string Organization { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "You must enter a valid email")]
        public string Email { get; set; }

        public EventSignUpModel(int id)
        {
            this.EventId = id;
        }
    }
}
