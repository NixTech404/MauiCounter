using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaApka.Models
{
    class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; set; } = new ObservableCollection<Counter>();

        public string AddButtonText => "Dodaj licznik";

        public AllCounters()
        {
            LoadCounters();
        }

        public void LoadCounters()
        {
            Counters.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Counter> counters = Directory.EnumerateFiles(appDataPath, "*.counter.txt")
                .Select(filename =>
                {
                    string[] lines = File.ReadAllLines(filename);
                    return new Counter()
                    {
                        Filename = filename,
                        Name = lines.Length > 0 ? lines[0] : "Licznik",
                        Value = lines.Length > 1 ? int.Parse(lines[1]) : 0,
                        Date = File.GetCreationTime(filename)
                    };
                })
                .OrderBy(x => x.Date);

            foreach (Counter counter in counters)
            {
                Counters.Add(counter);
            }
        }
    }
}
