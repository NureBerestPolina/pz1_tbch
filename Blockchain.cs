public class Blockchain
{
    public int Difficulty { get; private set; } = 4;

    public List<Block> List;

    public Blockchain()
    {
        List = new List<Block>() 
        { 
            CreateGenesisBlock() 
        };
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, DateTimeOffset.UtcNow.ToUnixTimeSeconds(), "The first (genesis) Block");
    }

    public Block GetLastBlock()
    {
        return List[^1];
    }

    public void AddBlock(Block block)
    {
        block.SetPreviousHash(GetLastBlock().Hash);
        block.Hash = ProofOfWork(block);
        List.Add(block);
    }

    private string ProofOfWork(Block block)
    {
        string aimedStart = new string('0', Difficulty);

        while (!block.Hash.StartsWith(aimedStart))
        {
            block.Nonce++;
            block.Hash = block.CalculateHash();
        }

        return block.Hash;
    }

    public bool IsChainValid()
    {
        for (int i = 1; i < List.Count; i++) 
        {
            Block currentBlock = List[i];
            Block previousBlock = List[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
            {
                Console.WriteLine($"Block #{currentBlock.Id} has incorrect hash\n\n");
                return false;
            }

            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                Console.WriteLine($"Block #{currentBlock.Id} is not linked correctly with the previous block\n\n");
                return false;
            }
        }

        Console.WriteLine("Blockchain is valid\n\n");
        return true;
    }
}
