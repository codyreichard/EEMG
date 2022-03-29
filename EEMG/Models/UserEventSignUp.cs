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
        public Events Event { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool AttendingEvent { get; set; }
    }
}
