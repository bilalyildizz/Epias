using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epias.Entities.Interfaces;

namespace Epias.Entities.Models;
public class BulletinYear:IEntity
{
    public long Id { get; set; }
    public string Link { get; set; } = null!;
    public string Title { get; set; } = null!;
}

