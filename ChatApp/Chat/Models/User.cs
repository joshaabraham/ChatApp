using Prism.Mvvm;

namespace ChatApp.Models
{
    public class User : BindableBase
    {       
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string ProfilePhoto { get; set; }
        public string Name { get; set; }       
    }
}
