namespace Policlinica.DB;

public class Service
{
    public int Id { get; set; }
    
    public string ServiceName { get; set; }
    
    public decimal Price { get; set; }
    
    public int DoctorId { get; set; }
}