
using AnyObject = System.Object;
public interface IUpdater
{
    void Register(AnyObject objectToUpdate, bool isPausable, params EUpdatePass[] updatePassList);
    void Unregister(AnyObject objectToUpdate, params EUpdatePass[] updatePassList);
    void Update();
    void SetPause(bool pause);
    bool IsPaused();
}

public class UpdaterProxy : UniqueProxy<IUpdater>
{ }