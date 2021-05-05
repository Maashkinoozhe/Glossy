using System;

namespace GlossyCore.Models
{
    public class Entry : IEquatable<Entry>
    {
        public Guid Id {get;set;}
        public string Abbreviation { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string AlternativeTitel { get; set; }

        bool IEquatable<Entry>.Equals(Entry other)
        {
            return Id.Equals(other.Id);
        }

        public override string ToString()
        {
            return $"{Abbreviation} | {Titel} ({AlternativeTitel})";
        }
    }
}
