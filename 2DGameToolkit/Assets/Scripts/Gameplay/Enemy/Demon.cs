using UnityEngine;
using System.Collections.Generic;

public class Demon : Enemy
{
    private List<Transform> m_Nodes;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNodes(List<Transform> nodes)
    {
        m_Nodes = nodes;
    }
}
