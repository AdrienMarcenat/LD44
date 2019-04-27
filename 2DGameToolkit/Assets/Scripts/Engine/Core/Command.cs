using UnityEngine;

public class Command
{
    protected GameObject m_Actor;

    public Command(GameObject actor)
    {
        m_Actor = actor;
    }

    public virtual void Execute() { }
    public virtual void Undo() { }
};