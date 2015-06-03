using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakersMarket.Web.Areas.LoggedUser.Models
{
    using System.ComponentModel.DataAnnotations;
    using MakersMarket.Models;

    public class ShopInputViewModel
    {
        public string UserId { get; set; }

        public int ShopId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}