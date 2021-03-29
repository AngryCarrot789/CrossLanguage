using System;
using System.Collections.Generic;
using System.Text;

namespace CrossLanguageCS.Functions
{
    public abstract class FunctionProcessor
    {
        protected FunctionTable Table;
        protected ParameterParser Parser;

        private List<string> InboundFunctionParameters;

        public FunctionProcessor()
        {
            Table = new FunctionTable();
            Parser = new ParameterParser();

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
        }
    }
}
