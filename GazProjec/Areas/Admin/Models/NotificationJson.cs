using System.Runtime.Serialization;

namespace GazProjec.Areas.Admin.Models
{
    [DataContract]
    public class NotificationJson
    {
        [DataMember(Name = "userIds")]
        public int[] Ids { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "json")]
        public bool Json { get; set; }
    }
}