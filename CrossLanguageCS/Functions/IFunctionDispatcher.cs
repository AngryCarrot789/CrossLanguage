namespace CrossLanguageCS.Functions
{
    public interface IFunctionDispatcher
    {
        void DispatchFunction(string name);
        void DispatchFunction<T1>(string name, T1 a);
        void DispatchFunction<T1, T2>(string name, T1 a, T2 b);
        void DispatchFunction<T1, T2, T3>(string name,T1 a, T2 b, T3 c);
        void DispatchFunction<T1, T2, T3, T4>(string name,T1 a, T2 b, T3 c, T4 d);
        void DispatchFunction<T1, T2, T3, T4, T5>(string name,T1 a, T2 b, T3 c, T4 d, T5 e);
        void DispatchFunction<T1, T2, T3, T4, T5, T6>(string name,T1 a, T2 b, T3 c, T4 d, T5 e, T6 f);
    }
}
