using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LabWorksLib
{
	public class LabTwo
	{
		public string InputFile = "Files/lab2/input.txt";

		private static int ConvertToInt32(string value)
		{
			bool parsed = Int32.TryParse(value, out int result);

			if (!parsed)
				throw new ArgumentException($"The value {value} was not parsed to int");

			return result;
		}

		public string Execute()
		{
			string strN = File.ReadAllText(InputFile);
			int N = ConvertToInt32(strN);

			if (!(1 <= N && N <= 1000))
			{
				throw new ArgumentException($"N must be >= 1 and <= 1000");
			}

			int res = (N + 1) * N / 2 + 1;

			return res.ToString();
		}
	}
}
