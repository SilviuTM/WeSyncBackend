public class FisierDTO
{
    public FisierDTO(Fisier f)
    {
        id = f.Id;
        name = f.Name;
        size = f.Size;
        virtualPath = f.VirtualPath;
        ExpirationTime = f.ExpirationTime;
    }

    public int id { get; set; }
    public string name { get; set; }
    public long size { get; set; }
    public string virtualPath { get; set; }
    public DateTime? ExpirationTime { get; set; }
}