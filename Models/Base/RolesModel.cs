
using MongoDB.Entities;

namespace Base.Models.Base
{
    public sealed class RolesModel : Entity
    {                

        public string Role { get; set; }
        
        public bool Default { get; set; }

    }
}
