using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Domain.Circles;
using Domain.Domain.Users;

namespace ProductInfrastructure.Circles
{
    public class CircleNotification : ICircleNotification {
        private CircleId id;
        private string name;
        private List<UserId> userIds;

        public void Id(CircleId id) {
            this.id = id;
        }

        public void Name(string name) {
            this.name = name;
        }

        public void Users(List<UserId> userIds) {
            this.userIds = userIds;
        }

        public CircleDataModel Build() {
            var ids = userIds.Select(x => x.Value).ToList();

            return new CircleDataModel(
                id.Value,
                name,
                ids
            );
        }
    }
}
