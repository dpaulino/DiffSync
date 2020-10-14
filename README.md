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
        }
    }
};

var diff = w1.Diff(w2);
var w3 = w1.Patch(diff);
var newResult = w2.Diff(w3);
Assert.Null(newResult);
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
Assert.Null(resultDiff);
```

# Attributions

- [jsondiffpatch.net](https://github.com/wbish/jsondiffpatch.net). License: https://github.com/wbish/jsondiffpatch.net/blob/master/LICENSE.
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json). License: https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md
