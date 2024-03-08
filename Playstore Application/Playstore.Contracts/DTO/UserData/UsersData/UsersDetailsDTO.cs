using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Playstore.Contracts.DTO.AppReview;
public class UsersDetailsDTO
{
    public string Name{get;set;}
    public List<string> Role{get;set;}
    public string Email{get;set;}
    public string PhoneNumber{get;set;}
}