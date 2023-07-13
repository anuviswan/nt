using System;
using System.Collections.Generic;
using Nt.Utils.Interfaces;

namespace Nt.Utils.Models;

public class Movie:IHasId
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string PlotSummary { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public string Genre { get; set; } 
}
