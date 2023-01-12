namespace DesignPattern.StrategyPattern
{
    class Before
    {
        public interface IMovable
        {
            void Move();
        }

        public class Train : IMovable
        {
            public void Move()
            {
                Console.WriteLine("선로로 이동");
            }
        }

        public class Bus : IMovable
        {
            public void Move()
            {
                Console.WriteLine("도로로 이동");
            }
        }

        public class GoGo
        {
            public void Init()
            {
                var bus = new Bus();
                var Train = new Train();

                bus.Move();
                Train.Move();
                // 여기서 bus가 이동 방법이 여러개 생긴다면?
                // 땅을 달리는 버스와, 하늘을 나는 버스일 경우
                // Move를 
            }
        }
    }

    class After
    {
        public interface IMovable
        {
            //void Move();
        }

        public interface IMove
        {
            void Move();
        }

        public class TrainMove : IMove
        {
            public void Move()
            {
                throw new NotImplementedException();
            }
        }

        public class Train : IMovable
        {
            private readonly IMove _move = new TrainMove();
            public void Move()
            {
                _move.Move();
            }
        }

        public class Bus : IMovable
        {
            public void Move()
            {
                Console.WriteLine("도로로 이동");
            }
        }

        public class GoGo
        {
            public void Init()
            {
                var bus = new Bus();
                var Train = new Train();

                bus.Move();
                Train.Move();
                // 여기서 bus가 이동 방법이 여러개 생긴다면?
                // 땅을 달리는 버스와, 하늘을 나는 버스일 경우
                // Move를 
            }
        }
    }
}
