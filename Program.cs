using System;
using System.IO;

namespace GausModifine
{
		class Program
		{
			static int N = 3, M = N + 1;
			static double[,] mas = new double[N, M];
			static double[] x = new double[N];

			static void Swap(int x, int y, double[,] mas)
			{
				double temp;
				for (int i = 0; i < M; i++)
				{
					temp = mas[x, i];
					mas[x, i] = mas[y, i];
					mas[y, i] = temp;
				}
			}

			static void Print()
			{
				Console.WriteLine();
				for (int i = 0; i < N; i++)
				{
					for (int j = 0; j < M; j++)
						Console.Write("{0,6:0.00} ", mas[i, j]);
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			static int Check(double[,] mas)
			{
				for (int i = 0; i < N; i++)
					if (mas[i, i] == 0)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write("\nМатрица вырожденая! Решение невозможно!");
						Console.ResetColor();
						return 1;
					}
				return 0;
			}

			static int Gauss(double[,] mas)
			{
				double temp = 0;
				for (int z = 0; z < N - 1; z++)
					for (int i = z + 1; i < N; i++)
					{
						temp = -mas[i, z] / mas[z, z];
						mas[i, z] += mas[z, z] * temp;
						for (int j = z + 1; j < M; j++)
							mas[i, j] = mas[z, j] * temp + mas[i, j];
					}

				Console.WriteLine("Матрица после прямого хода:");
				Print();

				if (Check(mas) == 1)
					return 1;

				for (int i = N - 1; i >= 0; i--)
				{
					for (int j = N - 1; j > i; j--)
					{
						mas[i, j] *= x[j];
						mas[i, M - 1] -= mas[i, j];
					}
					x[i] = mas[i, M - 1] / mas[i, i];
				}
				return 0;
			}

			static int ModifGauss(double[,] mas)
			{
				int max;
				for (int i = 0; i < N; i++)
				{
					max = i;
					for (int j = i + 1; j < N; j++)
						if (Math.Abs(mas[j, i]) > Math.Abs(mas[max, i]))
							max = j;
					if (max != i)
						Swap(max, i, mas);
				}

				Console.WriteLine("\nМатрица после модификации:");
				Print();

				if (Gauss(mas) == 1)
					return 1;
				return 0;
			}

			static void Main()
			{
				string[] file = File.ReadAllLines("input.txt"); // считывание текстового файла
				string[] numbers;
				Console.WriteLine("Входная матрица:\n");
				for (int i = 0; i < N; i++)
				{
					numbers = file[i].Split(' '); // считывание строк и их разделение
					for (int j = 0; j < M; j++)
					{
						mas[i, j] = double.Parse(numbers[j]); // преобразование типа 
					Console.Write("{0,6:0.00}", mas[i,j]); // вывод массивы на экран
					}
					Console.WriteLine();
				}

				Console.WriteLine();
			int error = 0;
			Console.WriteLine("Выберите каким методом решить матрицу: \n 1-Гаус \n 2- Модифицированный метод \n 3-Выход");
			switch (Console.ReadLine())
			{
				case "1":
					Console.WriteLine("Решение матрицы методом Гауса");
					Console.WriteLine();
					error = Gauss(mas);
				break;
				case "2":
					Console.WriteLine("Решение матрицы методом модифицированного Гауса");
					error = ModifGauss(mas);
				break;
				case "3":
					Environment.Exit(0);
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Некоректные символы");
					Console.ReadKey();
					Environment.Exit(1);

					break;	
			}
			if (error == 0)
			{
				for (int i = 0; i < N; i++)
					Console.Write("x{0} = {1,6:0.00}\n", i + 1, x[i]);
			}
				Console.ReadKey();
			}
		}
}