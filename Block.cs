using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Id { get; set; }

    public string PreviousHash { get; private set; } = "0";

    public long Timestamp { get; set; }

    public string StoredData { get; set; }

    public int Nonce { get; set; }

    public string Hash { get; set; }

    public Block(int index, long timestamp, string data, int nonce = 0)
    {
        Id = index;
        Timestamp = timestamp;
        StoredData = data;
        Nonce = nonce;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        SHA256 sha256 = SHA256.Create();
        string rawData = $"{Id}{PreviousHash}{Timestamp}{StoredData}{Nonce}";
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }

    public void SetPreviousHash(string previousHash)
    {
        PreviousHash = previousHash;
    }

    public string GetInfo()
    {
        return $"Block #{this.Id} \nBlock Hash: {this.Hash} \nPrevious Hash: {this.PreviousHash} \nData: {this.StoredData}\n";
    }
}
