using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using organizeIT.Models;
using System.Collections.ObjectModel;

namespace organizeIT.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Appointment> appointments = new();

    [ObservableProperty]
    private Appointment? selectedAppointment;

    [RelayCommand]
    private void AddAppointment()
    {
        var appointment = new Appointment
        {
            Id = Appointments.Count + 1,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1)
        };
        
        Appointments.Add(appointment);
        SelectedAppointment = appointment;
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
