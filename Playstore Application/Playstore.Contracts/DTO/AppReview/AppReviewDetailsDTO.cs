using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Playstore.Contracts.DTO.AppReview;
public class AppReviewDetailsDTO
{
    public Guid AppId{get;set;}
    // public Double AvergeRatings{get;set;}
    // public int AppCount{get;set;}
    public List<string> Username{get;set;}
    
    public Dictionary<Guid,List<string>> Commands{get;set;}=new Dictionary<Guid,List<string>>();
}