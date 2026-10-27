using System.Collections.ObjectModel;

namespace TestMauiApp.Models
{
    public class AllCounters
    {
        public string EntryPlaceholder => "Enter counter default value";
        public string AddButtonText => "+ Add New Counter";

        public ObservableCollection<Counter> Counters { get; set; } = new ObservableCollection<Counter>();
    }
}