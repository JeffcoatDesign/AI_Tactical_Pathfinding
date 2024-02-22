using System.Collections.Generic;

public class PathfindingList {
    List<NodeRecord> nodeRecords = new List<NodeRecord>();

    public int Length { get {  return nodeRecords.Count; } }

    public NodeRecord smallestElement
    {
        get {
            float smallestValue = float.MaxValue;
            NodeRecord smallest = null;
            foreach (NodeRecord record in nodeRecords)
            {
                if (record.costSoFar < smallestValue)
                {
                    smallestValue = record.costSoFar;
                    smallest = record;
                }
            }
            return smallest;
        }
    }

    public void Add(NodeRecord record) { 
        nodeRecords.Add(record);
    }

    public void Remove(NodeRecord record) { 
        nodeRecords.Remove(record);
    }

    public bool Contains (Node node)
    {
        bool found = false;
        foreach (NodeRecord nodeRecord in nodeRecords)
        {
            if (nodeRecord.node == node)
            {
                found = true;
                break;
            }
        }
        return found;
    }

    public NodeRecord Find (Node node) {
        NodeRecord record = null;
        foreach (NodeRecord nodeRecord in nodeRecords)
        {
            if (nodeRecord.node == node)
            {
                record = nodeRecord;
                break;
            }
        }
        return record;
    }
}