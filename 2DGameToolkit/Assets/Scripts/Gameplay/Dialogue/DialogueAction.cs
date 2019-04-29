
namespace Dialogue
{
    public enum EAction
    {
        None,
        Quit,
        UpgradeForce,
        UpgradeMagic,
        UpgradeJump,
        NextLevel,
        FightDemon,
        EndGame
    }

    public class DialogueCommand : Command
    {
        public DialogueCommand() : base(null)
        {
        }

        public static Command ConstructDialogueCommand(EAction action)
        {
            switch (action)
            {
                case EAction.None:
                    return null;
                case EAction.Quit:
                    return new Quit();
                case EAction.UpgradeForce:
                    return new ChangeStat<UpgradeForce>();
                case EAction.UpgradeMagic:
                    return new ChangeStat<UpgradeMagic>();
                case EAction.UpgradeJump:
                    return new ChangeStat<UpgradeJump>();
                case EAction.NextLevel:
                    return new NextLevel();
                case EAction.FightDemon:
                    return new FightDemon();
                case EAction.EndGame:
                    return new EndGame();
            }
            return null;
        }
    }
    public class ChangeStat<T> : DialogueCommand where T : IStatChange, new()
    {
        public override void Execute()
        {
            new ChangePlayerStatGameEvent(new T()).Push();
        }
    }

    public class Quit : DialogueCommand
    {
        public override void Execute()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    //Application.Quit ();
#endif
        }
    }

    public class NextLevel : DialogueCommand
    {
        public override void Execute()
        {
            LevelManagerProxy.Get().NextLevel();
        }
    }

    public class FightDemon : DialogueCommand
    {
        public override void Execute()
        {
            new DemonFightGameEvent().Push();
        }
    }
    public class EndGame : DialogueCommand
    {
        public override void Execute()
        {
            PlayerManagerProxy.Get().ResetStat();
            LevelManagerProxy.Get().Reset();
            new GameFlowEvent(EGameFlowAction.Quit).Push();
        }
    }
}

