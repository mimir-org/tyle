using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace TypeLibrary.Models.Data
{
    public class ProjectEdge
    {
        public string ProjectId { get; set; }
        public string EdgeId { get; set; }

        public List<SqlParameter> CreateParameters()
        {
            var parameters = new List<SqlParameter>()
            {
                new (nameof(ProjectId), ProjectId),
                new (nameof(EdgeId), EdgeId)
            };

            return parameters;
        }
    }
}
