
using UnityEngine;

public interface IPlayerManager
{
    PlayerStat GetPlayerStat();
    GameObject GetPlayer();
}

public class PlayerManagerProxy : UniqueProxy<IPlayerManager>
{ }