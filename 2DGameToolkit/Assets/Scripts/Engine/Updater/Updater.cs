using System.Collections.Generic;
using UnityEngine.Assertions;

using AnyObject = System.Object;

public enum EUpdatePass
{
    Invalid,
    First,
    BeforeAI,
    AI,
    AfterAI,
    Last,
    // Do not move count !!!
    Count
}

public class Updater : IUpdater
{
    List<ObjectToUpdate>[] m_ObjectListPerPass = new List<ObjectToUpdate>[(int)EUpdatePass.Count];
    private EUpdatePass m_UpdateGuardPass = EUpdatePass.Invalid;
    private bool m_IsPaused = false;
    private int m_PauseLock = 0;

    struct ObjectToUpdate
    {
        public readonly AnyObject m_ObjectToUpdate;
        public readonly bool m_IsPausable;

        public ObjectToUpdate(AnyObject objectToUpdate, bool isPausable = true)
        {
            this.m_ObjectToUpdate = objectToUpdate;
            this.m_IsPausable = isPausable;
        }
        public override bool Equals(object r)
        {
            return this.m_ObjectToUpdate == ((ObjectToUpdate)r).m_ObjectToUpdate;
        }
    }

    public Updater()
    {
        for (int i = 0; i < (int)EUpdatePass.Count; i++)
        {
            m_ObjectListPerPass[i] = new List<ObjectToUpdate> ();
        }
    }

    public void Update ()
    {
        for (int i = 1; i < (int)EUpdatePass.Count; i++)
        {
            EUpdatePass pass = (EUpdatePass)i;
            m_UpdateGuardPass = pass;
            foreach (ObjectToUpdate objectToUpdate in m_ObjectListPerPass[(int)pass])
            {
                if (!m_IsPaused || !objectToUpdate.m_IsPausable)
                {
                    ReflectionHelper.CallMethod("Update" + pass.ToString(), objectToUpdate.m_ObjectToUpdate);
                }
            }
            m_UpdateGuardPass = EUpdatePass.Invalid;
        }
    }

    public void Register (AnyObject objectToUpdate, bool isPausable, params EUpdatePass[] updatePassList)
    {
        ObjectToUpdate newEntry = new ObjectToUpdate(objectToUpdate, isPausable);
        foreach (EUpdatePass pass in updatePassList)
        {
            Assert.IsFalse(m_UpdateGuardPass == pass, "Cannot register an object to update while updating !");
            Assert.IsTrue (pass != EUpdatePass.Count, "Invalid Update Pass : " + pass.ToString ());
            if (!m_ObjectListPerPass[(int)pass].Contains (newEntry))
            {
                m_ObjectListPerPass[(int)pass].Add (newEntry);
            }
        }
    }

    public void Unregister (AnyObject objectToUpdate, params EUpdatePass[] updatePassList)
    {
        ObjectToUpdate entryToRemove = new ObjectToUpdate(objectToUpdate);
        foreach (EUpdatePass pass in updatePassList)
        {
            Assert.IsFalse(m_UpdateGuardPass == pass, "Cannot register an object to update while updating !");
            Assert.IsTrue (pass != EUpdatePass.Count, "Invalid Update Pass : " + pass.ToString ());
            m_ObjectListPerPass[(int)pass].Remove (entryToRemove);
        }
    }

    public void SetPause (bool pause)
    {
        m_PauseLock += pause ? 1 : -1;
        m_IsPaused = m_PauseLock > 0;
    }

    public bool IsPaused ()
    {
        return m_IsPaused;
    }
}