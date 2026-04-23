using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Common
{
    public static partial class TextTransformer
    {
		public static class HexText
		{
			static public String To(String text)
			{
				return String.Join(" ", Encoding.UTF8.GetBytes(text).Select(value => value.ToString("x2").ToUpper()).ToArray());
			}


			static public String From(String text)
			{
				return Encoding.UTF8.GetString(text.Split(' ').Select(c => Convert.ToByte(c, 16)).ToArray());
			}
		}
	}
}
