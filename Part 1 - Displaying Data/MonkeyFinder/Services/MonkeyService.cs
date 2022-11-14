using Android.Net.Wifi.Aware;
using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient = new();
    List<Monkey> monkeyList = new();

    public async Task<List<Monkey>> GetMonkeysAsync()
    {
        if (monkeyList.Count > 0)
            return monkeyList;

        var url = "https://www.montemagno.com/monkeys.json";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            // This Read form Json Async method is optimized and does not require Newtonsoft
            // Newtonsoft intorduces .5MG of data into the dll 
            monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        return monkeyList;
    }

}
