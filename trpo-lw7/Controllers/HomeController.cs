using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trpo_lw7.Models;

namespace trpo_lw7.Controllers
{
    public class HomeController : Controller
    {
        private TracksDBContext db;
        public HomeController(TracksDBContext tracks)
        {
            db = tracks;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = $"Музыкальные треки - вариант { ('B' * 'N') % 7}";
            var tracks = db.Tracks.Include("Musician").ToList();

            return View(tracks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult CreateTrack()
        {
            ViewBag.Musicians = db.Musicians.ToList();
            Track track = new Track();
            return View(track);
        }
        [HttpPost]
        public IActionResult CreateTrack(Track track)
        {
            if (ModelState.IsValid)
            {
                if (db.Tracks.Any(t => t.Id == track.Id))
                {
                    db.Tracks.Remove(db.Tracks.FirstOrDefault(t => t.Id == track.Id));
                }
                db.Tracks.Add(track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult EditTrack(Track track)
        {
            ViewData["Musicians"] = db.Musicians.ToList();
            ViewData["Id"] = track.Id;
            return View("CreateTrack", track);
        }

        public IActionResult DeleteTrack(Track track)
        {
            db.Tracks.Remove(track);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Musicians()
        {
            ViewData["Title"] = $"Музыканты";
            var musicians = db.Musicians.Include("Tracks").ToList();

            return View(musicians);
        }

        [HttpGet]
        public IActionResult CreateMusician()
        {
            ViewData["Tracks"] = db.Tracks.ToList();
            Musician musician = new Musician();
            return View(musician);
        }
        [HttpPost]
        public IActionResult CreateMusician(Musician musician)
        {
            if (ModelState.IsValid)
            {
                musician.Tracks = musician.Tracks?.ToList() ?? Enumerable.Empty<Track>().ToList();
                if (db.Musicians.Any(t => t.Id == musician.Id))
                {
                    db.Musicians.Remove(db.Musicians.FirstOrDefault(t => t.Id == musician.Id));
                }
                db.Musicians.Add(musician);
                db.SaveChanges();
                return RedirectToAction("Musicians");
            }

            return View();
        }
        public IActionResult EditMusician(Musician musician)
        {
            ViewData["Tracks"] = db.Tracks.ToList();
            ViewData["Id"] = musician.Id;
            return View("CreateMusician", musician);
        }

        public IActionResult DeleteMusician(Musician musician)
        {
            db.Musicians.Remove(musician);
            db.SaveChanges();
            return RedirectToAction("Musicians");
        }

        public IActionResult Statistics()
        {
            StatisticsData statisticsData = new StatisticsData(db);
            ViewBag.statisticsData = statisticsData;
            return View();
        }
    }
}
