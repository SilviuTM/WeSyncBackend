using System.ComponentModel.DataAnnotations;

public class Fisier
{
    public int id { get; set; }
    public string name { get; set; }
    public long size { get; set; }
    public byte[] content { get; set; }
}