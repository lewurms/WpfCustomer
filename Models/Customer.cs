using System.Collections.Generic;

namespace WpfApp2Test2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<YugiyohCard> Cards { get; set; }
    }
}
