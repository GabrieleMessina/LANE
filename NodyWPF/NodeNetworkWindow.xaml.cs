using DynamicData;
using NodeNetwork.ViewModels;
using System.Windows;

namespace NodyWPF;
/// <summary>
/// Interaction logic for NodeNetworkWindow.xaml
/// </summary>
public partial class NodeNetworkWindow : Window
{
    public NodeNetworkWindow()
    {
        InitializeComponent();

        //Create a new viewmodel for the NetworkView
        var network = new NetworkViewModel();

        //Create the node for the first node, set its name and add it to the network.
        //var node1 = new NodeViewModel("Node 1", System.Array.Empty<string>());
        ////network.Nodes.Add(node1);
        //network.Nodes.Add(new() { Name= });

        ////Create the viewmodel for the input on the first node, set its name and add it to the node.
        //var node1Input = new NodeInputViewModel();
        //node1Input.Name = "Node 1 input";
        //node1.Input.Add(node1Input);

        ////Create the second node viewmodel, set its name, add it to the network and add an output in a similar fashion.
        //var node2 = new NodeViewModel();
        //node2.Name = "Node 2";
        //network.Nodes.Add(node2);

        //var node2Output = new NodeOutputViewModel();
        //node2Output.Name = "Node 2 output";
        //node2.Output.Add(node2Output);

        //Assign the viewmodel to the view.
        networkView.ViewModel = network;
    }
}
