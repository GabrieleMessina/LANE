﻿using NodeNetwork;
using System.Windows;

namespace NodyWPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        NNViewRegistrar.RegisterSplat();
    }
}
