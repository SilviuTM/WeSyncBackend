public class FisierDTO
{
    public FisierDTO(Fisier f)
    {
        id = f.Id;
        name = f.Name;
        size = f.Size;
        virtualPath = f.VirtualPath;
        ExpirationTime = f.ExpirationTime;
        Owner = f.Owner;
    }

    public int id { get; set; }
    public string name { get; set; }
    public long size { get; set; }
    public string virtualPath { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public string Owner { get; set; }
}