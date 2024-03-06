using Microsoft.AspNetCore.Http;
 
public class Files
{
    public string Name{get;set;}
    public byte[] Image{get;set;}
    public string ContentType{get;set;}
   
 
}
public class Image{
    public string Name{get;set;}
    public IFormFile uploadImage{get;set;}
 
}