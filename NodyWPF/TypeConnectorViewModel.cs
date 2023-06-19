namespace NodyWPF;

public class TypeConnectorViewModel : ConnectorViewModel
{
    public string Type { get; }

    public TypeConnectorViewModel(string type, string description) : base(type, description)
    {
        Type = type;
    }
    public TypeConnectorViewModel(string type) : base(type, type)
    {
        Type = type;
    }
}