using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class TaskController : Controller
{
    private readonly TaskService _taskService;

    public TaskController()
    {
        _taskService = new TaskService();
    }

    public IActionResult Index()
    {
        var tasks = _taskService.GetAllTasks();
        return View(tasks);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        var tasks = _taskService.GetAllTasks();
        task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
        tasks.Add(task);
        _taskService.SaveTasks(tasks);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var tasks = _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        return View(task);
    }

    [HttpPost]
    public IActionResult Edit(TaskItem task)
    {
        var tasks = _taskService.GetAllTasks();
        var taskToUpdate = tasks.FirstOrDefault(t => t.Id == task.Id);
        if (taskToUpdate == null) return NotFound();

        taskToUpdate.Title = task.Title;
        taskToUpdate.Description = task.Description;
        taskToUpdate.Status = task.Status;
        _taskService.SaveTasks(tasks);

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var tasks = _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        tasks.Remove(task);
        _taskService.SaveTasks(tasks);

        return RedirectToAction("Index");
    }
}
