using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrossLanguageCS.Functions
{
    /// <summary>
    /// A class with some helper functions for extracting functions names, and also 
    /// a system for registering functions which create specific data types
    /// </summary>
    public class ParameterParser
    {
        /// <summary>
        /// The character used to split the function name and the parameters
        /// </summary>
        public char FuncNameParamsSplitter;

        /// <summary>
        /// The character used to differentiate multiple parameters
        /// </summary>
        public char ParameterSplitter;

        /// <summary>
        /// The character which is used to encapsulate strings
        /// </summary>
        public char StringEncapsulator;

        private Dictionary<Type, Func<object, string>> ParameterCreators;

        public ParameterParser(char funcParamSplitter = ':', char paramsSplitter = ',', char stringEncapsulator = '\'')
        {
            ParameterCreators = new Dictionary<Type, Func<object, string>>();
            FuncNameParamsSplitter = funcParamSplitter;
            ParameterSplitter = paramsSplitter;
            StringEncapsulator = stringEncapsulator;
        }



        public void AppendParameter(string str, StringBuilder sb, ref int index)
        {
            char c = str[++index];
            while (c != ParameterSplitter)
            {
                sb.Append(c);
                c = str[++index];
            }
        }

        public KeyValuePair<string, string> GetNameParams(string input)
        {
            int splitIndex = input.IndexOf(FuncNameParamsSplitter);
            if (splitIndex == -1)
                return default;

            string functionName = input.Substring(0, splitIndex);
            string parameters = input.Substring(splitIndex + 1);
            return new KeyValuePair<string, string>(functionName, parameters);
        }

        public static bool IsPrimitiveType(char c)
        {
            return c == 'i' || c == 'd' || c == 'b' || c == 's';
        }
    }
}
