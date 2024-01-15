using System.ComponentModel.DataAnnotations;
using WeSyncBackend.Models;

public class Fisier
{
    public int Id { get; set; }
    public string Owner { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    public byte[] Content { get; set; } = new byte[0]; 
    public string VirtualPath { get; set; } = string.Empty;

    public List<User> Users { get; set; } = new List<User>();
}