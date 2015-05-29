using System.ComponentModel.DataAnnotations;

namespace MakersMarket.Models.View.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}