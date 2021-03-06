using NUnit.Framework;
using System.Collections.Generic;
using System;
using NetworkTools;
using System.Linq;

[assembly: CLSCompliant(false)]
namespace NetworkTools.TESTS
{
    /// <summary>
    /// Ez az osztály a <see cref="ListProcessor.GetSummarizedStats(IEnumerable{PingStats})"/> metódusát teszteli.
    /// </summary>
    [TestFixture]
    public class ListProcessorTester
    {
        [Test]
        public void DisfunctionalWithNullParameter()
        {
            Assert.That(() => ListProcessor.GetSummarizedStats(null), Throws.ArgumentNullException);
        }

        [Test]
        public void ArgumentListCountEqualsReturnListCount()
        {
            List<PingStats> emptyList = new ();
            List<PingStats> singleItem = new () { new PingStats() };

            List<PingStats> zeroCount = ListProcessor.GetSummarizedStats(emptyList).ToList();
            List<PingStats> oneCount = ListProcessor.GetSummarizedStats(singleItem).ToList();

            Assert.That(zeroCount.Count, Is.EqualTo(emptyList.Count));
            Assert.That(oneCount.Count, Is.EqualTo(singleItem.Count));
        }

        [Test]
        public void ArgumentListHasSingleHostReturnedListHasSingleItem()
        {
            List<PingStats> input = new ()
            {
                new PingStats { Address = "host.xy" },
                new PingStats { Address = "host.xy" },
                new PingStats { Address = "host.xy" },
            };
            List<PingStats> expected = new () { new PingStats { Address = "host.xy" } };

            List<PingStats> output = ListProcessor.GetSummarizedStats(input).ToList();

            Assert.That(output.Count, Is.EqualTo(expected.Count));
            Assert.That(output, Is.EquivalentTo(expected));
        }

        [Test]
        public void ArgumentListHasOnlyDifferentHostsReturnedListIsEquivalent()
        {
            List<PingStats> input = new ()
            {
                new PingStats { Address = "first.xy" },
                new PingStats { Address = "second.xy" },
                new PingStats { Address = "third.xy" },
            };

            List<PingStats> output = ListProcessor.GetSummarizedStats(input).ToList();

            Assert.That(output, Is.EquivalentTo(input));
        }

        /* TO-DO:
         * Check if the method works well using at least three different test cases coming from a TestCaseSource.
         * Legalább három különböző, életszerű tesztesetet produkáló TestCaseSource segítségével ellenőrizze a metódus helyes működést.
         */
    }
}
