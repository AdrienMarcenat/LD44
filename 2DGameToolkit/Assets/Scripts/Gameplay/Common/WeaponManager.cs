using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class WeaponManager : MonoBehaviour
{
    private Dictionary<string, Weapon> m_Weapons = new Dictionary<string, Weapon>();

    void Start ()
    {
        foreach (Weapon w in GetComponentsInChildren<Weapon> ())
        {
            m_Weapons.Add (w.GetWeaponType (), w);
        }
    }

    public void Fire(int weaponIndex, Vector3 target, float sizeModifier = 1f)
    {
        int i = 0;
        foreach (Weapon weapon in m_Weapons.Values)
        {
            if (i == weaponIndex)
            {
                weapon.Fire(target, sizeModifier);
                return;
            }
            i++;
        }
        Assert.IsTrue(false, "Wrong weaponIndex " + weaponIndex);
    }

    public void Fire (string weaponType, Vector3 target, float sizeModifier = 1f)
    {
        if (m_Weapons.ContainsKey (weaponType))
        {
            m_Weapons[weaponType].Fire (target, sizeModifier);
        }
        else
        {
            Assert.IsTrue (false, "Wrong weapon Type");
        }
    }

    public void AddFireCommand(int weaponIndex, float numberOfShots, float sizeModifier, Vector3 target)
    {
        int i = 0;
        foreach (Weapon weapon in m_Weapons.Values)
        {
            if (i == weaponIndex)
            {
                weapon.AddFireCommand(numberOfShots, sizeModifier, target);
                return;
            }
            i++;
        }
        Assert.IsTrue(false, "Wrong weaponIndex " + weaponIndex);
    }

    public void AddFireCommand (string weaponType, float numberOfShots, float sizeModifier, Vector3 target)
    {
        if (m_Weapons.ContainsKey (weaponType))
        {
            m_Weapons[weaponType].AddFireCommand (numberOfShots, sizeModifier, target);
        }
        else
        {
            Assert.IsTrue (false, "Wrong weapon Type");
        }
    }
}

