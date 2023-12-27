using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static List<Student> student = new List<Student>();
    const string jsonFilePath = @"C:\DEV\C#\Track 2 Learning\json\jsonData\School.json";

    static void Main()
    {
        LoadData();

        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Read");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Save and Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateStudent();
                    break;
                case "2":
                    PrintStudent();
                    break;
                case "3":
                    UpdateStudent();
                    break;
                case "4":
                    DeleteStudent();
                    break;
                case "5":
                    SaveData();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void LoadData()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            student = JsonSerializer.Deserialize<List<Student>>(json);
        }
    }

    static void SaveData()
    {
        string json = JsonSerializer.Serialize(student, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFilePath, json);
        Console.WriteLine("Data saved successfully.");
    }

    static void CreateStudent()
    {
        Console.WriteLine("Enter Student details:");
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Age: ");
        int age = int.Parse(Console.ReadLine());

        Student newPerson = new Student { Id = id, Name = name, Age = age };
        student.Add(newPerson);
        Console.WriteLine($"Person {newPerson.Name} added successfully.");
    }

    static void UpdateStudent()
    {
        Console.Write("Enter ID of the Student to update: ");
        int id = int.Parse(Console.ReadLine());

        Student existingPerson = student.FirstOrDefault(p => p.Id == id);
        if (existingPerson != null)
        {
            Console.WriteLine($"Enter new details for Student with ID {id}:");
            Console.Write("Name: ");
            existingPerson.Name = Console.ReadLine();

            Console.Write("Age: ");
            existingPerson.Age = int.Parse(Console.ReadLine());

            Console.WriteLine($"Student with ID {id} updated successfully.");
        }
        else
        {
            Console.WriteLine($"Student with ID {id} not found.");
        }
    }

    static void DeleteStudent()
    {
        Console.Write("Enter ID of the Student to delete: ");
        int id = int.Parse(Console.ReadLine());

        Student personToRemove = student.FirstOrDefault(p => p.Id == id);
        if (personToRemove != null)
        {
            student.Remove(personToRemove);
            Console.WriteLine($"Student with ID {id} deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Student with ID {id} not found.");
        }
    }

    static void PrintStudent()
    {
        Console.WriteLine("Student:");
        foreach (var person in student)
        {
            Console.WriteLine($"ID: {person.Id}, Name: {person.Name}, Age: {person.Age}");
        }
        Console.WriteLine();
    }
}

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
