Blockchain blockchain = new Blockchain();

Console.WriteLine("Testing blocks adding functionality:");
for (int i = 1; i < 4; i++)
{
    string data = $"Some Important Data {i}";
    Block block = new Block(i, DateTimeOffset.UtcNow.ToUnixTimeSeconds(), data);
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
string modifiedData = "Modified Strange Data 1";
blockchain.List[1].StoredData = modifiedData;
blockchain.List[1].Hash = blockchain.List[1].CalculateHash();

foreach (Block block in blockchain.List)
{
    Console.WriteLine(block.GetInfo());
}
blockchain.IsChainValid();
