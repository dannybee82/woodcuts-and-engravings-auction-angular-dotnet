using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    [Table("DummyUsers", Schema = "Auction")]
    public class DummyUser
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
    }
}
