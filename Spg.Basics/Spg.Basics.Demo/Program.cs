// See https://aka.ms/new-console-template for more information
using Spg.Basics.Demo;
using System.Globalization;

Console.WriteLine("Hello, World!");

string s = "My first String";

Console.WriteLine(s);

Student student = new Student();
student.Id = 4711;
student.Name = "Max Muster";
student.LastName = "Schrutek2";
student.Gender = Genders.Male;

var fullName= student.GetType().GetMethods();

int i = new int();
i = 5;

Console.WriteLine("Hello" + " " + "World!");
Console.WriteLine("C:\\temp\\databases\\mydb.db");
Console.WriteLine(@"C:\temp\databases\mydb.db");
Console.WriteLine($@"Name: {student.Name} - {student.LastName} ; Path: C:\temp\databases\mydb.db");
Console.WriteLine(string.Format(new CultureInfo("en-US"), "Datum: {0:dd/MM/yyyy}, {1}", DateTime.Now, "asdasdasdasd"));

Nullable<int> a = 12;
if (a.HasValue)
{
    Console.WriteLine(a.Value);
}
else
{
    Console.WriteLine("");
}
Console.WriteLine(a?.ToString() ?? "");

Console.WriteLine(a.HasValue ? a.Value : ""); // das gleiche wie in Java (if-Kurzschreibweise)



Console.In.ReadLine();