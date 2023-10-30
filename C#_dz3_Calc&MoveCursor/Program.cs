//#define CALCULATOR
//#define MovingCursor
#define CALCULATOR_LONG_EXPRESSION

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;


namespace C__dz3_Calc_MoveCursor
{
	internal class Program
	{
		static void Main(string[] args)
		{
			NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
			nfi.NumberGroupSeparator = ".";
			nfi.NumberDecimalSeparator = ".";
			//Console.WriteLine(nfi.NumberGroupSeparator);

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
				//operand_1 = operand_1.Replace('.', ',');
				//operand_2 = operand_2.Replace('.', ',');

				//Console.WriteLine("\n" + operand_1);
				//Console.WriteLine(operand_2);
				//Console.WriteLine(operator_1);
				{
					
					a = Convert.ToDouble(operand_1, nfi);
					b = Convert.ToDouble(operand_2, nfi);
					
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

#if CALCULATOR_LONG_EXPRESSION

			Console.Write("Введите выражение: ");

			string expression = "";
			bool symbol_is = false;
			char last_key=' ';
			//string expression = Console.ReadLine();

			ConsoleKeyInfo key;

			List<char> numbers_and_operands = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/', '%', '.', ',' };
			Console.WriteLine("Введите выражение: ");
			do
			{
				key = Console.ReadKey(true);

				for (int i = 0; i < numbers_and_operands.Count; i++)
				{
					if (key.KeyChar == '+' && last_key == '+'/*&& expression.Contains('+')*/)
					{
						break;
					}
					if (key.KeyChar == numbers_and_operands[i])
					{
						Console.Write(key.KeyChar);
						expression += key.KeyChar;
						last_key = key.KeyChar;

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
				}
				if (key.KeyChar == 13)
				{
					break;
				}
			} while (true);

			if (expression.Length == 0)
			{
				expression = "0";
			}

			string original_expression = expression;
			
			//отcечение мусора
			bool trash = true;
			bool take_minus = false;
			do
			{
				if (expression[expression.Length - 1] == '+'
	|| expression[expression.Length - 1] == '-'
	|| expression[expression.Length - 1] == '*'
	|| expression[expression.Length - 1] == '/'
	|| expression[expression.Length - 1] == '%'
	|| expression[expression.Length - 1] == '.'
	|| expression[expression.Length - 1] == ',')
				{
					expression = expression.Remove(expression.Length - 1);
				}
				else if (expression[0] == '+'
					|| expression[0] == '*'
					|| expression[0] == '/'
					|| expression[0] == '%'
					|| expression[0] == '.'
					|| expression[0] == ',')
				{
					expression = expression.Remove(0,1);
				}
				else if (expression[0] == '-')
				{
					expression = expression.Remove(0, 1);
					take_minus = true;
				}
                else
                {
                    trash= false; 
                }
            } while (trash);

			string temp_number;
			int last_index;
			int first_index;
			if (take_minus)
			{
				expression = expression.Replace("-", "_");
				//last_index = expression.LastIndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
				//temp_number = expression.Substring(0, last_index);
				//multiple_elements.Add(Convert.ToDouble(temp_number, nfi));
			}

			do
			{
				if (expression.Contains("--"))
				{
					first_index = expression.IndexOf("--");
					expression = expression.Remove(expression.IndexOf("--"), 2);
					expression = expression.Insert(first_index, "-_");
				}
			} while (expression.Contains("--"));
			do
			{
				if (expression.Contains("+-"))
				{
					first_index = expression.IndexOf("+-");
					expression = expression.Remove(expression.IndexOf("+-"), 2);
					expression = expression.Insert(first_index, "+_");
				}
			} while (expression.Contains("+-"));

			do
			{
				if (expression.Contains("*-"))
				{
					first_index = expression.IndexOf("*-");
					expression = expression.Remove(expression.IndexOf("*-"), 2);
					expression = expression.Insert(first_index, "*_");
				}
			} while (expression.Contains("*-"));

			do
			{
				if (expression.Contains("/-"))
				{
					first_index = expression.IndexOf("/-");
					expression = expression.Remove(expression.IndexOf("/-"), 2);
					expression = expression.Insert(first_index, "/_");
				}
			} while (expression.Contains("/-"));
			do
			{
				if (expression.Contains("%-"))
				{
					first_index = expression.IndexOf("%-");
					expression = expression.Remove(expression.IndexOf("%-"), 2);
					expression = expression.Insert(first_index, "%_");
				}
			} while (expression.Contains("%-"));




			// причуды с привением типов
			expression = expression.Replace(',', '.');

			string[] values = expression.Split('+', '-', '*', '/', '%');
			double[] numbers = new double[values.Length];
			//Перевод на минус
			List<int> multiple_elements = new List<int>();
			for (int i = 0; i < numbers.Length; i++)
			{
				if (values[i].Contains("_"))
				{
					values[i] = values[i].Replace("_", "");
					multiple_elements.Add(i);
				}
			}

			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = Convert.ToDouble(values[i], nfi);

				//numbers[i] = Double.Parse(negativeNumber, CultureInfo.InvariantCulture);
				//numbers[i] = Double.Parse(values[i], CultureInfo.InvariantCulture);
			}
			// Исключение пустых вхождений;
			char[] separators = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ',' };
			string[] operators = expression.Split(separators, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < operators.Length; i++)
			{
				if (operators[i].Contains("_"))
				{
					operators[i] = operators[i].Replace("_", "");
				}
			}

			// Преобразование массива к листу https://ru.stackoverflow.com/questions/414655/Пожалуйста-приведите-пример-кода-удаления-элемента-из-массива-по-номеру-Номер
			var list_numbers = new List<double>(numbers);
			var list_operators = new List<string>(operators);

			for (int i = 0; i < list_numbers.Count; i++)
			{
				for (int j = 0; j < multiple_elements.Count; j++)
				{
					if (i == multiple_elements[j])
					{
						list_numbers[i] *= -1;
					}
				}
			}

			//Перевод в отрицательные числа
			//for (int i = 0; i < multiple_elements.Count; i++)
			//{
			//	list_numbers[multiple_elements[i]] *= -1;
			//}

			// Как тут работают указатели*??? Почему нельзя записать string *arg_operators
			void calculation(char arg_operators)
			{
				int i = 0;
				void dell_element()
				{
					list_operators.RemoveAt(i);
					list_numbers.RemoveAt(i + 1);
					i--;
				}
				if (list_operators.Count != 0)
				{
					do
					{
						if (list_operators[i] == Convert.ToString(arg_operators) && arg_operators == '*')
						{
							list_numbers[i] = list_numbers[i] * list_numbers[i + 1];
							dell_element();
						}
						else if (list_operators[i] == Convert.ToString(arg_operators) && arg_operators == '/')
						{
							if (list_numbers[i + 1] != 0)
							{
								list_numbers[i] = list_numbers[i] / list_numbers[i + 1];
							}
							else
							{
								Console.WriteLine("Деление на \"0\"!");
								list_numbers[i] = list_numbers[i] / list_numbers[i + 1];
							}

							dell_element();
						}
						else if (list_operators[i] == Convert.ToString(arg_operators) && arg_operators == '+')
						{
							list_numbers[i] = list_numbers[i] + list_numbers[i + 1];
							dell_element();
						}
						else if (list_operators[i] == Convert.ToString(arg_operators) && arg_operators == '-')
						{
							list_numbers[i] = list_numbers[i] - list_numbers[i + 1];
							dell_element();
						}
						else if (list_operators[i] == Convert.ToString(arg_operators) && arg_operators == '%')
						{
							list_numbers[i] = list_numbers[i] % list_numbers[i + 1];
							dell_element();
						}
						i++;
						if (i == list_operators.Count)
						{
							break;
						}
					} while (true);
				}
			}
			calculation('*');
			calculation('/');
			calculation('%');
			calculation('+');
			calculation('-');

			// Проверка вывода операторов и операндов
			//foreach (double i in list_numbers) Console.Write(i + " ");
			//Console.WriteLine();
			//foreach (string i in list_operators) Console.Write(i + " ");
			//Console.WriteLine();
			Console.WriteLine("\nРезультат вычисления выражения " + original_expression + " = " + list_numbers[0]);



			/*double a = Convert.ToDouble(values[0]);
			double b = Convert.ToDouble(values[1]);

			if (expression.Contains('+')) Console.WriteLine($"{a} + {b} = {a + b}");
			else if (expression.Contains('-')) Console.WriteLine($"{a} - {b} = {a - b}");
			else if (expression.Contains('*')) Console.WriteLine($"{a} * {b} = {a * b}");
			else if (expression.Contains('/')) Console.WriteLine($"{a} / {b} = {a / b}");
			else Console.WriteLine("Нэт такой животный...");*/

#endif
		}

	}
}
