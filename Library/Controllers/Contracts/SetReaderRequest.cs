namespace Library.Controllers.Contracts;

public class SetReaderRequest
{
    public int ReaderId { get; set; }
    
    public string FIO { get; set; }

    public DateTime Birthdate { get; set; }
}