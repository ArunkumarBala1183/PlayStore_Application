using System.ComponentModel.DataAnnotations.Schema;
using Playstore.Contracts.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Playstore.Contracts.DTO;
public class AppInfoDTO{
    public int AppId { get; set; }
 
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    public DateTime PublishedDate { get; set; }
    public string PublisherName { get; set; }=string.Empty;
       
    public IFormFile Logo {get;set;}
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public IFormFile appImages{get;set;}
    public IFormFile AppFile{get;set;}
    

    // public ICollection<byte[]> AppImages { get; set; } = new List<byte[]>();

}