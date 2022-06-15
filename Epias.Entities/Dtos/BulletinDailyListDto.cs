using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Models;
using Epias.Models;

namespace Epias.Entities.Dtos
{
    public class BulletinDailyListDto
    {
        public IList<BulletinDaily> BulletinDailies { get; set; }
    }
}
