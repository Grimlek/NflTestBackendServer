namespace AngularTestBackendServer.Core.Models;

public class Stadium
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string StadiumTypeName { get; set; }
    public int? Capacity { get; set; }
    public int? OpenYear { get; set; }
    public int? CloseYear { get; set; }
}