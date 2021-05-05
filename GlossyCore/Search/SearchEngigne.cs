using GlossyCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GlossyCore.Search
{
    public class SearchEngine : IEngine<Entry>
    {
        private readonly MyContext dbContext;

        public SearchEngine(MyContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IList<Entry> Search(string query)
        {
            Regex regexQuery = CreateRegularExpression(query);

            return GetMatches(regexQuery);
        }

        private Regex CreateRegularExpression(string query)
        {
            query = EscapeQuery(query);
            query = InjectSearchTermMagic(query);
            return new Regex(query, RegexOptions.IgnoreCase);
        }

        private string EscapeQuery(string query)
        {
            return query
                .Replace("/", "//")
                .Replace(".", "/.")
                .Replace("*", "/*");
        }

        private string InjectSearchTermMagic(string query)
        {
            char[] magicCharacter = { '/' };
            StringBuilder builder = new StringBuilder();

            builder.Append(".*?");

            foreach (char character in query)
            {
                builder.Append(character);

                if (!magicCharacter.Contains(character))
                {
                    builder.Append(".*?");
                }
            }
            return builder.ToString();
        }

        private IList<Entry> GetMatches(Regex regex)
        {
            List<Entry> results = new List<Entry>();
            List<Entry> entries = dbContext.Entries.ToList();

            results.AddRange(entries.Where(x => regex.IsMatch(x.Abbreviation)).ToList());
            results.AddRange(entries.Where(x => regex.IsMatch(x.Titel) && !results.Contains(x)).ToList());
            results.AddRange(entries.Where(x => regex.IsMatch(x.AlternativeTitel) && !results.Contains(x)).ToList());

            return results;
        }
    }

}
