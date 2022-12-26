using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Pattern
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            return _nextHandler.Handle(request);
        }
    }
    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request as string == "Banana")
            {
                return $"Invalid request. request : {request}";
            }
            else
            {
                return Handle(request);
            }
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Nut")
            {
                return $"Squirrel: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    public class Test
    {
        public Test()
        {
            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var squirrel1 = new SquirrelHandler();

            monkey.SetNext(squirrel).SetNext(squirrel1);
            // chain
        }
    }
}