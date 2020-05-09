using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using blazor_folding_rynningeasen.Models;
using Microsoft.AspNetCore.Mvc;

namespace blazor_folding_rynningeasen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly INotifier _notifier;

        public DataController(INotifier notifier)
        {
            _notifier = notifier;
        }

        public ActionResult Post([FromBody] string data)
        {
            var host = Request.Headers["fah-host"].FirstOrDefault();
            _notifier.Push(new KeyValuePair<string, string>(host, $"{host} - {DateTimeOffset.Now}{Environment.NewLine}{Convert(data)}"));

            return Ok();
        }

        private static string tasksStart = "======== Tasks ========";
        private static string projectsStart = "======== Projects ========";
        private static string workUnitsStart = "======== Workunits ========";
        private static string timeStatsStart = "======== Time stats ========";

        static string Convert(string content)
        {
            var temp = content.Substring(0, content.IndexOf("==="));

            var tasksSection = GetSection(content, tasksStart);
            var tasks = GetItems(tasksSection);
            var tasksJson = JsonSerializer.Serialize(tasks);

            //
            var projectsSection = GetSection(content, projectsStart, "GUI URL:");
            var projects = GetItems(projectsSection);
            var projectsJson = JsonSerializer.Serialize(projects);
            //
            var workunitsSection = GetSection(content, workUnitsStart);
            var workunits = GetItems(workunitsSection);
            var workunitJson = JsonSerializer.Serialize(workunits);
            //
            var timeStatsSection = GetSection(content, timeStatsStart);
            var timeStats = GetProps(timeStatsSection);
            var timeStatsJson = JsonSerializer.Serialize(timeStats);

            var hardware = new Hardware {Temperature = double.Parse(temp.Replace("temp=", "").Replace("'C", ""), CultureInfo.InvariantCulture)};
            var hostProjects = JsonSerializer.Deserialize<Project[]>(projectsJson);
            var hostTasks = JsonSerializer.Deserialize<Task[]>(tasksJson);
            var hostWorkUnits = JsonSerializer.Deserialize<WorkUnit[]>(workunitJson);
            var hostTimeStats = JsonSerializer.Deserialize<TimeStats>(timeStatsJson);

            var host = new BoincHost
            {
                Hardware = hardware,
                Projects = hostProjects,
                Tasks = hostTasks,
                WorkUnits = hostWorkUnits,
                TimeStats = hostTimeStats
            };

            return JsonSerializer.Serialize(host, new JsonSerializerOptions { WriteIndented = true });
        }

        private static List<Dictionary<string, object>> GetItems(string section)
        {
            var items = new List<Dictionary<string, object>>();

            var i = 1;
            var indexStart = section.IndexOf($"{i++}) -----------");
            while (indexStart != -1)
            {
                var indexEnd = section.IndexOf($"{i}) -----------");
                var itemString = section.Substring(indexStart, (indexEnd == -1 ? section.Length : indexEnd) - indexStart);
                var props = GetProps(itemString);

                items.Add(props);

                indexStart = section.IndexOf($"{i++}) -----------");
            }

            return items;
        }

        private static Dictionary<string, object> GetProps(string itemString)
        {
            var propsAsStrings = itemString.Split("   ", StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var props = new Dictionary<string, object>();

            foreach (var prop in propsAsStrings)
            {
                var strings = prop.Split(new[] { ':' }, 2).Select(x => x.Trim()).ToArray();
                var key = strings[0].Replace(" ", "");

                if (!props.ContainsKey(key))
                {
                    object value = strings[1];
                    if (DateTime.TryParseExact(strings[1].Replace("  ", " "), "ddd MMM d HH:mm:ss yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var parsed))
                    {
                        value = parsed;
                    }


                    if (strings[1].IsBytesSize())
                    {
                        value = new BytesSize(strings[1]);
                    }


                    props.Add(key, value);
                }
            }

            return props;
        }

        private static string GetSection(string content, string headerPattern, string alternativeEnding = null)
        {
            var indexOfStart = content.IndexOf(headerPattern);
            var indexOfEnd = string.IsNullOrWhiteSpace(alternativeEnding) 
                ? content.IndexOf("=", indexOfStart + headerPattern.Length)
                : content.IndexOf(alternativeEnding, indexOfStart + headerPattern.Length);

            return content.Substring(indexOfStart, (indexOfEnd == -1 ? content.Length : indexOfEnd) - indexOfStart);
        }
    }

    public static class StringExtension
    {
        //public static bool IsDate(this string value)
        //{
        //    var weekDays = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        //    return value.Split(' ').Intersect(weekDays).Any();
        //}

        public static bool IsBytesSize(this string value)
        {
            return value.ToUpperInvariant().Contains(" MB");
        }
    }

    public class BytesSize
    {
        public double Value { get; set; }
        public string Unit { get; set; }

        public BytesSize(string value)
        {
            var strings = value.Split(' ');
            Value = double.Parse(string.Join("", strings.Take(strings.Length - 1)), CultureInfo.InvariantCulture);
            Unit = strings.Last();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}