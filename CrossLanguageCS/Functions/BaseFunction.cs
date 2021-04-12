using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguageCS.Functions
{
    public class BaseFunction<A, B, C, D, E, F, G, H> : IFunction
    {
        private readonly Action<A, B, C, D, E, F, G, H> Action;

        public BaseFunction(Action<A, B, C, D, E, F, G, H> action)
        {
            this.Action = action;
        }

        public void Invoke(A a, B b, C c, D d, E e, F f, G g, H h)
        {
            Action(a, b, c, d, e, f, g, h);
        }
    }
}