// See https://aka.ms/new-console-template for more information
using MessageDrop.EF;

internal class Program
{
    private static MessageDropDataContext context = new MessageDropDataContext();
    static void Main(string[] args)
    {
        context.Database.EnsureCreated();
        context.SaveChanges();
        Console.WriteLine("Db initialized... Press any key...");
        Console.ReadLine();
    }

}




