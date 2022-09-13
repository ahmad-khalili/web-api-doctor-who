﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorWho.Db.Models;
[Table(nameof(Companion))]
public class Companion
{
    public Companion()
    {
        EpisodeCompanions = new List<EpisodeCompanion>();
    }
    
    public int CompanionId { get; set; }
    public string CompanionName { get; set; }
    public string WhoPlayed { get; set; }

    public List<EpisodeCompanion> EpisodeCompanions { get; set; }
}