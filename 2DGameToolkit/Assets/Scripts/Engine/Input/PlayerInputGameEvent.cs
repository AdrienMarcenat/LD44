
public class PlayerInputGameEvent : GameEvent
{
    public PlayerInputGameEvent(string input, EInputState state) : base("Player", EProtocol.Instant)
    {
        m_Input = input;
        m_State = state;
    }

    public string GetInput()
    {
        return m_Input;
    }

    public EInputState GetInputState()
    {
        return m_State;
    }

    private readonly string m_Input;
    private readonly EInputState m_State;
}