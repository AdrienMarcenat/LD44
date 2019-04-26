using UnityEngine;
using UnityEditor;
using System;

public class TestNodeEditor : NodeBasedEditor<TestNodeEditor, Node, Connection>
    , INodeEditor<Node, Connection>
{
    [MenuItem("Window/Node Based Editor test")]
    private static void OpenWindow()
    {
        TestNodeEditor window = GetWindow<TestNodeEditor>();
        window.titleContent = new GUIContent("Node Based Editor test");
    }

    public virtual Node CreateNode(Vector2 position
        , GUIStyle nodeStyle
        , GUIStyle selectedStyle
        , GUIStyle inPointStyle
        , GUIStyle outPointStyle
        , Action<ConnectionPoint> OnClickInPoint
        , Action<ConnectionPoint> OnClickOutPoint
        , Action<Node> OnClickRemoveNode
        , string inPointID
        , string outPointID)
    {
        return new Node(position, nodeStyle, selectedStyle, inPointStyle, outPointStyle,
            OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPointID, outPointID, false);
    }
    public virtual Node DeserializeNode(Node nodeDeserialized)
    {
        return new Node(nodeDeserialized.m_Rect.position,
                m_NodeStyle,
                m_SelectedNodeStyle,
                m_InPointStyle,
                m_OutPointStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode,
                nodeDeserialized.m_InPoint.m_Id,
                nodeDeserialized.m_OutPoint.m_Id,
                false,
                nodeDeserialized.m_ID
                );
    }

    public virtual Connection CreateConnection(Connection connection)
    {
        return connection;
    }
}