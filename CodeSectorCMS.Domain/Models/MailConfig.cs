using System;

namespace CodeSectorCMS.Domain
{
    public class MailConfig : BaseEntity
    {
        public int MailConfigID { get; set; }
        public int UserId { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "E-mail")]
        //[MaxLength(50)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Port is required.")]
        public int Port { get; set; }

        //[Required(ErrorMessage = "SMTP server address is required.")]
        //[Display(Name = "SMTP server address")]
        public string SMTPServerAddr { get; set; }

        public Boolean UseSSL { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Campaign>? Campaigns { get; set; }
    }
}
