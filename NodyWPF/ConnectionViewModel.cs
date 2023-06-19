namespace NodyWPF;

public class ConnectionViewModel
{
    public ConnectionViewModel(ConnectorViewModel source, ConnectorViewModel target)
    {
        Source = source;
        Target = target;

        Source.IsConnected = true;
        Target.IsConnected = true;
    }

    public ConnectorViewModel Source { get; }
    public ConnectorViewModel Target { get; }
}