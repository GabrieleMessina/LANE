using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace NodyWPF;

public class ClassSyntaxNodeViewModel : NodeViewModel
{
    public ClassDeclarationSyntax ClassDeclaration { get; set; }
    public int Weight { get; set; }
    public new ObservableCollection<TypeConnectorViewModel> Input { get; set; } = new ObservableCollection<TypeConnectorViewModel>();
    public ImmutableArray<TypeConnectorViewModel> ConnectedInputs => Input.Where(i => i.IsConnected).ToImmutableArray();
    public int ConnectedInputsCount => Input.Where(i => i.IsConnected).Count();
    public new ObservableCollection<TypeConnectorViewModel> Output { get; set; } = new ObservableCollection<TypeConnectorViewModel>();
    public ImmutableArray<TypeConnectorViewModel> ConnectedOutputs => Output.Where(o => o.IsConnected).ToImmutableArray();
    public int ConnectedOutputsCount => Output.Where(o => o.IsConnected).Count();
    public ClassSyntaxNodeViewModel(ClassDeclarationSyntax classDeclaration, string[] props, int weight) : base(classDeclaration.Identifier.ValueText, props)
    {
        ClassDeclaration = classDeclaration;
        Weight = weight;
    }
}
