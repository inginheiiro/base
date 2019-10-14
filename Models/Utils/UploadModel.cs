using Base.Models.Base;
using MongoDB.Entities.Common;
using System;
using System.Collections.Generic;

namespace Base.Models.Utils
{
    public class UploadModel: Entity
    {
        public string Title { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }        
        public List<Tuple<DateTime,UsersModel>> UploadedHistory { get; set; }     

    }
}
