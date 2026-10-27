using System.Xml.Linq;
using TestMauiApp.Models;

namespace TestMauiApp.Controllers
{
    public class CountersController
    {
        public void SaveCounter(Counter counter)
        {
            try
            {
                string fileName = $"counter_{counter.Id}.xml";
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                XDocument doc = new XDocument(
                    new XElement("Counter",
                        new XElement("Id", counter.Id),
                        new XElement("Name", counter.Name),
                        new XElement("DefaultValue", counter.DefaultValue),
                        new XElement("Count", counter.Count),
                        new XElement("Red", counter.Red),
                        new XElement("Green", counter.Green),
                        new XElement("Blue", counter.Blue)
                    )
                );
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void DeleteCounterFile(string id)
        {
            try
            {
                string fileName = $"counter_{id}.xml";
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public List<Counter> LoadCounters()
        {
            var counters = new List<Counter>();
            try
            {
                string dir = FileSystem.AppDataDirectory;
                if (Directory.Exists(dir))
                {
                    foreach (var file in Directory.GetFiles(dir, "counter_*.xml"))
                    {
                        try
                        {
                            XDocument doc = XDocument.Load(file);
                            var root = doc.Element("Counter");
                            if (root != null)
                            {
                                counters.Add(new Counter
                                {
                                    Id = root.Element("Id")?.Value ?? Guid.NewGuid().ToString(),
                                    Name = root.Element("Name")?.Value ?? "Licznik",
                                    DefaultValue = int.TryParse(root.Element("DefaultValue")?.Value, out int dv) ? dv : 0,
                                    Count = int.TryParse(root.Element("Count")?.Value, out int c) ? c : 0,
                                    Red = int.TryParse(root.Element("Red")?.Value, out int r) ? r : 255,
                                    Green = int.TryParse(root.Element("Green")?.Value, out int g) ? g : 255,
                                    Blue = int.TryParse(root.Element("Blue")?.Value, out int b) ? b : 255
                                });
                            }
                        }
                        catch(Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return counters;
        }
    }
}