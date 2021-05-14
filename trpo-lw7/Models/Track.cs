using System.Collections.Generic;
using trpo_lw7.Models;

namespace trpo_lw7.Models
{
    public enum genre
    {
        Chiptune, Dubstep, Electropop, House, Techno, Trance
    }

    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DurationInSeconds { get; set; }
        public int Year { get; set; }
        public genre Genre { get; set; }
        public int MusicianId { get; set; }
        public Musician Musician { get; set; }
    }
}