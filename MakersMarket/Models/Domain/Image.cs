using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakersMarket.Models.Domain
{
    public class Image
    {

        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}