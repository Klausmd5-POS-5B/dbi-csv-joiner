foreach (var filePath in Directory.GetFiles(".", "*.csv"))
{
    if(filePath.Contains("_new.csv"))
        continue;
    
    var content = File.ReadAllLines(filePath);

    var name = filePath.Split(".csv")[0];
    int mtyLines = int.Parse(name.Substring(name.Length -2)) > 13 ? 1 : 2;
    
    var header1 = content.Skip(mtyLines).Take(1).First().Split(',').ToList();
    var header2 = content.Skip(mtyLines+1).Take(1).First().Split(',').ToList();

    var newHeader = header1.Zip(header2).Select((x1, x2) =>
    {
        if (x1.Second.Contains("%") && x1.First == "")
        {
            return header1[x2-1] + " " + x1.Second;
        }
        return x1.First.Replace("-", "") + " " + x1.Second;
    }).ToList();
    
    var newContent = content.Skip(mtyLines+1).ToList();
    newContent[0] = String.Join(",", newHeader);
    
    var newFile = filePath.Replace(".csv", "")+"_new.csv";
    File.WriteAllLines(newFile, newContent);
    Console.WriteLine("File {0} created", newFile);
}