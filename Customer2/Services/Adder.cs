namespace Customer2.Services;

public class Adder : IAdder
{
    decimal IAdder.Add(decimal a, decimal b)
    {
        if (a < 0 || b < 0 ) { throw new NotImplementedException();}
        return a + b;
        
    }
}
