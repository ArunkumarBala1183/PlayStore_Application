using Playstore.Contracts.Data.Entities;
using Microsoft.AspNetCore.Http;
namespace Playstore.Contracts.DTO;
public class CreateAppInfoDTO{
   
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;

    public string PublisherName { get; set; }=string.Empty;
    public IFormFile Logo {get;set;}
    public IFormFileCollection appImages{get;set;}
    public IFormFile AppFile{get;set;}
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
   
    

    // public ICollection<byte[]> AppImages { get; set; } = new List<byte[]>();

}

