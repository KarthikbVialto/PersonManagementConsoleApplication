
using Microsoft.EntityFrameworkCore;
using PersonManagement1.Data;
using PersonManagement1.Domain;
using Spectre.Console;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

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
    
    static void Main(string[] args)
    {
       

        var builder = CoconaApp.CreateBuilder(args);
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddDbContext<PersonManagementDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PersonManagementDB")));

        builder.Services.AddSingleton<PersonCommands>();
        var app = builder.Build();
        app.AddCommand("addperson",() =>
        {
            var personCommands = app.Services.GetRequiredService<PersonCommands>();
            personCommands.Add();
        });
        app.AddCommand("deleteperson", () =>
        {
            var personCommands = app.Services.GetRequiredService<PersonCommands>();
            personCommands.Delete();
        });

        app.AddCommand("getallpersons", () =>
        {
            var personCommands = app.Services.GetRequiredService<PersonCommands>();
            personCommands.GetAllPerson();
        });

        //Not Working
        app.AddCommand("getperson", () =>
        {
            var personCommands = app.Services.GetRequiredService<PersonCommands>();
            personCommands.GetPerson();
        });

        app.Run();
        

    }
}

public class PersonCommands
{
    private readonly PersonManagementDbContext dbContext;

    public PersonCommands(PersonManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public void Add()
    {
        
        var person = new Person
        {
            FirstName = AnsiConsole.Ask<string>("Enter First Name of Person: "),
            LastName  = AnsiConsole.Ask<string>("Enter Last Name of Person: "),
            Address = new Address
            {
                Country = AnsiConsole.Ask<string>("Enter Country Name: "),
                State = AnsiConsole.Ask<string>("Enter State Name: "),
                City = AnsiConsole.Ask<string>("Enter City Name: ")
            },
            DateOfBirth = new DOB
            {
                Date = int.Parse(AnsiConsole.Ask<string>("Enter Date: ")),
                Month = int.Parse(AnsiConsole.Ask<string>("Enter Month: ")),
                Year = int.Parse(AnsiConsole.Ask<string>("Enter Year: ")),
            }
            
        };
        dbContext.Persons.Add(person);
        dbContext.SaveChanges();
        AnsiConsole.WriteLine("Person Added Successfully");
        Console.ReadLine();
    
    }
    public void Delete()
    {
        int pfindingId = int.Parse(AnsiConsole.Ask<string>("Enter Id of Person to delete: "));
        var personToDelete = dbContext.Persons.Include(a=>a.Address).Include(d=>d.DateOfBirth).FirstOrDefault(x => x.Id == pfindingId);
        if (personToDelete == null)
        {
            Console.WriteLine("Invalid Id!  Person Not Found");
            return;
        }
        dbContext.Persons.Remove(personToDelete);
        dbContext.SaveChanges();
        Console.WriteLine($"{personToDelete.FirstName} {personToDelete.LastName} has been deleted successfully");
        Console.ReadLine();
    }

    public void GetAllPerson()
    {
        var allPersons = dbContext.Persons.Include(a => a.Address).Include(d => d.DateOfBirth)
                                          .ToList();
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Address");
        table.AddColumn("Date of Birth");
        
        foreach(var ind in allPersons)
        {
            table.AddRow
             (
             $"{ind.Id}",
             $"{ind.FirstName} {ind.LastName}",
             $"{ind.Address.Country}, {ind.Address.State}, {ind.Address.City}, {ind.Address.Pincode}",
             $"{ind.DateOfBirth.Date}/{ind.DateOfBirth.Month}/{ind.DateOfBirth.Year}"
             );
            table.AddEmptyRow();
        }
        table.Border(TableBorder.Square);
        table.Expand();
        AnsiConsole.Write(table);
        Console.ReadLine();

    }
    public void GetPerson()
    {
        int id = int.Parse(AnsiConsole.Ask<string>("Enter id to find: "));
        var person = dbContext.Persons.Include(a=>a.Address).Include(d=>d.DateOfBirth).FirstOrDefault(x => x.Id == id);
        if(person == null)
        {
            return;
        }
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Address");
        table.AddColumn("Date of Birth");
        table.AddRow
            (
            $"{person.Id}",
            $"{person.FirstName} {person.LastName}",
            $"{person.Address.Country}, {person.Address.State}, {person.Address.City}, {person.Address.Pincode}",
            $"{person.DateOfBirth.Date}/{person.DateOfBirth.Month}/{person.DateOfBirth.Year}"
            );
        table.Border(TableBorder.Square);
        table.Expand();
        AnsiConsole.Write(table);
        Console.ReadLine();
    }    
    
}


