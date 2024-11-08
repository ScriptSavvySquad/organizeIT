using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using organizeIT.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using organizeIT.Views;

namespace organizeIT.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Appointment> appointments = new();

    [ObservableProperty]
    private Appointment? selectedAppointment;

    private Window? _mainWindow;

    public void Initialize(Window mainWindow)
    {
        _mainWindow = mainWindow;
    }

    [RelayCommand]
    private async Task AddAppointment()
    {
        Console.WriteLine("AddAppointment wurde aufgerufen");
        
        if (_mainWindow == null)
        {
            Console.WriteLine("_mainWindow ist null!");
            return;
        }

        var dialog = new AppointmentDialog
        {
            DataContext = new AppointmentDialogViewModel()
        };

        Console.WriteLine("Dialog wurde erstellt");
        
        var vm = (AppointmentDialogViewModel)dialog.DataContext;
        Appointment? newAppointment = null;
        
        vm.OnClose = appointment =>
        {
            Console.WriteLine("OnClose wurde aufgerufen");
            newAppointment = appointment;
            dialog.Close();
        };

        Console.WriteLine("Vor ShowDialog");
        await dialog.ShowDialog(_mainWindow);
        Console.WriteLine("Nach ShowDialog");

        if (newAppointment != null)
        {
            newAppointment.Id = Appointments.Count > 0 ? Appointments.Max(a => a.Id) + 1 : 1;
            Appointments.Add(newAppointment);
            SelectedAppointment = newAppointment;
        }
    }

    [RelayCommand]
    private void DeleteAppointment()
    {
        if (SelectedAppointment != null)
        {
            Appointments.Remove(SelectedAppointment);
            SelectedAppointment = null;
        }
    }
}
