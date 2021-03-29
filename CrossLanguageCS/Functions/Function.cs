using System;

namespace CrossLanguageCS.Functions
{
    public class Function : IFunction
    {
        private readonly Action Action;

        public Function(Action action)
        {
            this.Action = action;
        }

        public void Invoke()
        {
            this.Action();
        }
    }

    public class Function<T1> : IFunction
    {
        private readonly Action<T1> Action;

        public Function(Action<T1> action)
        {
            this.Action = action;
        }

        public void Invoke(T1 p1)
        {
            this.Action(p1);
        }
    }

    public class Function<T1, T2> : IFunction
    {
        public readonly Action<T1, T2> Action;

        public Function(Action<T1, T2> action)
        {
            this.Action = action;
        }

        public void Invoke(T1 p1, T2 p2)
        {
            this.Action(p1, p2);
        }
    }

    public class Function<T1, T2, T3> : IFunction
    {
        private readonly Action<T1, T2, T3> Action;

        public Function(Action<T1, T2, T3> action)
        {
            this.Action = action;
        }

        public void Invoke(T1 p1, T2 p2, T3 p3)
        {
            this.Action(p1, p2, p3);
        }
    }

    public class Function<T1, T2, T3, T4> : IFunction
    {
        private readonly Action<T1, T2, T3, T4> Action;

        public Function(Action<T1, T2, T3, T4> action)
        {
            this.Action = action;
        }

        public void Invoke(T1 p1, T2 p2, T3 p3, T4 p4)
        {
            this.Action(p1, p2, p3, p4);
        }
    }

    public class Function<T1, T2, T3, T4, T5> : IFunction
    {
        private readonly Action<T1, T2, T3, T4, T5> Action;

        public Function(Action<T1, T2, T3, T4, T5> action)
        {
            this.Action = action;
        }

        public void Invoke(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {
            this.Action(p1, p2, p3, p4, p5);
        }
    }

    public class Function<T1, T2, T3, T4, T5, T6> : IFunction
    {
        private readonly Action<T1, T2, T3, T4, T5, T6> Action;

        public Function(Action<T1, T2, T3, T4, T5, T6> action)
        {
            this.Action = action;
        }

        public void Invoke(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
        {
            this.Action(p1, p2, p3, p4, p5, p6);
        }
    }
}
