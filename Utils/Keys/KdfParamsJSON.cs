using Newtonsoft.Json;

public class KdfParamsJSON
{
    [JsonProperty("dklen")]
    public int DkLen { get; set; }

    [JsonProperty("salt")]
    public string Salt { get; set; }

    [JsonProperty("n")]
    public int N { get; set; }

    [JsonProperty("p")]
    public int P { get; set; }

    [JsonProperty("r")]
    public int R { get; set; }
}