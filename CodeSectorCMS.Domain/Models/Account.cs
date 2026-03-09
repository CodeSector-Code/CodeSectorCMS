using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
   public class Account : BaseEntity
    {
       public int AccountID { get; set; }
       public int ClientID { get; set; }

       //[Required(ErrorMessage = "Username is required.")]
       //[StringLength(20, ErrorMessage = "User name must be between 3 and 20 characters", MinimumLength = 3)]
       public string Username { get; set; }

       //[Required(ErrorMessage = "Password is required.")]
       //[StringLength(20, ErrorMessage = "Password must be between 4 and 20 characters", MinimumLength = 4)]
       public string Password { get; set; }

       //[Required(ErrorMessage = "Email is required.")]
       //[DataType(DataType.EmailAddress)]
       //[Display(Name = "E-mail")]
       //[MaxLength(50)]
       public string Email { get; set; }

       public virtual Client client { get; set; }
       public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
