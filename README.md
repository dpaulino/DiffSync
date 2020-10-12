# DiffSync
A .NET Standard 2.0 library to help with data synchronization, such as determining object differences, applying patches, and merging changes.

Install from nuget: https://www.nuget.org/packages/DiffSync

## Diff and Patch
```csharp
using DiffSync;
using JeniusApps.Nightingale.Data.Models;

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

// Here is the diff
var comparisonResult = w1.Diff(w2);

// Apply the diff to w1 to produce a new obj
var w3 = w1.Patch(comparisonResult);

// Try getting the diff between obj2 and obj3
var newResult = w2.Diff(w3);

// They should be identical.
Assert.True(newResult.AreEqual);
```

## Merge
```csharp
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

var mergeResult = wBase.Merge(wFork1, wFork2);
var resultDiff = expectedMergeResult.Diff(mergeResult);
Assert.True(resultDiff.AreEqual);
```

# Copyright and open source license notices

- This library uses [Compare-Net-Objects](https://github.com/GregFinzer/Compare-Net-Objects) to calculate the diff for objects. Its license is here: https://github.com/GregFinzer/Compare-Net-Objects/wiki/Licensing.
- This library uses [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json). Its license is here: https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md
