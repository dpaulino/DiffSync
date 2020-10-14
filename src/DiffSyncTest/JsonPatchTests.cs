using JeniusApps.Nightingale.Data.Models;
using JsonDiffPatchDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace DiffSyncTest
{
    public class JsonPatchTests
    {
        [Fact]
        public void SimplePatchTest()
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

            var jdp = new JsonDiffPatch();
            var left = JToken.FromObject(a1);
            var right = JToken.FromObject(a2);
            JToken patch = jdp.Diff(left, right);

            var output = jdp.Patch(left, patch);

            Assert.Equal(output.ToString(), right.ToString());
        }

        [Fact]
        public void ComplexPatchTest()
        {
            var obj1 = JsonConvert.DeserializeObject<Workspace>("{\"id\":\"d89c9279-1e45-41a7-9330-c35429509856\",\"parentId\":\"root\",\"Name\":\"magic\",\"Methods\":[\"GET\",\"POST\",\"PUT\",\"DELETE\",\"HEAD\",\"OPTIONS\",\"PATCH\",\"MERGE\",\"COPY\"],\"OpenItemIds\":[\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\"],\"TempItems\":[],\"Items\":[{\"id\":\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"\",\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":{}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"My request\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null},{\"id\":\"fa796649-43a2-4dfb-8a87-01b46382ab14\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{},\"Url\":{\"Base\":null,\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":null},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[{\"id\":\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"44444444444444444444444444444444444?\",\"Queries\":[{\"Key\":\"3333\",\"Value\":\"232424\",\"Enabled\":true,\"Private\":false,\"Type\":0}]},\"Auth\":{\"AuthType\":2,\"AuthProperties\":{\"BasicUsername\":\"asdf\",\"BasicPassword\":\"asdf\",\"DigestUsername\":\"asdf\",\"DigestPassword\":\"asdf\",\"BearerToken\":\"asdf\",\"OAuth2GrantType\":\"authorization_code\",\"OAuth2AccessTokenUrl\":\"asf\",\"OAuth2ClientSecret\":\"asdf\",\"OAuth1ConsumerSecret\":\"asdf\",\"OAuth1ConsumerKey\":\"asdf\"}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":\"{salfaslkdfj}\",\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"234234\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null}],\"Headers\":[],\"ChainingRules\":[],\"Type\":2,\"Name\":\"Untitled\",\"IsExpanded\":true,\"Method\":\"GET\",\"Response\":null}],\"HistoryItems\":[],\"Environments\":[{\"id\":null,\"parentId\":null,\"Name\":\"Base\",\"EnvironmentType\":1,\"Variables\":[{\"Key\":\"env\",\"Value\":\"value\",\"Enabled\":true,\"Private\":false,\"Type\":3}]}],\"Cookies\":null}");
            var obj2 = JsonConvert.DeserializeObject<Workspace>("{\"id\":\"d89c9279-1e45-41a7-9330-c35429509856\",\"parentId\":\"daniel\",\"Name\":\"magic\",\"Methods\":[\"GET\",\"POST\",\"PUT\",\"DELETE\",\"HEAD\",\"OPTIONS\",\"PATCH\",\"MERGE\",\"COPY\"],\"OpenItemIds\":[\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\"],\"TempItems\":[],\"Items\":[{\"id\":\"7c4a8648-24e1-48e0-b2f9-2e30ab8f2b36\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":1},\"Url\":{\"Base\":\"\",\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":{}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"My request\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null},{\"id\":\"fa796649-43a2-4dfb-8a87-01b46382ab14\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{},\"Url\":{\"Base\":null,\"Queries\":[]},\"Auth\":{\"AuthType\":0,\"AuthProperties\":null},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":null,\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[{\"id\":\"62b0aa85-9d5c-4fc2-b4cd-d2e80558e08e\",\"parentId\":null,\"IsTemporary\":false,\"Properties\":{\"RequestPivotIndex\":0},\"Url\":{\"Base\":\"44444444444444444444444444444444444?\",\"Queries\":[{\"Key\":\"123456\",\"Value\":\"alllllll\",\"Enabled\":false,\"Private\":false,\"Type\":0}]},\"Auth\":{\"AuthType\":2,\"AuthProperties\":{\"BasicUsername\":\"asdf\",\"BasicPassword\":\"asdf\",\"DigestUsername\":\"asdf\",\"DigestPassword\":\"asdf\",\"BearerToken\":\"asdf\",\"OAuth2GrantType\":\"authorization_code\",\"OAuth2AccessTokenUrl\":\"asf\",\"OAuth2ClientSecret\":\"asdf\",\"OAuth1ConsumerSecret\":\"asdf\",\"OAuth1ConsumerKey\":\"asdf\"}},\"Body\":{\"BodyType\":0,\"JsonBody\":null,\"XmlBody\":null,\"TextBody\":null,\"FormEncodedData\":[],\"FormDataList\":[],\"BinaryFilePath\":null},\"MockData\":{\"Body\":\"{salfaslkdfj}\",\"StatusCode\":200,\"ContentType\":\"application/json\"},\"Children\":[],\"Headers\":[],\"ChainingRules\":[],\"Type\":1,\"Name\":\"234234\",\"IsExpanded\":false,\"Method\":\"GET\",\"Response\":null}],\"Headers\":[],\"ChainingRules\":[],\"Type\":2,\"Name\":\"Untitled\",\"IsExpanded\":true,\"Method\":\"GET\",\"Response\":null}],\"HistoryItems\":[],\"Environments\":[{\"id\":null,\"parentId\":null,\"Name\":\"Base\",\"EnvironmentType\":1,\"Variables\":[{\"Key\":\"env\",\"Value\":\"value\",\"Enabled\":true,\"Private\":false,\"Type\":3}]}],\"Cookies\":null}");

            var jdp = new JsonDiffPatch();
            var left = JToken.FromObject(obj1);
            var right = JToken.FromObject(obj2);
            JToken patch = jdp.Diff(left, right);
            var output = jdp.Patch(left, patch);
            Assert.Equal(output.ToString(), right.ToString());
        }

    }
}
