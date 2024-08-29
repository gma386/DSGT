using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TaskService
{
    private readonly string _filePath = "tasks.json";

    public List<TaskItem> GetAllTasks()
    {
        if (!File.Exists(_filePath))
            return new List<TaskItem>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json);
    }

    public void SaveTasks(List<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(_filePath, json);
    }
}
