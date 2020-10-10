using DiffSync;
using JeniusApps.Nightingale.Data.Models;
using System.Collections.Generic;
using Xunit;

namespace DiffSyncTest
{
    public class DiffSyncTests
    {
        [Fact]
        public void DiffTest()
        {
            var w1 = new Workspace
            {
                Name = "test",
                Id = "1",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "item1"
                    }
                }
            };

            var w2 = new Workspace
            {
                Name = "test",
                Id = "1",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "item1new"
                    },
                    new Item
                    {
                        Name = "item2"
                    }
                }
            };


            var comparisonResult = w1.Diff(w2);

            Assert.True(comparisonResult.Differences.Count == 2);
        }

        [Fact]
        public void PatchTest()
        {
            var w1 = new Workspace
            {
                Name = "test",
                Id = "1",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "item1"
                    }
                }
            };

            var w2 = new Workspace
            {
                Name = "test",
                Id = "1",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "item1new"
                    }
                }
            };

            var comparisonResult = w1.Diff(w2);

            var w3 = w1.Patch(comparisonResult);

            var newResult = w2.Diff(w3);

            Assert.True(newResult.AreEqual);
        }

        [Fact]
        public void MergeTest()
        {
            var wBase = new Workspace
            {
                Name = "test",
                Id = "1",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "item1"
                    }
                }
            };

            var wFork1 = new Workspace
            {
                Name = "test",
                Id = "1ConflictFork1",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "fork 1"
                    }
                }
            };

            var wFork2 = new Workspace
            {
                Name = "fork 2",
                Id = "1ConflictFork2",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "item1"
                    },
                    new Item
                    {
                        Name = "foobar"
                    }
                }
            };

            var expectedMergeResult = new Workspace
            {
                Name = "fork 2",
                Id = "1ConflictFork2",
                Items = new List<Item>
                {
                    new Item
                    {
                        Name = "fork 1"
                    },
                    new Item
                    {
                        Name = "foobar"
                    }
                }
            };

            var mergeResult = wFork1.Merge(wFork2, wBase);
            var resultDiff = expectedMergeResult.Diff(mergeResult);
            Assert.True(resultDiff.AreEqual);
        }
    }
}
