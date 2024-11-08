using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using organizeIT.Models;

namespace organizeIT.ViewModels;

public partial class AppointmentDialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private DateTimeOffset startDate = DateTimeOffset.Now;

    [ObservableProperty]
    private TimeSpan startTime = TimeSpan.FromHours(DateTime.Now.Hour);

    [ObservableProperty]
    private DateTimeOffset endDate = DateTimeOffset.Now;

    [ObservableProperty]
    private TimeSpan endTime = TimeSpan.FromHours(DateTime.Now.Hour + 1);

    [ObservableProperty]
    private string location = string.Empty;

    public Action<Appointment?> OnClose { get; set; } = _ => { };

    [RelayCommand]
    private void Save()
    {
        DateTime startDateTime = startDate.Date + startTime;
        DateTime endDateTime = endDate.Date + endTime;

        if (endDateTime <= startDateTime)
        {
            // In einer vollständigen Implementierung sollten wir hier eine Fehlermeldung anzeigen
            return;
        }

        var appointment = new Appointment
        {
            Title = Title,
            Description = Description,
            StartTime = startDateTime,
            EndTime = endDateTime,
            Location = Location
        };

        OnClose(appointment);
    }

    [RelayCommand]
    private void Cancel()
    {
        OnClose(null);
    }
} 