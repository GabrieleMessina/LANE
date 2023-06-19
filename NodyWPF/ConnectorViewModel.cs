using System.ComponentModel;
using System.Windows;

using NodeNetwork.ViewModels;

namespace NodyWPF;

public class ConnectorViewModel : NodeInputViewModel, INotifyPropertyChanged
{
    private Point _anchor;
    public Point Anchor
    {
        set
        {
            _anchor = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Anchor)));
        }
        get => _anchor;
    }

    private bool _isConnected;

    public bool IsConnected
    {
        set
        {
            _isConnected = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConnected)));
        }
        get => _isConnected;
    }

    public string Id { get; set; }
    public string Description { get; set; }

    public new event PropertyChangedEventHandler? PropertyChanged;

    public ConnectorViewModel(string id, string description = "") : base()
    {
        Id = id;
        Description = description;
    }
}
