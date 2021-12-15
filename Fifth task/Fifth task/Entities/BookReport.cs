using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth_task
{
    public class BookReport
    {
        public int Id { get; set; }

        public DateTime DateOfGiving { get; set; }

        public bool ReturnStatus { get; set; }

        public string StateOfBook { get; set; }

        public int BookId { get; set; }

        public int SubscriberId { get; set; }
    }
}
