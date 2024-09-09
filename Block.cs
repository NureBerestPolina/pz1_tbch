using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Id { get; set; }
    public string PreviousHash { get; private set; } = "0";
    public long Timestamp { get; set; }
    public Dictionary<string, string> Transactions { get; set; }
    public int Nonce { get; set; }
    public string Hash { get; set; }

    public Block(int id, long timestamp, Dictionary<string, string> transactions, int nonce = 0)
    {
        Id = id;
        Timestamp = timestamp;
        Transactions = transactions;
        Nonce = nonce;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        SHA256 sha256 = SHA256.Create();
        string rawData = $"{Id}{PreviousHash}{Timestamp}{TransactionsAsString()}{Nonce}";
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }

    public void SetPreviousHash(string previousHash)
    {
        PreviousHash = previousHash;
    }

    public string GetInfo()
    {
        return $"Block #{this.Id} \nBlock Hash: {this.Hash} \nPrevious Hash: {this.PreviousHash} \nTransactions: {TransactionsAsString()}\n";
    }

    // Метод для преобразования словаря транзакций в строку
    public string TransactionsAsString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var transaction in Transactions)
        {
            sb.Append($"{transaction.Key}: {transaction.Value}, ");
        }
        return sb.ToString().TrimEnd(',', ' ');
    }
}