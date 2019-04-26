using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Dialogue
{
    public class Dialogue
    {
        public List<Node> m_Nodes = new List<Node>();
        public string m_RootNodeID;

        public Dialogue()
        { }

        public void AddNode(Node node)
        {
            m_Nodes.Add(node);
        }

        public Node GetNode(string nodeID)
        {
            Node node = null;
            foreach (Node n in m_Nodes)
            {
                if (n.m_ID == nodeID)
                {
                    node = n;
                }
            }
            Assert.IsTrue(node != null, "Cannot find node with ID " + nodeID);
            return node;
        }

        public string GetRootNodeID()
        {
            return m_RootNodeID;
        }
    }
}

