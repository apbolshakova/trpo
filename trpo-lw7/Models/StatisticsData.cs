using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace trpo_lw7.Models
{
    public class TracksByYear
    {
        public int Year { get; set; }
        public int TrNum { get; set; }
    }

    public class AvgByGenre
    {
        public genre Genre { get; set; }
        public double AvgDur { get; set; }
    }

    public class TracksByMusician
    {
        public string Mus { get; set; }
        public int TrNum { get; set; }
    }

    public class StatisticsData
    {
        public int NumOfTracks;
        public int NumOfMusicians;
        public List<TracksByYear> NumOfTracksByYear;
        public List<AvgByGenre> AvgDurationByGenre;
        public List<TracksByMusician> NumOfTracksByMusician;

        public TracksDBContext db;

        public StatisticsData(TracksDBContext db)
        {
            this.db = db;
            
            this.NumOfTracks = this.db.Tracks.ToList().Count();
            this.NumOfMusicians = this.db.Musicians.Count();

            this.NumOfTracksByYear = this.db.Tracks.ToList().GroupBy(
                track => track.Year,
                track => track.Id, 
                (year, id) => new TracksByYear
                {
                    Year = year,
                    TrNum = id.Count(),
                }).ToList();

            this.AvgDurationByGenre = this.db.Tracks.ToList().GroupBy(
                track => track.Genre,
                track => track.DurationInSeconds,
                (genre, dur) => new AvgByGenre
                {
                    Genre =  genre,
                    AvgDur = dur.Average(),
                }).ToList();

            this.NumOfTracksByMusician = this.db.Musicians.ToList().GroupBy(
                musician => musician,
                musician => musician.Tracks?.Count() ?? 0,
                (musician, trNum) => new TracksByMusician
                {
                    Mus = musician.StageName,
                    TrNum = trNum.ToArray()[0],
                }).ToList();
        }
    }
}