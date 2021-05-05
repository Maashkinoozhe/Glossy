using GlossyCore;
using GlossyCore.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GlossyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Glossy");

            MyContext context = new MyContext();
            context.Database.Migrate();

            Console.WriteLine("Known entries:");
            foreach (Entry entry in context.Entries)
            {
                Console.WriteLine($"{entry.Abbreviation}\t{entry.Titel}\t{entry.Description}");
            }

            Entry newEntry = new Entry()
            {
                Abbreviation = "KISS",
                Titel = "Keep It Stupid Simple",
                Description = "The name says it all"
            };

            context.Add<Entry>(newEntry);
            context.SaveChanges();
        }
    }
}
