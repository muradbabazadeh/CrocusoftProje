using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        public int Id { get; set; }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }
    }
}
