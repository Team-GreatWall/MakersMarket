namespace MakersMarket.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}