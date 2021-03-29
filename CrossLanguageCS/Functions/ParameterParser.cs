using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CrossLanguageCS.Helpers;

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

        /// <summary>
        /// The characere which should go before <see cref="StringEncapsulator"/> 
        /// value if the character after that is equal to <see cref="ParameterSplitter"/>,
        /// in order to stop the entire parser from breaking
        /// </summary>
        public char StringEncapsulatorEndCanceller;

        private Dictionary<Type, Func<object, string>> ParameterCreators;

        private StringBuilder ParameterBuilder;

        public ParameterParser(char funcParamSplitter = ':', char paramsSplitter = '|', char stringEncapsulator = '\'', char stringEncapsulatorCancel = '\\')
        {
            ParameterCreators = new Dictionary<Type, Func<object, string>>();
            ParameterBuilder = new StringBuilder(256);
            FuncNameParamsSplitter = funcParamSplitter;
            ParameterSplitter = paramsSplitter;
            StringEncapsulator = stringEncapsulator;
            StringEncapsulatorEndCanceller = stringEncapsulatorCancel;
        }

        /// <summary>
        /// Extracts all of the primitive data types from the string
        /// </summary>
        /// <param name="paramsList"></param>
        /// <param name="fullParameters"></param>
        public void DeserialiseAndAppendParameters(List<string> paramsList, string fullParameters)
        {
            if (fullParameters.Length < 2)
                return;

            for(int i = 0; i < fullParameters.Length; i++)
            {
                char c = fullParameters[i];
                if (c == 'i' || c == 'd' || c == 'b')
                {
                    c = fullParameters[++i];
                    while (c != ParameterSplitter)
                    {
                        ParameterBuilder.Append(c);
                        c = fullParameters[++i];
                    }
                    paramsList.Add(ParameterBuilder.ToString());
                    ParameterBuilder.Clear();
                }
                else if (c == 's')
                {
                    // this will allow almost any conbination of string. however, if the parameter string contains 
                    c = fullParameters[++i];
                    if (c == StringEncapsulator)
                    {
                        //  |   <- current index index 
                        // s'hello 'mister\', you're late!',i22
                        int encapsulatorEndIndex = EncapsulatorEnd(fullParameters, ++i, out int lastEncapsulatorIndex);
                        if (encapsulatorEndIndex == -1)
                            encapsulatorEndIndex = lastEncapsulatorIndex;
                        // if lastEncapsulatorIndex == -1... something horrible has happened

                        while(i < encapsulatorEndIndex)
                        {
                            ParameterBuilder.Append(fullParameters[i++]);
                        }
                        // at this point, fullParameters[i] should be '
                        // so increase by 1 to get to the splitter, and the for loop will increase it again
                        // to get to the next parameter
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Finds the end of a string in <paramref name="fullParameters"/> 
        /// </summary>
        /// <param name="fullParameters"></param>
        /// <param name="i">The start index of the string the fullParameters (value[i] should not equal ' unless the string is empty)</param>
        /// <param name="lastEncapsulatorIndex">
        /// The last found index of a string encapsulator character, 
        /// used incase a string encapsulator canceller character is used before the actual end of a string by accident
        /// </param>
        /// <returns></returns>
        public int EncapsulatorEnd(string fullParameters, int i, out int lastEncapsulatorIndex)
        {
            lastEncapsulatorIndex = -1;
            // i == the start index into the actual string 
            for (int j = i, len = fullParameters.Length, indexLen = len + 1; j < len; j++)
            {
                // is it the encapsulator?
                if (fullParameters[j] == StringEncapsulator)
                {
                    lastEncapsulatorIndex = j;
                    // this is the end of the fullParameters, so either the last parameter was a string
                    // or it was the only parameter (and therefore the last ;) )
                    if (j == indexLen)
                        return j;
                    // is the next character a parameter splitter?
                    if (fullParameters[j + 1] == ParameterSplitter)
                    {
                        // this might just be someone going "hi 'mister', ok", if the splitter character is ,
                        // the ', should be the end of a string but what if it shouldn't be... so,
                        // make sure the characer before the encapsulator is NOT encapsulator end canceller
                        if (fullParameters[j - 1] != StringEncapsulatorEndCanceller)
                        {
                            return j;
                        }
                        // if it was, then its not the end of the string and coninute looking
                    }
                }
            }
            // something terrible has happened...
            return -1;
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
