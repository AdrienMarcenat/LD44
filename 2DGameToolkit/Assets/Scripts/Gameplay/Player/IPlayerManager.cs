using UnityEngine;

public interface IPlayerStatWatcher
{
    void OnStatChanged(PlayerStat stat);
}

public interface IPlayerManager
{
    PlayerStat GetPlayerStat();
    GameObject GetPlayer();
    void RegiterStatChangeCallback(IPlayerStatWatcher cb);
    void UnregiterStatChangeCallback(IPlayerStatWatcher cb);
}

public class PlayerManagerProxy : UniqueProxy<IPlayerManager>
{ }