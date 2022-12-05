using System;
using System.IO;
using System.Linq;

namespace LabWorksLib
{
    public class LabOne
    {
        internal enum SideEnum
        {
            FRONT,
            LEFT,
            BACK,
            RIGHT,
            TOP,
            BOTTOM
        }

		public int execute()
		{
			const string inputFile = "Files/lab1/input.txt";
			//const string outputFile = "Files/lab1/output.txt";

			char[][][]? cube = null;

			char[][]? frontSide = null;
			char[][]? leftSide = null;
			char[][]? backSide = null;
			char[][]? rightSide = null;
			char[][]? topSide = null;
			char[][]? bottomSide = null;

			LoadCube();

			if (cube != null)
			{
				FormCube(cube.Length);
				int count = cube.SelectMany(x => x).SelectMany(x => x).Count(x => x != (char)'.');
				//using (var writer = new StreamWriter(outputFile, false))
				//writer.Write(count);
				return count;
				//Console.WriteLine("All done");
			}
			else
				return -1;
				//Console.WriteLine("Some error in input data");

			void LoadCube()
			{
				using (var reader = new StreamReader(inputFile))
				{
					string? sizeLine = reader.ReadLine();
					if (sizeLine != null)
					{
						if (byte.TryParse(sizeLine, out byte size))
						{
							InitArrays(size);
							LoadSideArrays(reader, size);
						}
					}
				}
			}

			void InitArrays(byte size)
			{
				cube = new char[size][][];
				frontSide = new char[size][];
				leftSide = new char[size][];
				backSide = new char[size][];
				rightSide = new char[size][];
				topSide = new char[size][];
				bottomSide = new char[size][];
				for (int i = 0; i < size; i++)
				{
					cube[i] = new char[size][];
					frontSide[i] = new char[size];
					leftSide[i] = new char[size];
					backSide[i] = new char[size];
					rightSide[i] = new char[size];
					topSide[i] = new char[size];
					bottomSide[i] = new char[size];
					for (int j = 0; j < size; j++)
						cube[i][j] = new char[size];
				}
			}

			void LoadSideArrays(StreamReader reader, int size)
			{
				for (int i = 0; i < size; i++)
				{
					string line = reader.ReadLine();
					string[] sideLines = line.Split(' ');
					for (int j = 0; j < size; j++)
					{
						frontSide[i][j] = sideLines[(int)SideEnum.FRONT][j];
						leftSide[i][j] = sideLines[(int)SideEnum.LEFT][j];
						backSide[i][j] = sideLines[(int)SideEnum.BACK][j];
						rightSide[i][j] = sideLines[(int)SideEnum.RIGHT][j];
						topSide[i][j] = sideLines[(int)SideEnum.TOP][j];
						bottomSide[i][j] = sideLines[(int)SideEnum.BOTTOM][j];
					}
				}
			}

			void FormCube(int size)
			{
				//fill empty blocks
				for (int x = 0; x < size; x++)
				{
					for (int y = 0; y < size; y++)
					{
						if (frontSide[x][y] == '.')
						{
							for (int z = 0; z < size; z++)
							{
								cube[x][y][z] = '.';
							}
						}
						if (leftSide[x][y] == '.')
						{
							for (int z = 0; z < size; z++)
							{
								cube[z][y][size - x - 1] = '.';
							}
						}
						if (topSide[x][y] == '.')
						{
							for (int z = 0; z < size; z++)
							{
								cube[x][z][size - y - 1] = '.';
							}
						}
					}
				}
			}


		}


	}
}
