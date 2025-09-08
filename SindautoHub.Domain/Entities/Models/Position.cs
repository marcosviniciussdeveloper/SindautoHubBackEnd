using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace SindautoHub.Domain.Entities.Models
{


    public class Position
    {

        public Guid? Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string? DescriptionDuties { get; set; }

      

        public ICollection<User> Users { get; set; } = new List<User>();
    }


}

