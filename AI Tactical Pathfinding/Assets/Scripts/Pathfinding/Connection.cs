public class Connection
{
    public float cost;
    public Node fromNode;
    public Node toNode;
    internal float GetCost() => cost;
    internal Node GetFromNode() => fromNode;
    internal Node GetToNode() => toNode;
    public Connection (float cost, Node fromNode, Node toNode)
    {
        this.cost = cost;
        this.fromNode = fromNode;
        this.toNode = toNode;
    }
}
