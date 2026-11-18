using MojaApka.Models;

namespace MojaApka.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class CounterPage : ContentPage
{
    public string ItemId
    {
        set { LoadCounter(value); }
    }

    public CounterPage()
    {
        InitializeComponent();
    }

    private void LoadCounter(string fileName)
    {
        Models.Counter counterModel = new Models.Counter();
        counterModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            counterModel.Name = lines.Length > 0 ? lines[0] : "Licznik";
            counterModel.Value = lines.Length > 1 ? int.Parse(lines[1]) : 0;
            counterModel.Date = File.GetCreationTime(fileName);
        }

        BindingContext = counterModel;
    }

    private void SaveCounter()
    {
        if (BindingContext is Models.Counter counter)
        {
            File.WriteAllText(counter.Filename, $"{counter.Name}\n{counter.Value}");
        }
    }

    private void Plus_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Counter counter)
        {
            counter.Value++;
            ValueLabel.Text = counter.Value.ToString();
            SaveCounter();
        }
    }

    private void Minus_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Counter counter)
        {
            counter.Value--;
            ValueLabel.Text = counter.Value.ToString();
            SaveCounter();
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Counter counter)
        {
            if (File.Exists(counter.Filename))
            {
                File.Delete(counter.Filename);
            }
        }
        await Shell.Current.GoToAsync("..");
    }
}
