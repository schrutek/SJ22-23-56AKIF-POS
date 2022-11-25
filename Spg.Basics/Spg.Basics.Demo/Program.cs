// See https://aka.ms/new-console-template for more information
using Spg.Basics.Demo;
using Spg.Basics.Demo.Delegates;
using System.Globalization;
using System.Reflection;

Console.WriteLine("Hello, World!");

string s = "My first String";

Console.WriteLine(s);

Person person = new Person();
person.Id = 4711;
person.FirstName = "";
person.LastName = "Schrutek2";
person.Gender = Genders.Male;

string x = person?.FirstName ?? "noName";


if (person is Student)
{
    Console.WriteLine("Objekt ist Student!");
}
else if (person is Teacher)
{
    Console.WriteLine("Objekt ist Lehrer!");
}

var fullName= person?.GetType().GetMethods() ?? new MethodInfo[0];

int i = new int();
i = 5;

Console.WriteLine("Hello" + " " + "World!");
Console.WriteLine("C:\\temp\\databases\\mydb.db");
Console.WriteLine(@"C:\temp\databases\mydb.db");
Console.WriteLine($@"Name: {person?.FirstName ?? ""} - {person?.LastName ?? ""} ; Path: C:\temp\databases\mydb.db");
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

//string eingabe = Console.In.ReadLine();

// Parse:


int eingabeConverted; // = int.Parse(eingabe);

//if (!int.TryParse(eingabe, out eingabeConverted))
//{
//    Console.WriteLine("Keine Zahl!!!");
//}

//Console.WriteLine(eingabeConverted);


SchoolClass schoolClass = new SchoolClass() { Name = "", MaxStudents = 32 };

List<SchoolClass> schoolClasses = new List<SchoolClass>()
{
    new SchoolClass() {  MaxStudents=10, Name="" },
    new SchoolClass() {  MaxStudents=8, Name="", 
        Students= new List<Student>()
        {
            new Student() { },
            new Student() { },
            new Student() { },
            new Student() { },
        }
    },
    new SchoolClass() {  MaxStudents=10, Name="" },
    new SchoolClass() {  MaxStudents=10, Name="" },
    new SchoolClass() { MaxStudents=12, Name="" }
};

schoolClasses[1].Name = "5AKIF";
schoolClasses[1].Students.ElementAt(2).FirstName = "Martin";


schoolClass.Name = "5AKIF";
//schoolClass.MaxStudents = 32;

Console.WriteLine(schoolClass.Name);


MyStudentList students = new MyStudentList()
{
    new Student() { FirstName="AFirst01", LastName="ALast01", BirthDate=new DateTime(2012, 05, 03) },
    new Student() { FirstName="AFirst02", LastName="ALast02", BirthDate=new DateTime(2002, 06, 03) },
    new Student() { FirstName="BFirst03", LastName="BLast03", BirthDate=new DateTime(1977, 04, 03) },
    new Student() { FirstName="CFirst04", LastName="CLast04", BirthDate=new DateTime(2000, 02, 03) },
    new Student() { FirstName="AFirst10", LastName="ALast10", BirthDate=new DateTime(1981, 03, 03) },
    new Student() { FirstName="CFirst11", LastName="CLast11", BirthDate=new DateTime(1990, 01, 03) },
};

MyStudentList results = students
    .Filter(s => s.BirthDate < new DateTime(2000, 01, 01))
    .Filter(s => s.FirstName.Contains("C"));

MyStudentList results2 = students
    .Filter(s => s.BirthDate < new DateTime(2000, 01, 01) && s.FirstName.Contains("C"));


foreach (Student item in results2)
{
    Console.WriteLine(item.ToString());
}


Person person2 = "Bill,Gates,US".Parse<Person>();


//bool LastNameConatinsA(Student s)
//{
//    return s.LastName.Contains("A");
//}

//bool BirthDateBefore2000(Student s)
//{
//    return s.BirthDate < new DateTime(2000, 01, 01);
//}


Console.In.ReadLine();