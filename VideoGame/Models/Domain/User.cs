using System.ComponentModel.DataAnnotations;

namespace VideoGame.Models.Domain
{
    public class User
    {   
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string UserName { get; set; }
        public string PasswordHashed { get; set; }= string.Empty;
       
    }

}
