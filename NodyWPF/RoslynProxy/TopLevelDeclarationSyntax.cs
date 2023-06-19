namespace NodyWPF.RoslynProxy;

public class TopLevelDeclarationSyntax
{
    public string FullDescriptorName { get; }
    public string Name { get; }
    public string Type { get; }

    public TopLevelDeclarationSyntax(string name, string type, string fullDescriptorName)
    {
        FullDescriptorName = fullDescriptorName;
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name}: {Type} ({FullDescriptorName})";
    }
}
