using Playstore.Contracts.Data.Entities;
using Microsoft.AspNetCore.Http;
namespace Playstore.Contracts.DTO;
public class Myappdetails
{

    public Guid AppId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Logo { get; set; }
    public DateTime PublishedDate { get; set; }
    public string PublisherName { get; set; }
    public int Apps{get;set;}
    public string CategoryName { get; set; }=string.Empty;
    public Double Rating{get;set;}
    public int Downloads{get;set;}

}

