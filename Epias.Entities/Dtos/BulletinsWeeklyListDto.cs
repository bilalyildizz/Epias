using Epias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Entities.Dtos
{
    public class BulletinsWeeklyListDto
    {
        public IList<BulletinsWeekly> BulletinsWeeklys { get; set; }
    }
}
