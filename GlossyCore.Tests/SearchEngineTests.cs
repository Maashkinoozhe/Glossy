using FluentAssertions;
using GlossyCore.Models;
using GlossyCore.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace GlossyCore.Tests
{
    [ExcludeFromCodeCoverage]
    public class SearchEngineTests
    {
        [Fact]
        public void Constructor_Throws_ArgumentNullException_If_Context_IsNull()
        {
            Func<IEngine<Entry>> createInstace = () => new SearchEngine(null);
            ArgumentNullException ex = createInstace.Should().ThrowExactly<ArgumentNullException>().Which;
            ex.ParamName.Should().Be("dbContext");
        }

        [Theory]
        [InlineData("spw", "SPW")]
        [InlineData("apw", "SPW")]
        [InlineData("BAg", "tst1,tst2")]
        [InlineData("osm", "OSM,tst3")]
        public void Search_For_Terms(string query, string rawExpectation)
        {
            string[] expectation = rawExpectation.Split(",");

            MyContext context = GetTestDb();

            SearchEngine engine = new SearchEngine(context);

            List<Entry> expectedResult = engine.Search(query).ToList();

            expectedResult.ForEach(x => expectation.Should().Contain(x.Abbreviation));
        }

        private MyContext GetTestDb()
        {
            MyContext context = new InMemoryContext();
            context.Database.EnsureCreated();

            context.Add(new Entry() { Abbreviation = "SPW", Titel = "Sonne Plane Wanne", AlternativeTitel = "not relevant" });
            context.Add(new Entry() { Abbreviation = "DWH", Titel = "Dot Wham Ham", AlternativeTitel = "not relevant" });
            context.Add(new Entry() { Abbreviation = "OSM", Titel = "Open Street Map", AlternativeTitel = "not relevant" });

            context.Add(new Entry() { Abbreviation = "tst1", Titel = "Banae Apfel Gurke", AlternativeTitel = "not relevant" });
            context.Add(new Entry() { Abbreviation = "tst2", Titel = "Badewanne Watte Inge", AlternativeTitel = "not relevant" });
            context.Add(new Entry() { Abbreviation = "tst3", Titel = "Open Street Map", AlternativeTitel = "not relevant" });

            context.SaveChanges();

            return context;
        }
    }
}
