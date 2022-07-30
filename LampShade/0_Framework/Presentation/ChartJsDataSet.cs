using Newtonsoft.Json;

namespace _0_Framework.Presentation
{
    public class ChartJsDataSet
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<int> Data { get; set; }

        [JsonProperty(PropertyName = "borderColor")]
        public string BorderColor { get; set; }

        [JsonProperty(PropertyName = "backgroundColor")]
        public string[] BackgroundColors { get; set; }
    }
}
