using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Demon : Enemy
{
    [SerializeField] float m_DecisionTime = 3f;
    [SerializeField] int m_WrathBulletNumber = 10;
    [SerializeField] List<GameObject> m_EnemiesToInvoke;

    private List<Transform> m_Nodes;
    private float m_DecisionTimer = 0;
    private int m_CurrentNodeIndex = 1;
    private WeaponManager m_WeaponManager;
    private Transform m_Target;

    private new void Awake()
    {
        base.Awake();
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        m_WeaponManager = GetComponent<WeaponManager>();
    }

    protected override void OnGameOver(bool real)
    {
        base.OnGameOver(real);
        if (real)
        {
            m_Target.GetComponent<Collider2D>().enabled = false;
            Dialogue.DialogueManagerProxy.Get().TriggerDialogue("EndGame");
        }
    }

    public void SetNodes(List<Transform> nodes)
    {
        m_Nodes = nodes;
        transform.position = m_Nodes[m_CurrentNodeIndex].position;
    }

    private void Update()
    {
        m_DecisionTimer += Time.deltaTime;
        if(m_DecisionTimer > m_DecisionTime)
        {
            m_DecisionTimer = 0;
            Action();
        }
    }

    private void Action()
    {
        StopAllCoroutines();
        int i = Random.Range(0, 8);
        switch(i)
        {
            case 0:
            case 1:
            case 2:
                StartCoroutine(FireRoutine());
                break;
            case 3:
                StartCoroutine(InvokeRoutine());
                break;
            case 4:
            case 5:
                StartCoroutine(TeleportRoutine());
                break;
            case 6:
            case 7:
                StartCoroutine(WrathRoutine());
                break;
        }
    }

    private IEnumerator FireRoutine()
    {
        m_Animator.SetTrigger("fire");
        yield return new WaitForSeconds(1);
        int direction = m_Target.position.x - transform.position.x < 0 ? -1 : 1;
        Vector3 orientation = direction < 0 ? Vector3.left : Vector3.right;
        for (int i = 0; i < 20; i++)
        {
            m_WeaponManager.AddFireCommand("DemonFire", 1, 1, orientation);
            orientation = Quaternion.AngleAxis(5 * direction, new Vector3(0, 0, -1)) * orientation;
        }
    }

    private IEnumerator WrathRoutine()
    {
        m_Animator.SetTrigger("fire");
        yield return new WaitForSeconds(1);
        Vector3 orientation = Vector3.left;
        for (int i = 0; i < m_WrathBulletNumber; i++)
        {
            int direction = Random.Range(0, 36);
            m_WeaponManager.AddFireCommand("DemonWrath", 1, 2, orientation);
            orientation = Quaternion.AngleAxis(10 * direction, new Vector3(0, 0, -1)) * orientation;
        }
    }

    private IEnumerator InvokeRoutine()
    {
        m_Animator.SetTrigger("invoke");
        yield return new WaitForSeconds(1);
        for (int i = 1; i < m_Nodes.Count; i++)
        {
            int j = Random.Range(0, m_EnemiesToInvoke.Count);
            GameObject enemy = Instantiate(m_EnemiesToInvoke[j]);
            int nodeIndex = (m_CurrentNodeIndex + i) % m_Nodes.Count;
            enemy.transform.position = m_Nodes[nodeIndex].position;
            int yOffset = Random.Range(0,3);
            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + yOffset, enemy.transform.position.z);
            EnemySimplePath enemyPath = enemy.GetComponentInChildren<EnemySimplePath>();
            if(enemyPath != null)
            {
                enemyPath.SetLeftNode(m_Nodes[0]);
                enemyPath.SetRitghNode(m_Nodes[m_Nodes.Count-1]);
            }
        }
    }

    private IEnumerator TeleportRoutine()
    {
        m_Animator.SetTrigger("teleport");
        yield return new WaitForSeconds(1);
        int i = Random.Range(0, 3);
        if(i == m_CurrentNodeIndex)
        {
            i = (i + 1) % m_Nodes.Count;
        }
        m_CurrentNodeIndex = i;
        transform.position = m_Nodes[m_CurrentNodeIndex].position;
    }
}
