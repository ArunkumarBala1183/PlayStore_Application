using System.ComponentModel.DataAnnotations.Schema;
using Playstore.Contracts.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Playstore.Contracts.DTO;
public class AppInfoDetailsDTO{
    public Guid AppId { get; set; }
 
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    public byte[] Logo {get;set;}
    public Guid UserId { get; set; }
    public List<byte[]> appImages{get;set;}
    public int Apps{get;set;}
    public Double Rating{get;set;}
    public List<string> Commands{get;set;}
    public string CategoryName{get;set;}
    public Guid CategoryId{get;set;}
    public int Downloads{get;set;}
   
    

    // public ICollection<byte[]> AppImages { get; set; } = new List<byte[]>();

}