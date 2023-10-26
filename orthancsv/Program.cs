using System.Collections.Concurrent;
using System.Data;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using CsvHelper;
using orthancsv.Models;


using HttpClient client = new();

List<string> studyIds;

if(args.Length is 0 or > 1) {
    Console.WriteLine("Invalid number of arguments");
    return;
}

try
{
    studyIds = await RetrieveStudyIdsByModality(client, args[0]);
}
catch (Exception ex)
{
    Console.WriteLine($"Unable to retrieve study by modality: {ex.Message}");
    return;
}


if (!studyIds.Any())
{
    Console.WriteLine($"Unable to find any studies for this modality {args[0]}");
}
else
{
    var targetCsv = $"c:\\csv\\{args[0]}.csv";
    await using var writer = new StreamWriter(targetCsv);
    await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

    csv.WriteHeader<StudyCsv>();
    csv.NextRecord();

    var studyCsvs = new ConcurrentQueue<StudyCsv>();

    var totalStudies = studyIds.Count;
    var currentStudyCount = 0;

    foreach (var studyId in studyIds)
    {
        currentStudyCount++;
        Console.WriteLine($"Processing {currentStudyCount}/{totalStudies}, studyId: {studyId}");

        try
        {
            await using var stream = await client.GetStreamAsync($"http://localhost:8042/studies/{studyId}");

            var accessionNum = (await JsonSerializer.DeserializeAsync<Study>(stream))?.DicomTag.AccessionNum;

            studyCsvs.Enqueue(new StudyCsv { AccessionNumber = accessionNum ?? "", OrthancStudyId = studyId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to query for study id : {studyId}");
        }

    }

    Console.WriteLine("Writing to csv");
    await csv.WriteRecordsAsync(studyCsvs);
    Console.WriteLine($"csv ready at {targetCsv}");
}




static async Task<List<string>> RetrieveStudyIdsByModality(HttpClient client, string modality)
{
    var studyLevelQuery = new LevelQuery
    {
        Level = "Study",
        Query = new Query
        {
            ModalitiesInStudy = modality
        }
    };

    var response = await client.PostAsJsonAsync("http://localhost:8042/tools/find", studyLevelQuery);
    var studyIds = await JsonSerializer.DeserializeAsync<List<string>>(await response.Content.ReadAsStreamAsync());
    return studyIds;
}
