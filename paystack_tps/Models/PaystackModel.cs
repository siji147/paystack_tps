using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paystack_tps.Models
{
    public class PaystackAddRecipientModel
    {
        public string type { get { return "nuban"; } }
        public string name { get; set; }
        public string acocunt_number { get; set; }
        public string bank_code { get; set; }
        public string currency { get { return "NGN"; } }

    }


    public class PaystackInitiateTransferModel
    {
        public string source { get { return "balance"; } }
        public decimal amount { get; set; }
        public string currency { get { return "NGN"; } }
        public string recipient { get; set; }
    }
}
