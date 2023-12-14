namespace PingOneDemo.Model
{    
    public class Name
    {
        public string given { get; set; }
        public string family { get; set; }
    }

    public class Population
    {
        public string id { get; set; }
    }
    public class Password
    {
        public string value { get; set; }
        public bool forceChange { get; set; }
    }
    public class RegistrationModel
    {
        public string email { get; set; }
        public Name name { get; set; }
        public Population population { get; set; }
        public string username { get; set; }
        public string department { get; set; }
        public List<string> locales { get; set; }
        public Password password { get; set; }
    }

    public class RegistrationViewModel
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
