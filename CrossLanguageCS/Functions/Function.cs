using System;

namespace CrossLanguageCS.Functions
{
    public class Function0 : IFunction
    {
        private readonly Action Action;

        public Function0(Action action)
        {
            this.Action = action;
        }

        public void Invoke()
        {
            Action();
        }
    }

    public class Function1 : IFunction
    {
        private readonly Action<object> Action;

        public Function1(Action<object> action)
        {
            this.Action = action;
        }

        public void Invoke(object a)
        {
            Action(a);
        }
    }

    public class Function2 : IFunction
    {
        private readonly Action<object, object> Action;

        public Function2(Action<object, object> action)
        {
            this.Action = action;
        }

        public void Invoke(object a, object b)
        {
            Action(a, b);
        }
    }

    public class Function3 : IFunction
    {
        private readonly Action<object, object, object> Action;

        public Function3(Action<object, object, object> action)
        {
            this.Action = action;
        }

        public void Invoke(object a, object b, object c)
        {
            Action(a, b, c);
        }
    }

    public class Function4 : IFunction
    {
        private readonly Action<object, object, object, object> Action;

        public Function4(Action<object, object, object, object> action)
        {
            this.Action = action;
        }

        public void Invoke(object a, object b, object c, object d)
        {
            Action(a, b, c, d);
        }
    }

    public class Function5 : IFunction
    {
        private readonly Action<object, object, object, object, object> Action;

        public Function5(Action<object, object, object, object, object> action)
        {
            this.Action = action;
        }

        public void Invoke(object a, object b, object c, object d, object e)
        {
            Action(a, b, c, d, e);
        }
    }

    public class Function6 : IFunction
    {
        private readonly Action<object, object, object, object, object, object> Action;

        public Function6(Action<object, object, object, object, object, object> action)
        {
            this.Action = action;
        }

        public void Invoke(object a, object b, object c, object d, object e, object f)
        {
            Action(a, b, c, d, e, f);
        }
    }
}
