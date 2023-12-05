public class FisierDTO
{
    public FisierDTO(Fisier f)
    {
        id = f.id;
        name = f.name;
        size = f.size;
    }

    public int id { get; set; }
    public string name { get; set; }
    public long size { get; set; }
}