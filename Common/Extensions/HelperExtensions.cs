namespace Common.Extensions;

public static class HelperExtensions
{
    public static int Duration(this DateTime startDate, DateTime endDate)
    {
        TimeSpan difference = endDate.Date - startDate.Date;
        return difference.Days;
    }
    
    public static int GetRandomNumber()
    {
        var random = new Random();
        return random.Next(5, 15);
    }
}