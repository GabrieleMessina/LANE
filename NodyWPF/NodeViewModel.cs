using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace NodyWPF;

public class NodeViewModel
{
    public string Id { get; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; }
    public string[] Props { get; set; }
    public Point Location { get; set; }

    public ObservableCollection<ConnectorViewModel> Input { get; set; } = new ObservableCollection<ConnectorViewModel>();
    public ObservableCollection<ConnectorViewModel> Output { get; set; } = new ObservableCollection<ConnectorViewModel>();

    public NodeViewModel(string title, string[] props, Point location = default)
    {
        Name = title;
        Props = props;
        Location = location;
    }
}
