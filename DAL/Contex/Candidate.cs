using System;
using System.Collections.Generic;

namespace DAL.Contex
{
    public partial class Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? ScoreInSortedTest { get; set; }
    }
}
