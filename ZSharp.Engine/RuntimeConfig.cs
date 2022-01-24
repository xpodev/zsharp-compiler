using Newtonsoft.Json;

namespace ZSharp.Engine
{
    internal class RuntimeConfig
    {
        public class Framework
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("version")]
            public string Version { get; set; }
        }

        public class RuntimeOptions
        {
            [JsonProperty("tfm")]
            public string TargetFramework { get; set; }

            [JsonProperty("framework")]
            public Framework Framework { get; set; }
        }

        public readonly RuntimeOptions runtimeOptions = new();
    }
}
