using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlyoutEvents.Contracts;

namespace FlyoutEvents.ViewModels;

public partial class BaseViewModel : ObservableObject, IEventHandler<MyEvent>
{
    private readonly IMediator mediator;

    public BaseViewModel(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [ObservableProperty]
    private string currentTime = string.Empty;
    
    [RelayCommand]
    private async Task PublishEvent()
    {
        var response = await mediator.Publish(new MyEvent());
    }
    
    [MainThread]
    public Task Handle(MyEvent @event, IMediatorContext context, CancellationToken cancellationToken)
    {
        CurrentTime = $"{DateTime.Now:HH:mm:ss:fff}";
        return Task.CompletedTask;
    }    
}