using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models
{
    public class Cars
    {
        public int Id { get; set; }
        public string  Brand { get; set; }      
        public string  Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool  New { get; set; }

            
    }
}