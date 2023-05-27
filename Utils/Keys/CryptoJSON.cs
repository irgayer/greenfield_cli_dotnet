using Newtonsoft.Json;

public class CryptoJSON
{
    [JsonProperty("ciphertext")]
    public string Ciphertext { get; set; }

    [JsonProperty("cipherparams")]
    public CipherParamsJSON CipherParams { get; set; }

    [JsonProperty("kdf")]
    public string Kdf { get; set; }

    [JsonProperty("kdfparams")]
    public KdfParamsJSON KdfParams { get; set; }

    [JsonProperty("mac")]
    public string Mac { get; set; }
}