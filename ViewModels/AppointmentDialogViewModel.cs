using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using organizeIT.Models;
using Avalonia.Media;

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

    [ObservableProperty]
    private bool showValidationErrors;

    [ObservableProperty]
    private bool canSave = true;

    partial void OnTitleChanged(string value)
    {
        CanSave = !string.IsNullOrWhiteSpace(value);
    }

    public Brush TitleError => ShowValidationErrors && string.IsNullOrWhiteSpace(Title) 
        ? new SolidColorBrush(Colors.Red) 
        : new SolidColorBrush(Colors.Gray);

    public Action<Appointment?> OnClose { get; set; } = _ => { };

    [RelayCommand]
    private void Save()
    {
        ShowValidationErrors = true;
        
        if (string.IsNullOrWhiteSpace(Title))
        {
            return;
        }

        DateTime startDateTime = StartDate.Date + StartTime;
        DateTime endDateTime = EndDate.Date + EndTime;

        if (endDateTime <= startDateTime)
        {
            // TODO: Fehlermeldung anzeigen
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