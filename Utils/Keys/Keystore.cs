using System;
using System.Security.Cryptography;
using Newtonsoft.Json;

public class Keystore
{
    public static byte[] EncryptKey(Key key, string auth, int scryptN, int scryptP)
    {
        byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key.PrivateKey);
        byte[] salt = GenerateRandomBytes(16); // Generate a random salt
        byte[] iv = GenerateRandomBytes(16); // Generate a random IV

        byte[] derivedKey = DeriveKey(auth, salt, scryptN, scryptP);

        using (Aes aes = Aes.Create())
        {
            aes.Key = derivedKey;
            aes.IV = iv;

            byte[] ciphertext = EncryptData(aes, keyBytes);

            EncryptedKey keyJSON = new EncryptedKey
            {
                Address = key.Address,
                Crypto = new CryptoJSON
                {
                    Ciphertext = Convert.ToBase64String(ciphertext),
                    CipherParams = new CipherParamsJSON
                    {
                        Iv = Convert.ToBase64String(iv)
                    },
                    Kdf = "PBKDF2", // Replace with the desired KDF name
                    KdfParams = new KdfParamsJSON
                    {
                        DkLen = derivedKey.Length,
                        Salt = Convert.ToBase64String(salt),
                        N = scryptN,
                        P = scryptP,
                        R = 1 // Replace with the desired r value
                    },
                    Mac = Convert.ToBase64String(ComputeHMAC(derivedKey, ciphertext))
                }
            };

            string json = JsonConvert.SerializeObject(keyJSON);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }
    }

    public static string DecryptKey(byte[] keyJson, string auth)
    {
        EncryptedKey k = JsonConvert.DeserializeObject<EncryptedKey>(System.Text.Encoding.UTF8.GetString(keyJson));

        byte[] salt = Convert.FromBase64String(k.Crypto.KdfParams.Salt);
        int scryptN = k.Crypto.KdfParams.N;
        int scryptP = k.Crypto.KdfParams.P;

        byte[] derivedKey = DeriveKey(auth, salt, scryptN, scryptP);

        byte[] ciphertext = Convert.FromBase64String(k.Crypto.Ciphertext);

        byte[] hmac = Convert.FromBase64String(k.Crypto.Mac);
        byte[] computedHmac = ComputeHMAC(derivedKey, ciphertext);

        bool isMacValid = ConstantTimeEquals(hmac, computedHmac);

        if (!isMacValid)
        {
            throw new InvalidOperationException("Invalid MAC");
        }

        byte[] iv = Convert.FromBase64String(k.Crypto.CipherParams.Iv);

        using (Aes aes = Aes.Create())
        {
            aes.Key = derivedKey;
            aes.IV = iv;

            byte[] decryptedBytes = DecryptData(aes, ciphertext);
            return System.Text.Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        byte[] randomBytes = new byte[length];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return randomBytes;
    }

    private static byte[] DeriveKey(string password, byte[] salt, int scryptN, int scryptP)
    {
        using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, scryptN, HashAlgorithmName.SHA256))
        {
            rfc2898DeriveBytes.IterationCount = scryptP;
            return rfc2898DeriveBytes.GetBytes(32); // Derived key length of 32 bytes (256 bits)
        }
    }

    private static byte[] EncryptData(Aes aes, byte[] data)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
            }
            return ms.ToArray();
        }
    }

    private static byte[] DecryptData(Aes aes, byte[] encryptedData)
    {
        using (MemoryStream ms = new MemoryStream(encryptedData))
        {
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                using (MemoryStream decryptedMs = new MemoryStream())
                {
                    while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        decryptedMs.Write(buffer, 0, bytesRead);
                    }
                    return decryptedMs.ToArray();
                }
            }
        }
    }

    private static byte[] ComputeHMAC(byte[] key, byte[] data)
    {
        using (HMACSHA256 hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(data);
        }
    }

    private static bool ConstantTimeEquals(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
        {
            return false;
        }

        int result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result |= a[i] ^ b[i];
        }

        return result == 0;
    }
}
