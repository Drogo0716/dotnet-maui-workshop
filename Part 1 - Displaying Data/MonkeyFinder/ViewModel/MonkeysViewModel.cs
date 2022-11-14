using MonkeyFinder.Services;


namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    MonkeyService monkeyService;
    public MonkeysViewModel(MonkeyService monkeyService) 
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
    }

    // Command class comes with MAUI but does not handle async code well, but CommunityToolkit does
    [RelayCommand]
    async Task GetMonkeysAsync() 
    {
        if (IsBusy)
            return;

        try 
        {
            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeysAsync();

            if (Monkeys.Count != 0)
                Monkeys.Clear();

            foreach(var monkey in monkeys) 
            {
                // Look into ObservableRangCollection.cs, should be on JM's github
                Monkeys.Add(monkey);
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"The following error occurred: {ex.Message}", "OK");
        }
        finally 
        {
            IsBusy= false;
        }
    }
}
