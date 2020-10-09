# DiffSync
A .NET Standard 2.0 library to help with data synchronization, such as determining object differences, applying patches, and merging changes.

# Sample Usage

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
