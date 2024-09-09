Blockchain blockchain = new Blockchain();

Console.WriteLine("Testing blocks adding functionality:");
for (int i = 1; i < 4; i++)
{
    Dictionary<string, string> transactions = new Dictionary<string, string>
    {
        { $"Tx{i}1", $"Sender {i} -> Receiver {i}, Amount {i * 10}" },
        { $"Tx{i}2", $"Sender {i} -> Receiver {i+1}, Amount {i * 15}" }
    };
    Block block = new Block(i, DateTimeOffset.UtcNow.ToUnixTimeSeconds(), transactions);
    blockchain.AddBlock(block);
    Console.WriteLine($"Block #{i} was successfully added to the blockchain \nBlock Hash: {block.Hash}\n");
}

Console.WriteLine("Demo-test of chain integrity:");
foreach (Block block in blockchain.List)
{
    Console.WriteLine(block.GetInfo());
}
blockchain.IsChainValid();

Console.WriteLine("Demo of blockchain security:");
Dictionary<string, string> modifiedTransactions = new Dictionary<string, string>
{
    { "Tx1", "Modified Strange Data" }
};
blockchain.List[1].Transactions = modifiedTransactions;
blockchain.List[1].Hash = blockchain.List[1].CalculateHash();

foreach (Block block in blockchain.List)
{
    Console.WriteLine(block.GetInfo());
}
blockchain.IsChainValid();