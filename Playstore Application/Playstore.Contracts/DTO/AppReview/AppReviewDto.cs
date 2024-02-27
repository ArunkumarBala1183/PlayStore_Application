using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Playstore.Contracts.DTO.AppReview;
public class AppreviewDTO
{
    public Guid AppId{get;set;}
    public Guid UserId{get;set;}
    public int Rating{get;set;}
    public string Commands{get;set;}=string.Empty;
}