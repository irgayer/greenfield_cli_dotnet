using Newtonsoft.Json;

public class CipherParamsJSON
{
    [JsonProperty("iv")]
    public string Iv { get; set; }
}