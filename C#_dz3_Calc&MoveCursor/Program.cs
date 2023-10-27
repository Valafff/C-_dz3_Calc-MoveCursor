#define CALCULATOR
//#define MovingCursor

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace C__dz3_Calc_MoveCursor
{
	internal class Program
	{
		static void Main(string[] args)
		{


#if CALCULATOR
			ConsoleKeyInfo key;
			string expression = "";
			bool dot_is = false;
			bool operator_is = false;
			string operand_1 = "", operand_2 = "", operator_1 = "";

			double a = 0, b = 0;
			double result = 0;

			List<char> numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			List<char> operators = new List<char> { '+', '-', '*', '/', '%', '.', ',' };
            //List<char> numbers_and_operands = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/', '%' };
            Console.WriteLine("Для ввода отрицательных значений после ввода числа нажмите \"m\"");
            Console.Write("Введите выражение: ");
			do
			{
				key = Console.ReadKey(true);

				if (!operator_is && key.KeyChar == 'm')
				{
					operand_1 = operand_1.Insert(0, "-");
					expression = operand_1;
					Console.CursorTop = 1;
					Console.CursorLeft = 0;
					Console.Write("Введите выражение: " + expression);
				}
				else if (operator_is && key.KeyChar == 'm')
				{
					operand_2 = operand_2.Insert(0, "-");
					expression = operand_1+operator_1+operand_2;
					Console.CursorTop = 1;
					Console.CursorLeft = 0;
					Console.Write("Введите выражение: " + expression);
				}

				for (int i = 0; i < numbers.Count; i++)
				{
					if (key.KeyChar == numbers[i])
					{
						Console.Write(key.KeyChar);
						expression += key.KeyChar;
						if (!operator_is)
						{
							operand_1 += key.KeyChar;
						}
						else
						{
							operand_2 += key.KeyChar;
						}
					}
				}
				// Проверка на операторы и .
				for (int j = 0; j < operators.Count; j++)
				{
					if (key.KeyChar == operators[j])
					{

						if (operator_is && key.KeyChar != '.')
						{
							break;
						}
						if (key.KeyChar != '.' && key.KeyChar != '\b')
						{
							operator_is = true;
						}


						//if ("+-*/%" == expression.Substring(expression.Length - 1))
						//{
						//	operator_is = true;
						//}
						//else
						//{
						//	operator_is = false;
						//}

						//Условия пропуска символа
						if (key.KeyChar == '.' && expression.Count() == 0 || Convert.ToChar(expression.Substring(expression.Length - 1)) == key.KeyChar || (dot_is && key.KeyChar == '.'))
						{
							break;
						}

						Console.Write(key.KeyChar);
						expression += key.KeyChar;
						if (!operator_is && key.KeyChar == '.')
						{
							operand_1 += key.KeyChar;
						}
						else if (operator_is && key.KeyChar == '.')
						{
							operand_2 += key.KeyChar;
						}

						if (key.KeyChar != '.')
						{ 
							operator_1 = Convert.ToString (key.KeyChar);
						}

						dot_is = false;
						if (key.KeyChar == '.')
						{
							dot_is = true;
						}

					}
				}
				//Стирашка
				if (key.KeyChar == '\b')
				{
					Console.Write("\b \b");
					if (expression.Count() != 0)
					{
						expression = expression.Remove(expression.Count() - 1);
					}
					if (!operator_is && operand_1.Length != 0)
					{
						operand_1 = operand_1.Remove(operand_1.Count() - 1);
					}
					else if (operator_is && operand_2.Length != 0)
					{
						operand_2 = operand_2.Remove(operand_2.Count() - 1);
					}
				}
				if (key.KeyChar == 13)
				{
					break;
				}

			} while (key.KeyChar != 27);


			if (key.KeyChar == 13)
			{
				operand_1 = operand_1.Replace('.', ',');
				operand_2 = operand_2.Replace('.', ',');

				//Console.WriteLine("\n" + operand_1);
				//Console.WriteLine(operand_2);
				//Console.WriteLine(operator_1);
				{
					
					a = Convert.ToDouble(operand_1);
					b = Convert.ToDouble(operand_2);
					
				}

				if (operator_1 == "+")
				{
					result = a + b;
				}
				else if (operator_1 == "-")
				{
					result = a - b;
				}
				else if (operator_1 == "*")
				{
					result = a * b;	
				}
				else if(operator_1 == "/" && b!=0)
				{
					result = a / b;
				}
				else if (operator_1 == "%")
				{
					result = a % b;
				}
				else if (operator_1 == "/" && b == 0)
				{
					Console.WriteLine("\nОшибка! Деление на 0!");
					result = -1;
				}


				Console.WriteLine("\nРезультат операции: " + expression + "=" + result);

			}


#endif



#if MovingCursor

			char symbol = '+';
			int Y = 11, X = 11;
			int frame_size = 25;
			char frame = '#';

			void printCursor(int arg_X, int arg_Y, char arg_symbol, int new_arg_X, int new_arg_Y)
			{

				if (new_arg_X == 0)
				{
					X = 1;
					new_arg_X = 1;
					Console.Beep(1000, 300);
				}
				if (new_arg_Y == 0)
				{
					Y = 1;
					new_arg_Y = 1;
					Console.Beep(1000, 300);
				}
				if (new_arg_X >= frame_size-1)
				{
					X = frame_size - 2;
					new_arg_X = frame_size - 2;
					Console.Beep(1000, 300);
				}
				if (new_arg_Y >= frame_size-1)
				{
					Y = frame_size - 2;
					new_arg_Y = frame_size - 2;
					Console.Beep(1000, 300);
				}

				Console.CursorLeft = arg_X; Console.CursorTop = arg_Y;
				Console.Write(' ');
				Console.CursorLeft = new_arg_X; Console.CursorTop = new_arg_Y;
				Console.Write(arg_symbol);
			}



			for (int i = 0; i < frame_size; i++)
			{
				for (int j = 0; j < frame_size; j++)
				{
					if (i == 0 || i == frame_size - 1 || j == 0 || j == frame_size - 1)
					{
						Console.Write(frame);
					}
					else
					{
						Console.Write(' ');
					}
				}
				Console.WriteLine();
			}

			printCursor(X, Y, symbol, X, Y);

			ConsoleKey key = ConsoleKey.Spacebar;
			Console.CursorVisible = false;

			printCursor(X, Y, symbol, X, Y);
			do
			{

					key = Console.ReadKey(true).Key;

				if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
				{
					printCursor(X, Y, symbol, X, --Y);
				}
				else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
				{
					printCursor(X, Y, symbol, X, ++Y);
				}
				else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
				{
					printCursor(X, Y, symbol, ++X, Y);
				}
				else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
				{
					printCursor(X, Y, symbol, --X, Y);
				}
			}
			while ((key != ConsoleKey.Escape));
#endif

		}
	}
}
