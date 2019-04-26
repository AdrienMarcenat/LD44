
namespace _2DGameToolKitTest
{
    class NullUpdater : IUpdater
    {
        bool IUpdater.IsPaused()
        {
            return false;
        }

        void IUpdater.Register(object objectToUpdate, bool isPausable, params EUpdatePass[] updatePassList)
        { }

        void IUpdater.SetPause(bool pause)
        { }

        void IUpdater.Unregister(object objectToUpdate, params EUpdatePass[] updatePassList)
        { }

        void IUpdater.Update()
        { }
    }
}
