using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguageCS.Functions
{
    /// <summary>
    /// A class that contains a reference to functions based on their name, and allows you to get, set and invoke them
    /// </summary>
    public class FunctionTable
    {
        public readonly Dictionary<string, IFunction> FunctionsMap;

        public FunctionTable()
        {
            FunctionsMap = new Dictionary<string, IFunction>();
        }

        #region Registering functions

        public void RegisterFunction(string functionName, Action function)
        {
            FunctionsMap.Add(functionName, new Function(function));
        }

        public void RegisterFunction<T1>(string functionName, Action<T1> function)
        {
            FunctionsMap.Add(functionName, new Function<T1>(function));
        }

        public void RegisterFunction<T1, T2>(string functionName, Action<T1, T2> function)
        {
            FunctionsMap.Add(functionName, new Function<T1, T2>(function));
        }

        public void RegisterFunction<T1, T2, T3>(string functionName, Action<T1, T2, T3> function)
        {
            FunctionsMap.Add(functionName, new Function<T1, T2, T3>(function));
        }

        public void RegisterFunction<T1, T2, T3, T4>(string functionName, Action<T1, T2, T3, T4> function)
        {
            FunctionsMap.Add(functionName, new Function<T1, T2, T3, T4>(function));
        }

        public void RegisterFunction<T1, T2, T3, T4, T5>(string functionName, Action<T1, T2, T3, T4, T5> function)
        {
            FunctionsMap.Add(functionName, new Function<T1, T2, T3, T4, T5>(function));
        }

        public void RegisterFunction<T1, T2, T3, T4, T5, T6>(string functionName, Action<T1, T2, T3, T4, T5, T6> function)
        {
            FunctionsMap.Add(functionName, new Function<T1, T2, T3, T4, T5, T6>(function));
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
