using System;
using System.Collections.Generic;
using System.Text;

namespace ProductInfrastructure.Circles
{
    public class CircleDataModel
    {
        public CircleDataModel(string id, string name, List<string> userIds) {
            Id = id;
            Name = name;
            UserIds = userIds;
        }

        public string Id { get; }
        public string Name { get; }
        public List<string> UserIds { get; }
    }
}
