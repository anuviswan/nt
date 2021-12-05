using System.Collections.Generic;
using Nt.Utils.Interfaces;

namespace Nt.Utils.Models
{
    public class User:IHasId
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public IEnumerable<string> Followers { get; set; }
        public IEnumerable<string> Follows { get; set; }
    }
}
