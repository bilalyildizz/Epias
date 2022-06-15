using Epias.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Epias.Models
{
    public partial class BulletinsMonthly : IEntity
    {
        public long Id { get; set; }
        public string Link { get; set; } = null!;
        public string Title { get; set; } = null!;
    }
}
