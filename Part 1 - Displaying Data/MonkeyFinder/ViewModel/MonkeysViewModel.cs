using MonkeyFinder.Services;


namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    MonkeyService monkeyService;
    IConnectivity connectivity;
    IGeolocation geolocation;
    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation) 
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [RelayCommand]
    async Task GetClosestMonkeyAsync() 
    {
        if (IsBusy || Monkeys.Count == 0)
            return;
        try 
        {
            IsBusy = true;
            var location = await geolocation.GetLastKnownLocationAsync();

            if (location is null) 
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }//|| location.Timestamp.AddMinutes(10) > DateTime.UtcNow) 

            var first = Monkeys.OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles))
                .FirstOrDefault();


            await Shell.Current.DisplayAlert("", first.Name + " " +
                first.Location, "OK");
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");

        }
        finally 
        {
        
        }
    }

    // Command class comes with MAUI but does not handle async code well, but CommunityToolkit does
    [RelayCommand]
    async Task GetMonkeysAsync() 
    {
        if (IsBusy)
            return;

        // Instead of Platform Conditionals the following can be done
        

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No Internet", "Must have internet connection for monkey retireval.",
                "Ok");
            return;
        }

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

    [RelayCommand]
    async Task GoToDetailsAsync(Monkey monkey) 
    {
        if (monkey is null)
            return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>() 
        { 
            { "Monkey", monkey }
        });
    }


}
