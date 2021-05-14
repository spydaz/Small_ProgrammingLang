using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.VisualBasic;

namespace SAL_VM.STACK_VM
{
    public static class Ext
    {
        public static IEnumerable<string> SplitAtNewLine(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string ExtractLastChar(this ref string InputStr)
        {
            string ExtractLastCharRet = default;
            ExtractLastCharRet = Strings.Right(InputStr, 1);
            return ExtractLastCharRet;
        }

        public static string ExtractFirstChar(this ref string InputStr)
        {
            string ExtractFirstCharRet = default;
            ExtractFirstCharRet = Strings.Left(InputStr, 1);
            return ExtractFirstCharRet;
        }
        /// <summary>
        /// Rule for tagging text
        /// </summary>
        public class GrammarRule
        {
            /// <summary>
            /// Serializes object to json
            /// </summary>
            /// <returns> </returns>
            public string ToJson()
            {
                var Converter = new JavaScriptSerializer();
                return Converter.Serialize(this);
            }

            public List<string> ComponentStrings;
            public string TagString;

            public GrammarRule()
            {
                ComponentStrings = new List<string>();
            }
        }
        /// <summary>
        /// AbstractSyntax Basic TOKEN
        /// </summary>
        public struct Token
        {
            public string ToJson()
            {
                var Converter = new JavaScriptSerializer();
                return Converter.Serialize(this);
            }

            public string Name;
            public string Value;
        }

        public struct AbstractSyntaxToken
        {
            public string ToJson()
            {
                var Converter = new JavaScriptSerializer();
                return Converter.Serialize(this);
            }

            public string Name;
            public List<Token> Value;
        }
    }
}