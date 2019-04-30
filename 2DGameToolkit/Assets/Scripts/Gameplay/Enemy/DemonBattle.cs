using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBattle : MonoBehaviour
{
    [SerializeField] GameObject m_DemonPrefab;
    [SerializeField] List<Transform> m_Nodes;
    [SerializeField] AudioClip m_BossMusic;

    private void OnEnable()
    {
        this.RegisterAsListener("Game", typeof(DemonFightGameEvent));
    }

    private void OnDisable()
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
        GameObject demon = Instantiate(m_DemonPrefab, transform);
        demon.GetComponent<Demon>().SetNodes(m_Nodes);
    }
}
