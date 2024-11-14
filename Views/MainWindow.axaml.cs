using Avalonia.Controls;
using organizeIT.ViewModels;

namespace organizeIT.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        if (DataContext is MainWindowViewModel vm)
        {
            vm.Initialize(this);
        }
    }
}