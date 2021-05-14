using System.Collections.Generic;

namespace trpo_lw7.Models
{
    public class Musician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StageName { get; set; }
        public int BirthYear { get; set; }
        public List<Track> Tracks { get; set; }
    }
}