public class FisierDTO
{
    public FisierDTO(Fisier f)
    {
        id = f.Id;
        name = f.Name;
        size = f.Size;
    }

    public int id { get; set; }
    public string name { get; set; }
    public long size { get; set; }
}