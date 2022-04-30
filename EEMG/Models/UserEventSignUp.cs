using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEMG.Models
{
    public class UserEventSignUp
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Events")]
        public int EventId { get; set; }
        public Event Event { get; set; }
        //[ForeignKey("ApplicationUser")]
        //public int UserId { get; set; }
        //public ApplicationUser User { get; set; }
        public bool AttendingEvent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
        public bool PaidForEvent { get; set; }
    }
}
