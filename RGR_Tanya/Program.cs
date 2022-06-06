using System;

namespace RGR_Tanya
{
    class Program
    {

        static int Is_P_ichnoe(ref string Chislo, int index = 0)
        {
            string DopustimieSimvoli = "012";
            Chislo = Chislo.Replace('.', ',');
            if (Chislo.Length == 0) return index;
            if (Chislo[index] == '-') index++;
            int count_sep = 0;
            while (index < Chislo.Length) 
            {
                if (Chislo[index] == ',' && index != 0)
                {
                    if (count_sep == 0)
                    {
                        if (Chislo[0] == '-')
                        {
                            if (index > 1)
                            {
                                index++;
                                count_sep = 1;
                                continue;
                            }
                        }
                        else
                        {
                            index++;
                            count_sep = 1;
                            continue;
                        }
                    }
                }
                if (DopustimieSimvoli.IndexOf(Chislo[index]) != -1) index++;
                else return index;
            }
            return -1;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Раздел «Калькулятор p – ичных чисел»");
            Console.WriteLine("Спроектируйте и реализуйте приложение, выполняющее арифметические операции над p - ичными числами");
            Console.WriteLine("Система счисления p = 3");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Пожалуйста, вводите числа в 3-ичной системе счисления!\n");
                string num1;
                string num2;
                while (true)
                {
                    Console.Write("Введите первое число:    ");
                    num1 = Console.ReadLine();
                    int index = Is_P_ichnoe(ref num1);
                    if (index != -1)
                    {
                        Console.WriteLine("Это не число в 3-ичной системе счисления, ошибка в символе с номером {0}",index+1);
                        Console.WriteLine("Повторите попытку...\n");
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.Write("Введите второе число:    ");
                    num2 = Console.ReadLine();
                    int index = Is_P_ichnoe(ref num2);
                    if (index != -1)
                    {
                        Console.WriteLine("Это не число в 3-ичной системе счисления, ошибка в символе с номером {0}", index + 1);
                        Console.WriteLine("Повторите попытку...\n");
                        continue;
                    }
                    break;
                }

                Console.WriteLine("Выберете операцию которую вы хотите провести над числами:");
                string operacia = "asdf";
                string DostupnieOperacii = "+-*/";
                while (true)
                {
                    Console.WriteLine("Доступные операции:    + - * /");
                    operacia = Console.ReadLine();
                    if (DostupnieOperacii.IndexOf(operacia) == -1 || operacia == "")
                    {
                        Console.WriteLine("Выбрана не корректная операция");
                        Console.WriteLine("Повторите попытку...\n");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("\n\n==============================================");
                Console.WriteLine("Проверьте корректность введённых данных:");
                Console.WriteLine("Первое число:    {0}\nВторое число:    {1}\nОперация:    {2}",num1,num2,operacia);
                Console.WriteLine("==============================================\n");

                string num1_v10 = Perevod_V10(num1);// Первое число в 10-ичной системе счисления в формате строки
                double num1_v10_doub = double.Parse(num1_v10);// Первое число в 10-ичной системе счисления типа дабл


                string num2_v10 = Perevod_V10(num2);// Второе число в 10-ичной системе счисления в формате строки
                double num2_v10_doub = double.Parse(num2_v10);// Второе число в 10-ичной системе счисления типа дабл
                string result;
                if(operacia == "+")
                {
                    result = Convert.ToString(num1_v10_doub + num2_v10_doub);
                }
                else if (operacia == "-")
                {
                    result = Convert.ToString(num1_v10_doub - num2_v10_doub);
                }
                else if(operacia == "*")
                {
                    result = Convert.ToString(num1_v10_doub * num2_v10_doub);
                }
                else
                {
                    if (num2_v10_doub != 0) result = Convert.ToString(num1_v10_doub / num2_v10_doub);
                    else
                    {
                        Console.WriteLine("На 0 делить нельзя!\nПовторите попытку...\n");
                        continue;
                    }
                }

                string result_v3 = Perevod_V3(result);
                Console.WriteLine("Итог\n{0} {1} {2} = {3}", num1, operacia, num2, result_v3);
            }
        }
        static string Perevod_V10(string Chislo)// Переводит число из 3-ичной сс в 10-ичную сс
        {
            bool negativ = false;
            if (Chislo[0] == '-')
            {
                Chislo = Chislo.Substring(1);
                negativ = true;
            }

            string[] arr = Chislo.Split('.', ',');
            string Celoe_str = arr[0];
            string Drobnoe_str = "0";
            if (arr.Length > 1) Drobnoe_str = arr[1];
            double celoe_v10 = 0;
            double drob_v10 = 0;
            int index = 0;
            while (index < Celoe_str.Length)
            {
                //celoe_v10 += (Celoe_str[^(index+1)] - '0') * Math.Pow(3, index);
                celoe_v10 += (Celoe_str[Celoe_str.Length - 1 - index] - '0') * Math.Pow(3, index);
                index++;
            }
            index = -1;
            if (Drobnoe_str != "0")
            {
                while (Math.Abs(index) <= Drobnoe_str.Length)
                {
                    drob_v10 += (Drobnoe_str[Math.Abs(index)-1] - 48) * Math.Pow(3, index);
                    index--;
                }
            }
            double result = celoe_v10 + drob_v10;
            if (negativ) return Convert.ToString(-1 *result);
            return Convert.ToString(result);
        }
        static string Perevod_V3(string Chislo)// Переводит число из 10-ичной сс в 3-ичную сс
        {
            bool negativ = false;
            if (Chislo[0] == '-')
            {
                Chislo = Chislo.Substring(1);
                negativ = true;
            }

            string[] arr = Chislo.Split('.', ',');
            string result = "";
            int Celoe = int.Parse(arr[0]);
            double Drobnoe = 0;
            if (arr.Length > 1) Drobnoe = Convert.ToDouble("0," + arr[1]);
            int index = 0;
            while ( Celoe !=0)
            {
                result += Celoe % 3;
                Celoe /= 3;
                index++;
            }
            char[] ch_arr = result.ToCharArray();//Переворачивает получившееся число
            Array.Reverse(ch_arr);
            result = new string(ch_arr);
            result += ",";
            index = 0;

            if (Drobnoe != 0)
            {
                while (index <= 4)//ограничивает до 4 знаков после запятой
                {
                    Drobnoe *= 3;
                    if (Drobnoe >= 1)
                    {
                        string chislo = Convert.ToString(Drobnoe);
                        string[] s2 = chislo.Split(',');
                        result += s2[0];
                        if (s2.Length > 1) Drobnoe = Convert.ToDouble("0," + s2[1]);
                        else break;
                    }
                    else result += "0";
                    index++;
                }
            }
            double doubresult = double.Parse(result);
            result = Convert.ToString(doubresult);
            if (negativ) return "-" + result;
            return result;
        }
    }
}
