class Task
{
    public string Name { get; set; } 
    public string Description { get; set; } 
    public string Status { get; set; } 

    public Task(string name, string description, string status)
    {
        Name = name;
        Description = description;
        Status = status;
    }
}

class TaskManager
{
    public delegate void TaskStatusChangedHandler(Task task);
    public event TaskStatusChangedHandler TaskStatusChanged;
    private System.Collections.Generic.List<Task> tasks;
    public TaskManager()
    {
        tasks = new System.Collections.Generic.List<Task>();
    }
    public void AddTask(Task task)
    {
        tasks.Add(task);
    }
    public void RemoveTask(Task task)
    {
        tasks.Remove(task);
    }
    public void UpdateTask(Task task, string name, string description, string status)
    {
        task.Name = name;
        task.Description = description;
        task.Status = status;
    }
    public void CompleteTask(Task task)
    {
        task.Status = "выполнена";

        TaskStatusChanged?.Invoke(task);
    }
}

class TaskSubscriber
{
    public void TaskCompletedNotification(Task task)
    {
        Console.WriteLine($"Задача {task.Name} выполнена!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();

        Task task1 = new Task("Написать код", "Написать код по заданию", "в процессе");
        Task task2 = new Task("Протестировать код", "Проверить работоспособность кода", "");
        Task task3 = new Task("Отправить код", "Отправить код на проверку", "Бегать вокруг себя");

        taskManager.AddTask(task1);
        taskManager.AddTask(task2);
        taskManager.AddTask(task3);

        TaskSubscriber taskSubscriber = new TaskSubscriber();
        taskManager.TaskStatusChanged += taskSubscriber.TaskCompletedNotification;
        taskManager.CompleteTask(task2);
    }
}