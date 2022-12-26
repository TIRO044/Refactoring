namespace DesignPattern.Pattern
{
    public class Payload
    {
        public string Value;
    }

    public class Subject
    {
        public IList<IObserver<Payload>> Observers { get; set; } = new List<IObserver<Payload>>();

        public IDisposable Subscribe(IObserver<Payload> observer)
        {
            if (Observers.Contains(observer) == false)
            {
                Observers.Add(observer);
            }

            return new UnSubscriber(observer, Observers);
        }

        public void SendMessage(string message)
        {
            foreach (var t in Observers)
            {
                t.OnNext(new Payload() { Value = message });
            }
        }
    }

    public class UnSubscriber : IDisposable
    {
        private IObserver<Payload> observer;
        private IList<IObserver<Payload>> observers;

        public UnSubscriber(IObserver<Payload> observer, IList<IObserver<Payload>> observers)
        {
            this.observer = observer;
            this.observers = observers;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }

    class Observer : IObserver<Payload>
    {
        public string Value;
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Payload value)
        {
            Value = value.Value;
            throw new NotImplementedException();
        }

        public IDisposable Register(Subject subject)
        {
            return subject.Subscribe(this);
        }
    }

    public class ObserverTest
    {
        public void RegisterObserver()
        {
            var observer = new Observer();
            var subject = new Subject();
            var disposer = observer.Register(subject);
            
            subject.SendMessage("RegisterObserver");

            // 요런 식이다. 근데 이건 Subject를 좀 주도적으로 컨트롤 하는 구조가 핑료하구나

            // 요런 식이다.. 요러믄 뭐 흠 부모 클래스를 subjecter로 하고 자식을 subject로 하는 구조라던가?
            // 
            disposer.Dispose();
        }
    }
}