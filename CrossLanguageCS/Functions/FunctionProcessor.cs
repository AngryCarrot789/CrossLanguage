using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguageCS.Functions
{
    public abstract class FunctionProcessor
    {
        private FunctionTable Table;
        private ParameterParser Parser;

        public FunctionProcessor()
        {
            Table = new FunctionTable();
            Parser = new ParameterParser();
        }

        /// <summary>
        /// Called when a function is received with the given name and serialised parameters which will be parsed later
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public void OnFunctionReceived(string name, string parameters)
        {

        }
    }
}
