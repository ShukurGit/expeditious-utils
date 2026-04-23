
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Dynamic;


namespace Expeditious.Common
{
    public static class JsonHelper
    {
        public enum JsonEngine { Newtonsoft, Microsoft };


        static public dynamic DynamicDeserializeText(string jsonContent, JsonEngine jsonEngine = JsonEngine.Newtonsoft)
        {
            dynamic dynamicJson = null;

            if (!string.IsNullOrWhiteSpace(jsonContent))
            {
                if (jsonEngine == JsonEngine.Newtonsoft)
                    dynamicJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonContent);
                else if (jsonEngine == JsonEngine.Microsoft)
                    dynamicJson = System.Text.Json.JsonSerializer.Deserialize<ExpandoObject>(jsonContent);
            }

            return dynamicJson;
        }


        static public dynamic DynamicDeserializeFile(string jsonFile, JsonEngine jsonEngine = JsonEngine.Newtonsoft)
        {
            string jsonContent = File.ReadAllText(jsonFile);
            return DynamicDeserializeText(jsonContent, jsonEngine);
        }


        static public T DeserializeFile<T>(string jsonFilePath, JsonEngine jsonEngine = JsonEngine.Newtonsoft)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            return Deserialize<T>(jsonContent, jsonEngine);
        }


        static public T Deserialize<T>(string jsonContent, JsonEngine jsonEngine = JsonEngine.Newtonsoft)
        {
            T result;

            // if (String.IsNullOrWhiteSpace(jsonContent))

            if (jsonEngine == JsonEngine.Newtonsoft)
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonContent);
            else // if (jsonEngine == JsonEngine.Microsoft)
                result = System.Text.Json.JsonSerializer.Deserialize<T>(jsonContent);

            return result;
        }


        static public void SerializeFile<T>(T obj, string outJsonFilePath, JsonEngine jsonEngine = JsonEngine.Newtonsoft)
        {
            string jsonContent = Serialize(obj, jsonEngine);
            File.WriteAllText(outJsonFilePath, jsonContent);
        }


        static public string Serialize<T>(T obj, JsonEngine jsonEngine = JsonEngine.Newtonsoft)
        {
            string result = null;

            if (jsonEngine == JsonEngine.Newtonsoft)
                result = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented /*, new JsonSerializerSettings { ContractResolver = new  CamelCasePropertyNamesContractResolver() }*/ );
            else if (jsonEngine == JsonEngine.Microsoft)
                result = System.Text.Json.JsonSerializer.Serialize(obj);

            return result;
        }


        // new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
        // new JsonSerializerSettings { ContractResolver = new OrderedContractResolver() }
        public class OrderedContractResolver : DefaultContractResolver
        {
            protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return base.CreateProperties(type, memberSerialization).OrderBy(p => p.PropertyName).ToList();
            }
        }

        static public dynamic ReadJsonToDynamic(String filePath)
        {
            String jsonContent = File.ReadAllText(filePath);

            //var dynamicObject = Json.Decode(jsonString);
            // var jRoot = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(resolvedEvent.Event.Data));
            // dynamic data = JsonConvert.DeserializeObject<dynamic>(json);


            dynamic dynamicJson = System.Text.Json.JsonSerializer.Deserialize<System.Dynamic.ExpandoObject>(jsonContent)!;

            var jRoot = JsonConvert.DeserializeObject<dynamic>(jsonContent);

            return dynamicJson;
        }


        //static public dynamic ReadJsonFileToDynamicMs



        //static public dynamic ParseJsonToDynamicMs(String jsonContent)
        //{
        //    if (String.IsNullOrWhiteSpace(jsonContent)) return null;

        //    dynamic dynamicJson = System.Text.Json.JsonSerializer.Deserialize<System.Dynamic.ExpandoObject>(jsonContent);

        //    return dynamicJson;
        //}


        static public dynamic ParseJsonToDynamicNewtonsoft(String jsonContent)
        {
            if (String.IsNullOrWhiteSpace(jsonContent)) return null;

            dynamic dynamicJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonContent)!;

            return dynamicJson;
        }



    }
}
