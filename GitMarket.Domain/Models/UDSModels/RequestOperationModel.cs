using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMarket.Domain.Models.UDSModels
{
    public class RequestOperationModel
    {
        public string? code { get; set; }
        public Participant? participant { get; set; }
        public string? nonce { get; set; }
        public Cashier? cashier { get; set; }
        public Receipt? receipt { get; set; }
        public int[]? tags { get; set; }
    }
    public class Cashier 
    {
        public string? externalId { get; set; }
        public string? name { get; set; }

    }
}
