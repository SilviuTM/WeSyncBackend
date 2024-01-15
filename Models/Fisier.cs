using System.ComponentModel.DataAnnotations;

public class Fisier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    public byte[] Content { get; set; } = new byte[0]; 
    public string VirtualPath { get; set; } = string.Empty;
}