using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Playstore.Contracts.DTO;
public class AppStoreDTO
{

    public Guid FileId { get; set; }
    public string FileName { get; set; }
    public byte[] Logo { get; set; }
    public string Description { get; set; }
    public int Downloads { get; set; }
    public string Category { get; set; }
    public int Apps{get;set;}
    public Double Rating{get;set;}
}