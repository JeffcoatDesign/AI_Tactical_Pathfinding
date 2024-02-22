public class NodeRecord
{
    public Node node;
    public Connection connection;
    public float costSoFar;

    public int CompareTo(NodeRecord other)
    {
        if (other == null) return 1;
        return (int)(costSoFar - other.costSoFar);
    }
}
