using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBattle : MonoBehaviour
{
    [SerializeField] GameObject m_DemonPrefab;
    [SerializeField] List<Transform> m_Nodes;
    [SerializeField] AudioClip m_BossMusic;

    private void Awake()
    {
        this.RegisterAsListener("Game", typeof(DemonFightGameEvent));
    }

    private void OnDestroy()
    {
        this.UnregisterAsListener("Game");
    }

    public void OnGameEvent(DemonFightGameEvent fightEvent)
    {
        SoundManagerProxy.Get().PlayMusic(m_BossMusic);
        StartCoroutine(DemonSpawnRoutine());
    }

    private IEnumerator DemonSpawnRoutine()
    {
        yield return new WaitForSeconds(2);
        Instantiate(m_DemonPrefab, transform);
        m_DemonPrefab.GetComponent<Demon>().SetNodes(m_Nodes);
    }
}
