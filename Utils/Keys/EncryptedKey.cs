using Newtonsoft.Json;

public class EncryptedKey
{
    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("crypto")]
    public CryptoJSON Crypto { get; set; }
}