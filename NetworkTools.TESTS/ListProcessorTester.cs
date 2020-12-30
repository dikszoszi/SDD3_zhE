using NUnit.Framework;
using System.Collections.Generic;
using System;
using NetworkTools;

namespace NetworkTools.TESTS
{
    /// <summary>
    /// Ez az osztály a <see cref="ListProcessor.GetSummarizedStats(List{PingStats})"/> metódusát teszteli.
    /// </summary>
    [TestFixture]
    public class ListProcessorTester
    {
        [Test]
        public void NotWorking_With_NullParametre()
        {
            ListProcessor lp = new ListProcessor();
            Assert.That(() => lp.GetSummarizedStats(null), Throws.ArgumentNullException);
        }

        [Test]
        public void ArgumentListCount_SameAs_ReturnListCount()
        {
            ListProcessor lp = new ListProcessor();
            List<PingStats> emptyList = new List<PingStats>();
            List<PingStats> singleItem = new List<PingStats> { new PingStats() };

            List<PingStats> zeroCount = lp.GetSummarizedStats(emptyList);
            List<PingStats> oneCount = lp.GetSummarizedStats(singleItem);

            Assert.That(zeroCount.Count, Is.EqualTo(emptyList.Count));
            Assert.That(oneCount.Count, Is.EqualTo(singleItem.Count));
        }

        [Test]
        public void ArgumentListHasSingleHost_ReturnedListHasSingleItem()
        {
            ListProcessor lp = new ListProcessor();
            List<PingStats> input = new List<PingStats>
            {
                new PingStats { Address = "host.xy" },
                new PingStats { Address = "host.xy" },
                new PingStats { Address = "host.xy" },
            };
            List<PingStats> expected = new List<PingStats> { new PingStats { Address = "host.xy" } };

            List<PingStats> output = lp.GetSummarizedStats(input);

            Assert.That(output.Count, Is.EqualTo(expected.Count));
            Assert.That(output, Is.EquivalentTo(expected));
        }

        [Test]
        public void ArgumentListHasOnlyDifferentHosts_ReturnedListIsEquivalent()
        {
            ListProcessor lp = new ListProcessor();
            List<PingStats> input = new List<PingStats>
            {
                new PingStats { Address = "first.xy" },
                new PingStats { Address = "second.xy" },
                new PingStats { Address = "third.xy" },
            };

            List<PingStats> output = lp.GetSummarizedStats(input);

            Assert.That(output, Is.EquivalentTo(input));
        }

        /* TO-DO:
         * Check if the method works well using at least three different test cases coming from a TestCaseSource.
         * Legalább három különböző, életszerű tesztesetet produkáló TestCaseSource segítségével ellenőrizze a metódus helyes működést.
         */
    }
}
