using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Entities.Customers
{
    public class Customer : IMustHaveTanent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        //public Address Address { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifiedBy { get; set; }
        public string TenantId { get ; set; }

        public Customer()
        {
            Status = true;
            CreatedDate = DateTime.UtcNow;
            ModifyDate = DateTime.UtcNow;
        }

    }
}
