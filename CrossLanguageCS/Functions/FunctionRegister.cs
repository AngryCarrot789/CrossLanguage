using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguageCS.Functions
{
    /// <summary>
    /// A class that contains a reference to functions based on their name, and allows you to get, set and invoke them
    /// </summary>
    public class FunctionRegister
    {
        /// <summary>
        /// A map where the key is the function name, and the value is the function itself
        /// </summary>
        public readonly Dictionary<string, IFunction> FunctionsMap;

        public FunctionRegister()
        {
            FunctionsMap = new Dictionary<string, IFunction>();
        }

        #region Registering functions

        public void RegisterFunction(string functionName, Action function)
        {
            FunctionsMap.Add(functionName, new Function0(function));
        }

        public void RegisterFunction(string functionName, Action<object> function)
        {
            FunctionsMap.Add(functionName, new Function1(function));
        }

        public void RegisterFunction(string functionName, Action<object, object> function)
        {
            FunctionsMap.Add(functionName, new Function2(function));
        }

        public void RegisterFunction(string functionName, Action<object, object, object> function)
        {
            FunctionsMap.Add(functionName, new Function3(function));
        }

        public void RegisterFunction(string functionName, Action<object, object, object, object> function)
        {
            FunctionsMap.Add(functionName, new Function4(function));
        }

        public void RegisterFunction(string functionName, Action<object, object, object, object, object> function)
        {
            FunctionsMap.Add(functionName, new Function5(function));
        }

        public void RegisterFunction(string functionName, Action<object, object, object, object, object, object> function)
        {
            FunctionsMap.Add(functionName, new Function6(function));
        }

        #endregion

        #region Unregistering

        public void UnregisterFunction(string functionName)
        {
            FunctionsMap.Remove(functionName);
        }

        public void UnregisterFunction(IFunction function)
        {
            string name = null;
            foreach(KeyValuePair<string, IFunction> pair in FunctionsMap)
            {
                if (function == pair.Value)
                {
                    name = pair.Key;
                    break;
                }
            }
            if (name != null)
                FunctionsMap.Remove(name);
        }

        #endregion

        #region Getting

        public IFunction GetFunction(string name)
        {
            if (FunctionsMap.TryGetValue(name, out IFunction function))
                return function;
            return null;
        }

        #endregion
    }
}
