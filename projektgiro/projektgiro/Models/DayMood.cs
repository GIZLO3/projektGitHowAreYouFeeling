using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace projektgiro.Models
{
    public class DayMood
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public MoodEnum Mood { get; set; }
    }
}
