using System.Reflection.PortableExecutable;

namespace CrossLanguageCS.Helpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// An efficient version of String.IndexOf, with no checking and exception throwing (well, no
        /// deliberate exception throwing or checking. only possible exceptions are index out of bounds exceptions)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="character"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int FastIndexOf(this string value, char character, int startIndex = 0)
        {
            for(int i = startIndex, length = value.Length; i < length; i++)
            {
                if (value[i] == character)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the given character only if the character afer that (in the given string) is the given next character 
        /// <code>
        /// CharacterNextTo("ello there", 'h', 'e') returns 6
        /// </code>
        /// <code>
        /// CharacterNextTo("ello there", 'l', 'e') returns -1
        /// </code>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="character"></param>
        /// <param name="before"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int CharacterNextTo(this string value, char character, char next, int startIndex = 0)
        {
            for (int i = startIndex, length = value.Length; i < length; i++)
            {
                if (value[i] == character && value[++i] == next)
                    return i;
            }
            return -1;
        }
    }
}
