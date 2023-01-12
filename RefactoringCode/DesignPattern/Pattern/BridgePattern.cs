
namespace DesignPattern.Pattern
{
    public class Abstraction
    {
        protected IImplementation Implementation;

        public Abstraction(IImplementation implementation)
        {
            Implementation = implementation;
        }

        public virtual string Operation()
        {
            return "Abstraction: baseOperation";
        }
    }

    public class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation) {}

        public override string Operation()
        {
            return "5";
        }
    }

    public interface IImplementation
    {
        string OperationImplementation();
    }

    public class ConcreteImplementationA : IImplementation
    {
        public string OperationImplementation()
        {
            return nameof(ConcreteImplementationA);
        }
    }

    public class Client
    {
        private Abstraction _abstraction;
        public Client(Abstraction abstraction)
        {
            _abstraction = abstraction;
        }

        public void ClientCode()
        {
            _abstraction.Operation();
        }
    }

    public class Main
    {
        public static void Start()
        {
            var client = new Client(new ExtendedAbstraction(new ConcreteImplementationA()));
            client.ClientCode();

            // 설명하는 곳에서도 커맨드랑 전략이랑 비슷하다고 말하고 있네
            // 전략 같은 경우 제일 단순하게 사용할 수 있는 경우고
            // 커맨드 같은 경우는 리시버와 인보커가 있고
            // 브릿지 패턴 같은 경우는 커맨드 패턴에서 클라이언트 하위에 abstraction 이라는 녀석이 추상화 되어서 전략을 들고있음
            // 다 또이 또이 하다 사실
        }
    }
}