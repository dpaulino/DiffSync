using DiffSync;
using JeniusApps.Nightingale.Data.Models;
using Newtonsoft.Json;
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


            var diff = w1.Diff(w2);
            string expectedString = "{\r\n  \"Items\": {\r\n    \"_t\": \"a\",\r\n    \"0\": {\r\n      \"Name\": [\r\n        \"item1\",\r\n        \"item1new\"\r\n      ]\r\n    },\r\n    \"1\": [\r\n      {\r\n        \"id\": null,\r\n        \"parentId\": null,\r\n        \"IsTemporary\": false,\r\n        \"Properties\": null,\r\n        \"Url\": null,\r\n        \"Auth\": null,\r\n        \"Body\": null,\r\n        \"MockData\": null,\r\n        \"Children\": null,\r\n        \"Headers\": null,\r\n        \"ChainingRules\": null,\r\n        \"Type\": 0,\r\n        \"Name\": \"item2\",\r\n        \"IsExpanded\": false,\r\n        \"Method\": null,\r\n        \"Response\": null\r\n      }\r\n    ]\r\n  }\r\n}";

            Assert.Equal(expectedString, diff.ToString());
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

            var diff = w1.Diff(w2);
            var w3 = w1.Patch(diff);
            var newResult = w2.Diff(w3);
            Assert.Null(newResult);
        }

        [Fact]
        public void PatchDictionaryTest()
        {
            var a1 = new Authentication
            {
                AuthProperties = new Dictionary<string, string>
                {
                    { "Test", "value" }
                }
            };

            var a2 = new Authentication
            {
                AuthProperties = new Dictionary<string, string>
                {
                    { "Test", "value2" }
                }
            };

            var diff = a1.Diff(a2);
            var a3 = a1.Patch(diff);
            var newResult = a2.Diff(a3);
            Assert.Null(newResult);
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

            var mergeResult = wBase.Merge(wFork1, wFork2);
            var resultDiff = expectedMergeResult.Diff(mergeResult);
            Assert.Null(resultDiff);
        }

        [Fact]
        public void ComplexObj1ChangeOnlyMergeTest()
        {
            var wBase = JsonConvert.DeserializeObject<Workspace>("{\"id\":\"d89c9279-1e45-41a7-9330-c35429509856\",\"parentId\":\"root\",\"Name\":\"magic\",\"Methods\":[\"GET\",\"POST\",\"PUT\",\"DELETE\",\"HEAD\",\"OPTIONS\",\"PATCH\",\"MERGE\",\"COPY\"],\"OpenItemIds\":[\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\"],\"TempItems\":[],\"Items\":[{\"id\":\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"\",\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":{}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"My request\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null},{\"id\":\"fa796649-43a2-4dfb-8a87-01b46382ab14\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{},\"Url\":{\"Base\":null,\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":null},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[{\"id\":\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"44444444444444444444444444444444444?\",\"Queries\":[{\"Key\":\"3333\",\"Value\":\"232424\",\"Enabled\":true,\"Private\":false,\"Type\":0}]},\"Auth\":{\"AuthType\":2,\"AuthProperties\":{\"BasicUsername\":\"asdf\",\"BasicPassword\":\"asdf\",\"DigestUsername\":\"asdf\",\"DigestPassword\":\"asdf\",\"BearerToken\":\"asdf\",\"OAuth2GrantType\":\"authorization_code\",\"OAuth2AccessTokenUrl\":\"asf\",\"OAuth2ClientSecret\":\"asdf\",\"OAuth1ConsumerSecret\":\"asdf\",\"OAuth1ConsumerKey\":\"asdf\"}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":\"{salfaslkdfj}\",\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"234234\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null}],\"Headers\":[],\"ChainingRules\":[],\"Type\":2,\"Name\":\"Untitled\",\"IsExpanded\":true,\"Method\":\"GET\",\"Response\":null}],\"HistoryItems\":[],\"Environments\":[{\"id\":null,\"parentId\":null,\"Name\":\"Base\",\"EnvironmentType\":1,\"Variables\":[{\"Key\":\"env\",\"Value\":\"value\",\"Enabled\":true,\"Private\":false,\"Type\":3}]}],\"Cookies\":null}");
            var wFork2 = JsonConvert.DeserializeObject<Workspace>("{\"id\":\"d89c9279-1e45-41a7-9330-c35429509856\",\"parentId\":\"root\",\"Name\":\"magic\",\"Methods\":[\"GET\",\"POST\",\"PUT\",\"DELETE\",\"HEAD\",\"OPTIONS\",\"PATCH\",\"MERGE\",\"COPY\"],\"OpenItemIds\":[\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\"],\"TempItems\":[],\"Items\":[{\"id\":\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"\",\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":{}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"My request\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null},{\"id\":\"fa796649-43a2-4dfb-8a87-01b46382ab14\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{},\"Url\":{\"Base\":null,\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":null},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[{\"id\":\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"44444444444444444444444444444444444?\",\"Queries\":[{\"Key\":\"3333\",\"Value\":\"232424\",\"Enabled\":true,\"Private\":false,\"Type\":0}]},\"Auth\":{\"AuthType\":2,\"AuthProperties\":{\"BasicUsername\":\"asdf\",\"BasicPassword\":\"asdf\",\"DigestUsername\":\"asdf\",\"DigestPassword\":\"asdf\",\"BearerToken\":\"asdf\",\"OAuth2GrantType\":\"authorization_code\",\"OAuth2AccessTokenUrl\":\"asf\",\"OAuth2ClientSecret\":\"asdf\",\"OAuth1ConsumerSecret\":\"asdf\",\"OAuth1ConsumerKey\":\"asdf\"}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":\"{salfaslkdfj}\",\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"234234\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null}],\"Headers\":[],\"ChainingRules\":[],\"Type\":2,\"Name\":\"Untitled\",\"IsExpanded\":true,\"Method\":\"GET\",\"Response\":null}],\"HistoryItems\":[],\"Environments\":[{\"id\":null,\"parentId\":null,\"Name\":\"Base\",\"EnvironmentType\":1,\"Variables\":[{\"Key\":\"env\",\"Value\":\"value\",\"Enabled\":true,\"Private\":false,\"Type\":3}]}],\"Cookies\":null}");

            var wFork1 = JsonConvert.DeserializeObject<Workspace>("{\"id\":\"d89c9279-1e45-41a7-9330-c35429509856\",\"parentId\":\"daniel\",\"Name\":\"magic\",\"Methods\":[\"GET\",\"POST\",\"PUT\",\"DELETE\",\"HEAD\",\"OPTIONS\",\"PATCH\",\"MERGE\",\"COPY\"],\"OpenItemIds\":[\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\"],\"TempItems\":[],\"Items\":[{\"id\":\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":1},\"Url\":{\"Base\":\"\",\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":{}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"My request\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null},{\"id\":\"fa796649-43a2-4dfb-8a87-01b46382ab14\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{},\"Url\":{\"Base\":null,\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":null},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[{\"id\":\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"44444444444444444444444444444444444?\",\"Queries\":[{\"Key\":\"123456\",\"Value\":\"alllllll\",\"Enabled\":false,\"Private\":false,\"Type\":0}]},\"Auth\":{\"AuthType\":2,\"AuthProperties\":{\"BasicUsername\":\"asdf\",\"BasicPassword\":\"asdf\",\"DigestUsername\":\"asdf\",\"DigestPassword\":\"asdf\",\"BearerToken\":\"asdf\",\"OAuth2GrantType\":\"authorization_code\",\"OAuth2AccessTokenUrl\":\"asf\",\"OAuth2ClientSecret\":\"asdf\",\"OAuth1ConsumerSecret\":\"asdf\",\"OAuth1ConsumerKey\":\"asdf\"}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":\"{salfaslkdfj}\",\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"234234\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null}],\"Headers\":[],\"ChainingRules\":[],\"Type\":2,\"Name\":\"Untitled\",\"IsExpanded\":true,\"Method\":\"GET\",\"Response\":null}],\"HistoryItems\":[],\"Environments\":[{\"id\":null,\"parentId\":null,\"Name\":\"Base\",\"EnvironmentType\":1,\"Variables\":[{\"Key\":\"env\",\"Value\":\"value\",\"Enabled\":true,\"Private\":false,\"Type\":3}]}],\"Cookies\":null}");

            var expectedMergeResult = wFork1;

            var mergeResult = wBase.Merge(wFork1, wFork2);
            var resultDiff = expectedMergeResult.Diff(mergeResult);
            Assert.Null(resultDiff);
        }
    }
}
