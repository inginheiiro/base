using Base.Models.Utils;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using MongoDB.Entities.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Base.Models.Settings
{
    public sealed class OrganizationModel : Entity
    {        
        
        
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string PhoneNumber { get; set; }

        [BsonIgnoreAttribute]
        public IEnumerable<UploadModel> MaterializedUploadedFiles { 
            get { return !string.IsNullOrEmpty(ID) ? UploadedFiles.ChildrenQueryable().ToList() : null; }
        }  

        [JsonIgnore]
         public Many<UploadModel> UploadedFiles { get; set; }  

         public OrganizationModel()
        {
            this.InitOneToMany(() => UploadedFiles);            
        }

    }
}