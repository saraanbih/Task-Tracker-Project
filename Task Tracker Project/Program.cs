using System;
using System.Collections.Generic;
using System.Globalization;

namespace TaskTracker
{
    class Task
    {
        public string Title;
        public string Description;
        public DateTime DueDate;
        public string Priority;
        public string Status;
        public string Category;
    }

    class Program
    {
        static Task ReadInfo()
        {
            Task task = new Task();

            Console.Write("Please Enter Task Title: ");
            task.Title = Console.ReadLine();

            Console.Write("Please Enter Task Description: ");
            task.Description = Console.ReadLine();

            Console.Write("Enter the due date (yyyy-MM-dd HH:mm) like (2025-03-01 14:00): ");
            string dateInput = Console.ReadLine();
            try
            {
                DateTime dueDate = DateTime.ParseExact(dateInput, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                Console.WriteLine("The due date you entered is: " + dueDate.ToString("MMMM dd, yyyy hh:mm tt"));
                task.DueDate = dueDate;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format! Setting default due date to today.");
                task.DueDate = DateTime.Now;
            }

            Console.Write("Please Choose Task Priority (High, Medium, or Low): ");
            string pri = Console.ReadLine();
            if (pri.ToLower() == "high" || pri.ToLower() == "medium" || pri.ToLower() == "low")
                task.Priority = pri;
            else
                task.Priority = "Medium";

            task.Status = "Pending";
            task.Category = "Active";

            return task;
        }

        static void AddTasks(List<Task> tasks)
        {
            Console.Write("Do you want to add tasks? (y/n): ");
            char answer = char.Parse(Console.ReadLine());
            if (answer == 'y')
            {
                Console.Write("Enter the number of tasks you want to add: ");
                ushort numTasks = ushort.Parse(Console.ReadLine());
                for (int i = 1; i <= numTasks; i++)
                {
                    Console.WriteLine($"Task {i}");
                    Task task = ReadInfo();
                    tasks.Add(task);
                }
            }
            else
                Console.WriteLine("No tasks were added!");
        }

        static void UpdateTasks(List<Task> tasks)
        {
            Console.Write("Enter the title of the task you want to update: ");
            string title = Console.ReadLine();

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Title.ToLower() == title.ToLower())
                {
                    PrintTask(tasks[i]);
                    Console.Write("Enter the new status (Pending, In Progress, Completed): ");
                    string status = Console.ReadLine();
                    if (status.ToLower() == "pending" || status.ToLower() == "in progress" || status.ToLower() == "completed")
                        tasks[i].Status = status;
                    else
                        tasks[i].Status = "Pending";
                    Console.WriteLine("Updated Task:");
                    PrintTask(tasks[i]);
                    return;
                }
            }
            Console.WriteLine($"No task found with title '{title}'.");
        }

        static void PrintTask(Task task)
        {
            Console.WriteLine("************************** Task Info **************************");
            Console.WriteLine($"Title: {task.Title}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Due Date: {task.DueDate:MMMM dd, yyyy hh:mm tt}");
            Console.WriteLine($"Priority: {task.Priority}");
            Console.WriteLine($"Status: {task.Status}");
            Console.WriteLine($"Category: {task.Category}");
            Console.WriteLine("****************************************************************");
        }

        static void ViewTasks(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available");
                return;
            }

            foreach (Task task in tasks)
            {
                PrintTask(task);
                Console.WriteLine();
            }
        }

        static void DeleteTasks(List<Task> tasks)
        {
            Console.Write("Are you sure you want to delete all tasks? (y/n): ");
            char answer = char.Parse(Console.ReadLine());

            if (answer == 'y' || answer == 'Y')
            {
                tasks.Clear();
                Console.WriteLine("All tasks removed successfully!");
            }
            else
            {
                Console.WriteLine("No tasks were removed");
            }
        }

        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();

            //Sample tasks
            tasks.Add(new Task { Title = "Pray", Description = "Go to the mosque",
                DueDate = DateTime.Now, Category = "Active", Status = "Pending", Priority = "High" });
            tasks.Add(new Task { Title = "Sport", Description = "Running",
                DueDate = DateTime.Parse("2025-01-31 06:00"), Category = "Active", Status = "In Progress", Priority = "Medium" });

            while (true)
            {
                Console.WriteLine("\n1. Add Tasks\n2. Update Tasks\n3. View Tasks\n4. Delete Tasks\n5. Exit");
                Console.Write("Please Enter Number (1-5) according to your Choice: ");

                try
                {
                    ushort choice = ushort.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddTasks(tasks);
                            break;
                        case 2:
                            UpdateTasks(tasks);
                            break;
                        case 3:
                            ViewTasks(tasks);
                            break;
                        case 4:
                            DeleteTasks(tasks);
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("You have entered a number not between 1 to 5.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("You have entered an invalid number!");
                }
            }
        }
    }
}
