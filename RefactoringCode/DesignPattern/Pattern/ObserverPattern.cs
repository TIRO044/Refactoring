namespace DesignPattern.Pattern
{
    public class Payload
    {
        public string Value;
    }

    public class Subject
    {
        public IList<IObserver<Payload>> Observers { get; set; } = new List<IObserver<Payload>>();

        public IDisposable Attach(IObserver<Payload> observer)
        {
            if (Observers.Contains(observer) == false)
            {
                Observers.Add(observer);
            }

            return new UnSubscriber(observer, Observers);
        }

        public void Notify(string message)
        {
            foreach (var t in Observers)
            {
                t.OnNext(new Payload() { Value = message });
            }
        }
    }

    public class UnSubscriber : IDisposable
    {
        private readonly IObserver<Payload> _observer;
        private readonly IList<IObserver<Payload>> _observers;

        public UnSubscriber(IObserver<Payload> observer, IList<IObserver<Payload>> observers)
        {
            this._observer = observer;
            this._observers = observers;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
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
        }

        public IDisposable Register(Subject subject)
        {
            return subject.Attach(this);
        }
    }

    public class ObserverTest
    {
        public void RegisterObserver()
        {
            var observer = new Observer();
            var subject = new Subject();
            var disposer = observer.Register(subject);
            
            subject.Notify("RegisterObserver");
            disposer.Dispose();
        }
    }


    public class Tset{

    }

    public class TestChild : Tset
    {

    }

    public class TT
    {
        public void Tt()
        {
            IEnumerable<string> strings = new List<string>();
            IEnumerable<object> objects = strings;

            IEnumerable<object> objects1 = new List<object>();
            //IEnumerable<string> stringss = objects1;
        }
    }
}


namespace Observer1
{
    public struct ObserveData
    {
        public ObserveData()
        {

        }

        public string NotifyString { set; get; } = "OnEnd!";
    }

    public class NotifyTarget : IObserver<ObserveData> // IObserver<T> 반공변성 부모 -> 자식 대입 가능
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ObserveData value)
        {
            throw new NotImplementedException();
        }
    }


    public interface ISubject
    {
        void Add(IObserver<ObserveData> observer);
        void Remove(IObserver<ObserveData> observer);
        void Notify();
    }

    public class Subject : ISubject
    {
        private readonly List<IObserver<ObserveData>> _observers = new();

        public void Add(IObserver<ObserveData> observer)
        {
            _observers.Add(observer);
        }

        public void Remove(IObserver<ObserveData> observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            foreach (var t in _observers)
            {
                t.OnNext(new ObserveData());
            }
        }
    }
}