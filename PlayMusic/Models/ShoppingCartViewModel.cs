using System.Collections.Generic;
using PlayMusic.Models;

namespace PlayMusic.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }

        public decimal FinalTotal { get; set; }
    }
}