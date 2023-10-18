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


studyIds = await RetrieveStudyIdsByModality(client, args[0]);

if (!studyIds.Any())
{
    Console.WriteLine($"Unable to find any studies for this modality {args[0]}");
}
else
{
    await using var writer = new StreamWriter("c:\\csv\\test.csv");
    await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

    csv.WriteHeader<StudyCsv>();
    csv.NextRecord();

    foreach (var studyId in studyIds)
    {
        await using var stream = await client.GetStreamAsync($"http://localhost:8042/studies/{studyId}");

        var accessionNum = (await JsonSerializer.DeserializeAsync<Study>(stream))?.DicomTag.AccessionNum;

        var studyCsv = new StudyCsv { AccessionNumber = accessionNum ?? "", OrthancStudyId = studyId };

        csv.WriteRecord(studyCsv);
        csv.NextRecord();
    }
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
