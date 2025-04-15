
using Microsoft.EntityFrameworkCore;
using PersonManagement1.Data;
using PersonManagement1.Domain;
using Spectre.Console;

public class Program
{
    static void RenderLayout()
    {
        var topPanel = new Panel(
            new Markup("[bold yellow]Menu[/]\n" +
                        "[blue]1.Add Person[/]\n" +
                        "[blue]2.Delete Person[/]\n" +
                        "[blue]3.Get all Person[/]\n" +
                        "[blue]4.Get Person by id[/]\n" +
                        "[blue]5.Exit[/]\n"
                        )).Border(BoxBorder.Rounded).Expand();

        var bottomPanel = new Panel(
             new Markup("[bold yellow] Input Area:[/]\n" +
                        "[yellow]Follow instruction to interact with the system[/]"
                        )).Border(BoxBorder.Rounded).Expand();
        var grid = new Grid();
        grid.AddColumn(new GridColumn().NoWrap());
        grid.AddRow(topPanel);
        grid.AddRow(bottomPanel);

        AnsiConsole.Write(grid);
    }
    public static Person GetPerson()
    {
        return new Person
        {
            FirstName = AnsiConsole.Ask<string>("Enter First Name: "),
            LastName = AnsiConsole.Ask<string>("Enter Last Name: "),
            Address = new Address
            {
                Country = AnsiConsole.Ask<string>("Enter Country:"),
                State = AnsiConsole.Ask<string>("Enter State: "),
                City = AnsiConsole.Ask<string>("Enter City: "),
                Pincode = int.Parse(AnsiConsole.Ask<string>("Enter PinCode: "))
            },
            DateOfBirth = new DOB
            {
                Date = int.Parse(AnsiConsole.Ask<string>("Enter Date: ")),
                Month = int.Parse(AnsiConsole.Ask<string>("Enter Month: ")),
                Year = int.Parse(AnsiConsole.Ask<string>("Enter Year: "))
            }
        };
    }

    static void Main(string[] args)
    {
        using (var dbContext = new PersonManagementDbContext(new DbContextOptions<PersonManagementDbContext>()))
        {
            #region previous code
            //var person = new Person
            //{
            //    FirstName = "karthik",
            //    LastName = "balabathula",
            //    Address = new Address
            //    {
            //        City = "Hyderabad",
            //        State = "Telangana",
            //        Country = "India"
            //    },
            //    DateOfBirth = new DOB
            //    {
            //        Date = 23,
            //        Month = 11,
            //        Year = 2004
            //    }
            //};
            //dbContext.Persons.Add(person);
            //dbContext.SaveChanges();
            //Console.WriteLine("Operation Successfull");
            #endregion

            //ConsoleApplication user input code
            bool isExit = true;
            while (isExit) 
            {
                AnsiConsole.Clear();
                RenderLayout();
                int input = int.Parse(AnsiConsole.Ask<string>("Please select an option from above list"));

                switch (input) {
                    case 1:
                        AnsiConsole.Clear();
                        dbContext.Persons.Add(GetPerson());
                        dbContext.SaveChanges();
                        AnsiConsole.WriteLine("Person Added Successfully");
                        AnsiConsole.WriteLine("Enter any key to go back to mehu");
                        Console.ReadLine();
                        break;
                    case 2:
                        AnsiConsole.Clear();
                        int id = int.Parse(AnsiConsole.Ask<string>("Enter Id of Person to delete: "));
                        var person = dbContext.Persons.FirstOrDefault(x => x.Id == id);
                        if(person == null)
                        {
                            AnsiConsole.WriteLine("Invalid Id or person Not found in Database!");
                            break;
                        }
                        dbContext.Persons.Remove(person);
                        AnsiConsole.WriteLine($"{person.FirstName}{person.LastName} has been removed successfully from Database");
                        AnsiConsole.WriteLine("Enter a key to go back to menu");
                        Console.ReadLine();
                        break;
                    case 3:
                        AnsiConsole.Clear();
                        var personList = dbContext.Persons.Include(a=>a.Address)
                                                          .Include(d=>d.DateOfBirth)
                                                          .ToList();

                        foreach (var ind in personList)
                        {
                            AnsiConsole.WriteLine($"Name:{ind.FirstName} {ind.LastName}");
                            AnsiConsole.WriteLine($"Address:{ind.Address.Country}, {ind.Address.State}, {ind.Address.City}, {ind.Address.Pincode}");
                            AnsiConsole.WriteLine($"Date of Birth: {ind.DateOfBirth.Date}/{ind.DateOfBirth.Month}/{ind.DateOfBirth.Year}");
                        }
                        AnsiConsole.WriteLine("Enter a key to go back to menu");
                        Console.ReadLine();
                        break;
                    case 4:
                        AnsiConsole.Clear();
                        int pfindingId = int.Parse(AnsiConsole.Ask<string>("Enter id of Person to View: "));
                        var personById = dbContext.Persons.Include(p=>p.Address)
                                                          .Include(d=>d.DateOfBirth)
                                                          .FirstOrDefault(x => x.Id == pfindingId);
                        if(personById == null)
                        {
                            AnsiConsole.WriteLine("Person Not Found in Database!");
                            break;
                        }
                        AnsiConsole.WriteLine($"Name:{personById.FirstName} {personById.LastName}");
                        AnsiConsole.WriteLine($"Address:{personById.Address.Country}, {personById.Address.State}, {personById.Address.City}, {personById.Address.Pincode}");
                        AnsiConsole.WriteLine($"Date of Birth: {personById.DateOfBirth.Date}/{personById.DateOfBirth.Month}/{personById.DateOfBirth.Year}");
                        AnsiConsole.WriteLine("Enter a key to go back to menu");
                        Console.ReadLine();
                        break;

                    case 5:
                        isExit = false;
                        break;     
                }
            }
        }
    }
}



