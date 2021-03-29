using System.Collections.Generic;
using System.Reflection;

namespace CrossLanguageCS.Functions
{
    public class FunctionProcessor
    {
        public FunctionTable Table;
        public ParameterCreator Parameters;

        private List<string> InboundFunctionParameters;

        public FunctionProcessor()
        {
            Table = new FunctionTable();
            Parameters = new ParameterCreator();

            InboundFunctionParameters = new List<string>(6);
        }

        /// <summary>
        /// Called when a function is received with the given name and serialised parameters which will be parsed later
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public void OnFunctionReceived(string name, string parameters)
        {
            // sendPing
            // s'www.google.co.uk',i1000

            IFunction function = Table.GetFunction(name);
            if (function != null)
            {
                List<object> paramz = new List<object>(6);
                Parameters.DeserialiseAndAppendParameters(paramz, parameters);
                InvokeFunction(function, paramz);
            }
        }

        public void InvokeFunction(IFunction function, List<object> p)
        {
            int length = p.Count;
            InvokeFunction(function,
                length > 0 ? p[0] : null,
                length > 1 ? p[1] : null,
                length > 2 ? p[2] : null,
                length > 3 ? p[3] : null,
                length > 4 ? p[4] : null,
                length > 5 ? p[5] : null);
        }

        public void InvokeFunction<A, B, C, D, E, F>(IFunction function, A a, B b, C c, D d, E e, F f)
        {
            if (a == null && b == null && c == null && d == null && e == null && f == null) InvokeFunction0((Function)function);
            else if (b == null && c == null && d == null && e == null && f == null) InvokeFunction1((Function<A>)function, a);
            else if (c == null && d == null && e == null && f == null) InvokeFunction2((Function<A,B>)function, a, b);
            else if (d == null && e == null && f == null) InvokeFunction3((Function<A,B,C>)function, a, b, c);
            else if (e == null && f == null) InvokeFunction4((Function<A,B,C,D>)function, a, b, c, d);
            else if (f == null) InvokeFunction5((Function<A,B,C,D,E>)function, a, b, c, d, e);
            else InvokeFunction6((Function<A,B,C,D,E,F>)function, a, b, c, d, e, f);
        }

        private void InvokeFunction0(Function function)
            => function.Invoke();
        private void InvokeFunction1<T1>(Function<T1> function, T1 a) 
            => function.Invoke(a);
        private void InvokeFunction2<T1, T2>(Function<T1, T2> function, T1 a, T2 b) 
            => function.Invoke(a, b);
        private void InvokeFunction3<T1, T2, T3>(Function<T1, T2, T3> function, T1 a, T2 b, T3 c) 
            => function.Invoke(a, b, c);
        private void InvokeFunction4<T1, T2, T3, T4>(Function<T1, T2, T3, T4> function, T1 a, T2 b, T3 c, T4 d)
            => function.Invoke(a, b, c, d);
        private void InvokeFunction5<T1, T2, T3, T4, T5>(Function<T1, T2, T3, T4, T5> function, T1 a, T2 b, T3 c, T4 d, T5 e) 
            => function.Invoke(a, b, c, d, e);
        private void InvokeFunction6<T1, T2, T3, T4, T5, T6>(Function<T1, T2, T3, T4, T5, T6> function, T1 a, T2 b, T3 c, T4 d, T5 e, T6 f) 
            => function.Invoke(a, b, c, d, e, f);
    }
}
