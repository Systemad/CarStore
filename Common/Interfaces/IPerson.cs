namespace Common.Interfaces;

public interface IPerson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string SSN { get; set; }
}