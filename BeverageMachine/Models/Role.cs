using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class Role : IdentityRole<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public override string Name { get; set; }

        public Role(string name)
        {
            Name = name;
        }

    }
}
