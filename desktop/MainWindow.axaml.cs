using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using JGoodeAIO.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace JGoodeAIO;

public partial class MainWindow : Window
{
    private MainViewModel? _vm;
    private Dictionary<string, Button>? _navButtons;

    public MainWindow()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    protected override void OnLoaded(Avalonia.Interactivity.RoutedEventArgs e)
    {
        base.OnLoaded(e);
        _navButtons = new()
        {
            ["Dashboard"] = BtnDashboard,
            ["Tweaks"]    = BtnTweaks,
            ["Cleaner"]   = BtnCleaner,
            ["Settings"]  = BtnSettings,
            ["GitHub"]    = BtnGitHub,
        };
        UpdateNavHighlights();
    }

    private void OnDataContextChanged(object? sender, System.EventArgs e)
    {
        if (_vm != null) _vm.PropertyChanged -= VmPropertyChanged;
        _vm = DataContext as MainViewModel;
        if (_vm != null) _vm.PropertyChanged += VmPropertyChanged;
        UpdateNavHighlights();
    }

    private void VmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.SelectedNav))
            UpdateNavHighlights();
    }

    private void UpdateNavHighlights()
    {
        if (_navButtons is null || _vm is null) return;
        var accent    = Application.Current?.Resources["AccentBrush"]    as IBrush;
        var accentDim = Application.Current?.Resources["AccentDimBrush"] as IBrush;
        var secondary = Application.Current?.Resources["TextSecondary"]  as IBrush;

        foreach (var (page, btn) in _navButtons)
        {
            bool active    = _vm.SelectedNav == page;
            btn.Background = active ? accentDim  : Brushes.Transparent;
            btn.Foreground = active ? accent      : secondary;
        }
    }

    private void OnTitlebarPress(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            BeginMoveDrag(e);
    }

    private void OnMinimize(object? sender, Avalonia.Interactivity.RoutedEventArgs e) =>
        WindowState = WindowState.Minimized;

    private void OnMaximize(object? sender, Avalonia.Interactivity.RoutedEventArgs e) =>
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;

    private void OnClose(object? sender, Avalonia.Interactivity.RoutedEventArgs e) =>
        Close();
}
