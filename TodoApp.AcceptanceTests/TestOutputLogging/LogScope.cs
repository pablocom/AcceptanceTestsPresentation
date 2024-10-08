namespace TodoApp.AcceptanceTests.TestOutputLogging;

public sealed class LogScope
{
    public LogScope? Parent { get; }

    private readonly object _state;
    
    public LogScope(object state, LogScope? parent)
    {
        _state = state;
        Parent = parent;
    }

    public IEnumerable<string?> GetProperties()
    {
        switch (_state)
        {
            case IEnumerable<KeyValuePair<string, object>> pairs:
            {
                foreach (var pair in pairs)
                    yield return $"{pair.Key}: {pair.Value}";
                break;
            }
            case IEnumerable<string> entries:
            {
                foreach (var entry in entries)
                    yield return entry;
                break;
            }
            default:
            {
                yield return _state.ToString();
                break;
            }
        }
    }
}