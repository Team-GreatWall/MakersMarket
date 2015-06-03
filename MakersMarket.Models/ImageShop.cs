using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarket.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImageShop
    {
        [Key]
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int ShopId { get; set; }

        public Shop Shop { get; set; }
    }
}
