using System.Collections.Generic;
using CrossLanguageCS.FunctionDispatchers.Serial;

namespace CrossLanguageCS.Functions
{
    public class FunctionProcessor
    {
        public FunctionRegister Register { get; private set; }
        public IFunctionDispatcher Dispatcher { get; protected set; }
        public ParameterParser Parameters { get; private set; }

        private List<object> InboundFunctionParameters;

        public FunctionProcessor()
        {
            Register = new FunctionRegister();
            Parameters = new ParameterParser();
            InboundFunctionParameters = new List<object>(8);
        }

        /// <summary>
        /// Called when a function is received with the given name and serialised parameters which will be parsed later
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public void OnFunctionReceived(string name, string parameters)
        {
            IFunction function = Register.GetFunction(name);
            if (function != null)
            {
                Parameters.DeserialiseAndAppendParameters(InboundFunctionParameters, parameters);
                InvokeFunction(function, InboundFunctionParameters);
                InboundFunctionParameters.Clear();
            }
        }

        public void InvokeFunction(IFunction function, List<object> p)
        {
            int length = p.Count;

            switch (length)
            {
                case 0: ((Function0)function).Invoke(); break;
                case 1: ((Function1)function).Invoke(p[0]); break;
                case 2: ((Function2)function).Invoke(p[0], p[1]); break;
                case 3: ((Function3)function).Invoke(p[0], p[1], p[2]); break;
                case 4: ((Function4)function).Invoke(p[0], p[1], p[2], p[3]); break;
                case 5: ((Function5)function).Invoke(p[0], p[1], p[2], p[3], p[4]); break;
                case 6: ((Function6)function).Invoke(p[0], p[1], p[2], p[3], p[4], p[5]); break;
            }
        }
    }
}
