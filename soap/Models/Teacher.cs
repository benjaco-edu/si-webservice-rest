using System.Runtime.Serialization;

namespace MiniprojectSoapService
{
    [DataContract]
    public class Teacher
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}