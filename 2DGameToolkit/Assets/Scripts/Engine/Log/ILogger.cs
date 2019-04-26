
public interface ILogger
{
    void Log(object message);
    void Warning(object message);
}

public class LoggerProxy : UniqueProxy<ILogger>
{ }
