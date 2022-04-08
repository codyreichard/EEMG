using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EEMG.Pages
{
    public class EventSignUpModel : PageModel
    {
        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }

        public EventSignUpModel(int id)
        {
            this.EventId = id;
        }
    }
}
