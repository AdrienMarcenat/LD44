using UnityEngine;

public class EnemySeekerIdle : EnemySeeker
{
    public override void Seek(GameObject target)
    {
        base.Seek(target);

        EnemyPlayerAiming enemyAI = gameObject.AddComponent<EnemyPlayerAiming>();
        enemyAI.SetFireParameters(3, 1, 1, 0);
    }
}