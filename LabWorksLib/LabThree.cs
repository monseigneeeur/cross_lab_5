using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LabWorksLib
{
	public class LabThree
	{
		const string _inputFile = "Files/lab3/input.txt";
		const string _outputFile = "Files/lab3/output.txt";

		int n, m, counter = 0, x, y;
		int[] route = new int[0];
		int[] nodes = new int[0];
		int[] pairNodes = new int[0];
		int[] nextNode = new int[0];

		public void execute()
		{


			if (read())
			{
				if (solve())
					write();
			}


		}
		bool read()
		{
			if (File.Exists(_inputFile))
			{
				using (var reader = new StreamReader(_inputFile))
				{
					string line = reader.ReadLine();
					string[] pair = line.Split(' ');
					if (pair.Length != 2)
					{
						Console.WriteLine($"Wrong data in line '{line}'");
						return false;
					}
					n = int.Parse(pair[0]);
					m = int.Parse(pair[1]);
					route = new int[n * 2 + 2];
					pairNodes = new int[n * 2 + 2];
					nextNode = new int[n * 2 + 2];
					nodes = new int[n + 1];
					for (int i = 1; i <= m; i++)
					{
						line = reader.ReadLine();
						pair = line.Split(' ');
						if (pair.Length != 2)
						{
							Console.WriteLine($"Wrong data in line '{line}'");
							return false;
						}
						x = int.Parse(pair[0]);
						y = int.Parse(pair[1]);
						if (x > n || y > n || x < 1 || y < 1)
						{
							Console.WriteLine($"Wrong data in line '{line}'");
							return false;
						}
						pairNodes[i] = y;
						nextNode[i] = nodes[x];
						nodes[x] = i;

						pairNodes[i + m] = x;
						nextNode[i + m] = nodes[y];
						nodes[y] = i + m;
					}
				}
			}
			else
			{
				Console.Write("File INPUT.TXT doesn`t exist");
				return false;
			}
			return true;
		}
		bool dfs(int v)
		{
			int j = nodes[v];
			while (j != 0)
			{
				nodes[v] = 0;
				bool l = dfs(pairNodes[j]);
				if (!l)
				{
					return false;
				}
				j = nextNode[j];
			}
			counter++;
			if (counter >= route.Length)
			{
				Console.WriteLine("Wrong input data...");
				return false;
			}
			route[counter] = v;
			return true;
		}
		bool solve()
		{
			return dfs(1);
		}
		void write()
		{
			int step = counter - 1;
			string line = "";
			for (int i = counter; i >= 1; i--)
				line += " " + route[i];
			using (var writer = new StreamWriter(_outputFile))
			{
				Console.WriteLine(step);
				writer.WriteLine(step);
				Console.WriteLine(line.Trim());
				writer.WriteLine(line.Trim());
			}
		}


	}
}
