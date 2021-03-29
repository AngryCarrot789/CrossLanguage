using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using CrossLanguageCS.Exceptions;

namespace CrossLanguageCS.Functions
{
    /// <summary>
    /// A class with some helper functions for extracting functions names, and also 
    /// a system for registering functions which create specific data types
    /// </summary>
    public class ParameterCreator
    {
        /// <summary>
        /// The character used to split the function name and the parameters
        /// </summary>
        public char FuncNameParamsSplitter;

        /// <summary>
        /// The character used to differentiate multiple parameters
        /// </summary>
        public char ParamSplitter;

        /// <summary>
        /// The character which is used to encapsulate strings
        /// </summary>
        public char StringEncapsulator;

        /// <summary>
        /// The characere which should go before <see cref="StringEncapsulator"/> 
        /// value if the character after that is equal to <see cref="ParamSplitter"/>,
        /// in order to stop the entire parser from breaking
        /// </summary>
        public char StringEncapsulatorEndCanceller;

        private Dictionary<Type, Func<object, string>> ParameterCreators;

        private StringBuilder ParameterBuilder;

        public ParameterCreator(char funcParamSplitter = ':', char paramsSplitter = '|', char stringEncapsulator = '\'', char stringEncapsulatorCancel = '\\')
        {
            ParameterCreators = new Dictionary<Type, Func<object, string>>();
            ParameterBuilder = new StringBuilder(256);
            FuncNameParamsSplitter = funcParamSplitter;
            ParamSplitter = paramsSplitter;
            StringEncapsulator = stringEncapsulator;
            StringEncapsulatorEndCanceller = stringEncapsulatorCancel;
        }

        /// <summary>
        /// Extracts all of the primitive data types from the string
        /// </summary>
        /// <param name="paramsList"></param>
        /// <param name="fullParameters"></param>
        public void DeserialiseAndAppendParameters(List<object> paramsList, string fullParameters)
        {
            if (fullParameters.Length < 2)
                return;

            for(int i = 0; i < fullParameters.Length; i++)
            {
                char c = fullParameters[i];
                if (c == 'i')
                {
                    ExtractParameter(ParameterBuilder, fullParameters, ref i);
                    paramsList.Add(int.Parse(ParameterBuilder.ToString()));
                    ParameterBuilder.Clear();
                }
                else if (c == 'd')
                {
                    ExtractParameter(ParameterBuilder, fullParameters, ref i);
                    paramsList.Add(double.Parse(ParameterBuilder.ToString()));
                    ParameterBuilder.Clear();
                }
                else if (c == 'b')
                {
                    ExtractParameter(ParameterBuilder, fullParameters, ref i);
                    paramsList.Add(bool.Parse(ParameterBuilder.ToString()));
                    ParameterBuilder.Clear();
                }
                else if (c == 's')
                {
                    // this will allow almost any conbination of string
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
                        paramsList.Add(ParameterBuilder.ToString());
                        ParameterBuilder.Clear();

                        // at this point, fullParameters[i] should be '
                        // so increase by 1 to get to the splitter, and the for loop will increase it again
                        // to get to the next parameter
                        i++;
                    }
                }
            }
        }

        private void ExtractParameter(StringBuilder sb, string fullParams, ref int index)
        {
            char c = fullParams[++index];
            int indexLength = fullParams.Length - 1;
            while (c != ParamSplitter)
            {
                sb.Append(c);
                if (index == indexLength)
                    break;
                c = fullParams[++index];
            }
        }

        public string SerialiseParameters<T1>(T1 p1)
        {
            return SerialiseType(p1);
        }

        public string SerialiseParameters<T1, T2>(T1 p1, T2 p2)
        {
            return new StringBuilder(64).
                Append(SerialiseType(p1)).Append(ParamSplitter).
                Append(SerialiseType(p2)).ToString();
        }

        public string SerialiseParameters<T1, T2, T3>(T1 p1, T2 p2, T3 p3)
        {
            return new StringBuilder(64).
                Append(SerialiseType(p1)).Append(ParamSplitter).
                Append(SerialiseType(p2)).Append(ParamSplitter).
                Append(SerialiseType(p3)).ToString();
        }

        public string SerialiseParameters<T1, T2, T3, T4>(T1 p1, T2 p2, T3 p3, T4 p4)
        {
            return new StringBuilder(64).
                Append(SerialiseType(p1)).Append(ParamSplitter).
                Append(SerialiseType(p2)).Append(ParamSplitter).
                Append(SerialiseType(p3)).Append(ParamSplitter).
                Append(SerialiseType(p4)).ToString();
        }

        public string SerialiseParameters<T1, T2, T3, T4, T5>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {
            return new StringBuilder(64).
                Append(SerialiseType(p1)).Append(ParamSplitter).
                Append(SerialiseType(p2)).Append(ParamSplitter).
                Append(SerialiseType(p3)).Append(ParamSplitter).
                Append(SerialiseType(p4)).Append(ParamSplitter).
                Append(SerialiseType(p5)).ToString();
        }


        public string SerialiseParameters<T1, T2, T3, T4, T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
        {
            return new StringBuilder(128).
                Append(SerialiseType(p1)).Append(ParamSplitter).
                Append(SerialiseType(p2)).Append(ParamSplitter).
                Append(SerialiseType(p3)).Append(ParamSplitter).
                Append(SerialiseType(p4)).Append(ParamSplitter).
                Append(SerialiseType(p5)).Append(ParamSplitter).
                Append(SerialiseType(p6)).ToString();
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
                    if (fullParameters[j + 1] == ParamSplitter)
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

        public KeyValuePair<string, string> SplitNameAndParameters(string input)
        {
            int splitIndex = input.IndexOf(FuncNameParamsSplitter);
            if (splitIndex == -1)
                return default;

            string functionName = input.Substring(0, splitIndex);
            string parameters = input.Substring(splitIndex + 1);
            return new KeyValuePair<string, string>(functionName, parameters);
        }

        public static string SerialiseType<T>(T t)
        {
            if (t is int)
                return SerialiseInteger(t.ToString());
            if (t is double)
                return SerialiseDouble(t.ToString());
            if (t is bool)
                return SerialiseBoolean(t.ToString());
            if (t is string)
                return SerialiseString(t.ToString());

            throw new UnsupportedTypeException(t.GetType());
        }

        public static string SerialiseInteger(string a)
        {
            return $"i{a}";
        }

        public static string SerialiseDouble(string a)
        {
            return $"d{a}";
        }

        public static string SerialiseBoolean(string a)
        {
            return $"b{a}";
        }

        public static string SerialiseString(string a)
        {
            return $"s'{a}'";
        }

        public static bool IsPrimitiveType(char c)
        {
            return c == 'i' || c == 'd' || c == 'b' || c == 's';
        }
    }
}
