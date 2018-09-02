using System;
using System.Transactions;
using Domain.Domain.Circles;
using Domain.Domain.Users;

namespace Domain.Application {
    public class CircleApplicationService {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly IUserRepository userRepository;

        public CircleApplicationService(
            ICircleFactory circleFactory,
            ICircleRepository circleRepository,
            IUserRepository userRepository
        ) {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.userRepository = userRepository;
        }

        public void CreateCircle(string userId, string circleName) {
            using (var transaction = new TransactionScope()) {
                var ownerId = new UserId(userId);
                var owner = userRepository.Find(ownerId);
                if (owner == null) {
                    throw new Exception("owner not found. userId: " + userId);
                }
                var circle = owner.CreateCircle(circleFactory, circleName);
                circleRepository.Save(circle);
                transaction.Complete();
            }
        }

        public void JoinUser(string circleId, string userId) {
            using (var transaction = new TransactionScope()) {
                var targetCircleId = new CircleId(circleId);
                var targetCircle = circleRepository.Find(targetCircleId);
                if (targetCircle == null) {
                    throw new Exception("circle not found. circleId: " + circleId);
                }

                var joinUserId = new UserId(userId);
                var joinUser = userRepository.Find(joinUserId);
                if (joinUser == null) {
                    throw new Exception("user not found. userId: " + userId);
                }

                targetCircle.Join(joinUser); // targetCircle.Users.Add(joinUser); とは書けなくなりました
                circleRepository.Save(targetCircle);
                transaction.Complete();
            }
        }
    }
}
