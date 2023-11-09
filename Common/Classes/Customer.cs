using Common.Interfaces;

namespace Common.Classes;

public class Customer : IPerson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string SSN { get; set; }
    public Customer(string name, string lastname, string ssn)
    {
        Name = name;
        Lastname = lastname;
        SSN = ssn;
    }
}