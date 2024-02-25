public class Connection
{
    float cost;
    float influence;
    Node fromNode;
    Node toNode;
    internal float GetCost() => cost;
    internal float GetInfluence() => influence;
    internal Node GetFromNode() => fromNode;
    internal Node GetToNode() => toNode;
    public Connection (float cost, Node fromNode, Node toNode)
    {
        this.cost = cost;
        this.fromNode = fromNode;
        this.toNode = toNode;

        if (fromNode.type == NodeType.SpikeTrap)
            influence = 5.0f;
        else if (fromNode.type == NodeType.FireTrap)
            influence = 7.0f;
        else
            influence = 0f;
    }
}
