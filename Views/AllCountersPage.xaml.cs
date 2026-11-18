using MojaApka.Models;

namespace MojaApka.Views;

public partial class AllCountersPage : ContentPage
{
    public AllCountersPage()
    {
        InitializeComponent();

        BindingContext = new Models.AllCounters();
    }

    protected override void OnAppearing()
    {
        ((Models.AllCounters)BindingContext).LoadCounters();
    }

    private async void countersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var counter = (Models.Counter)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(CounterPage)}?{nameof(CounterPage.ItemId)}={counter.Filename}");

            countersCollection.SelectedItem = null;
        }
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Nowy licznik", "Podaj nazwe licznika:", "OK", "Anuluj", "Moj licznik");

        if (!string.IsNullOrEmpty(name))
        {
            string appDataPath = FileSystem.AppDataDirectory;
            string randomFileName = $"{Path.GetRandomFileName()}.counter.txt";
            string filePath = Path.Combine(appDataPath, randomFileName);

            File.WriteAllText(filePath, $"{name}\n0");

            await Shell.Current.GoToAsync($"{nameof(CounterPage)}?{nameof(CounterPage.ItemId)}={filePath}");
        }
    }
}
