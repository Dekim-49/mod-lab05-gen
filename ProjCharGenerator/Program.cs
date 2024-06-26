﻿using System;
using System.Collections.Generic;
using System.IO;

namespace generator
{
    public class CharGenerator_1
    {
        public string syms = "абвгдежзийклмнопрстуфхцчшщыьэюя";
        public char[] syms_array; //массив из символов
        public int count_syms; //количество букав

        //вероятности. макс = 84
        //0-2 - AA
        //3-14 - БА
        //15-49 - ВА и т.д.
        public int[,] veroyatnost =
           {{2,12,35,8,14,7,6,15,7,7,19,27,19,45,3,11,26,31,27,3,1,10,6,7,10,1,0,0,2,6,9},
           {5,0,0,0,0,9,1,0,6,0,0,6,0,2,21,0,8,1,0,6,0,0,0,0,0,1,11,0,0,0,2},
           {35,1,5,3,3,32,0,2,17,0,7,10,3,9,58,6,6,19,6,7,0,1,1,2,4,1,18,1,2,0,3},
           {7,0,0,0,3,3,0,0,5,0,1,5,0,1,50,0,7,0,0,2,0,0,0,0,0,0,0,0,0,0,0},
           {25,0,3,1,1,29,1,1,13,0,1,5,1,13,22,3,6,8,1,10,0,0,1,1,1,0,5,1,0,0,1},
           {2,9,18,11,27,7,5,10,6,15,13,35,24,63,7,16,39,37,33,3,1,8,3,7,3,3,0,0,1,1,2},
           {5,1,0,0,6,12,0,0,5,0,0,0,0,6,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
           {35,1,7,1,5,3,0,0,4,0,2,1,2,9,9,1,3,1,0,2,0,0,0,0,0,0,4,0,0,0,4},
           {4,6,22,5,10,21,2,23,19,11,19,21,20,32,8,13,11,29,29,3,1,17,3,11,1,1,0,0,1,3,17},
           {1,1,4,1,3,0,1,2,4,0,5,1,2,7,9,7,3,10,2,0,0,0,1,3,2,0,0,0,0,0,0},
           {24,1,4,1,0,4,1,1,26,0,1,4,1,2,66,2,10,3,7,10,0,0,1,0,0,0,0,0,0,0,0},
           {25,1,1,1,1,33,2,1,36,0,1,2,1,8,30,2,0,3,1,6,0,4,0,1,0,0,2,30,0,4,9},
           {18,2,4,1,1,21,1,2,23,0,3,1,3,7,19,5,2,5,3,9,1,0,0,2,0,0,5,1,1,0,3},
           {54,1,2,3,3,34,0,0,58,0,3,0,1,24,67,2,1,9,9,7,1,0,5,2,0,0,36,3,0,0,5},
           {1,28,84,32,47,15,7,18,12,29,19,41,38,30,9,18,43,50,39,3,2,5,2,12,4,3,0,0,2,3,2},
           {7,0,0,0,0,15,0,0,4,0,0,9,0,1,46,0,41,1,0,6,0,0,0,0,0,0,2,0,0,0,2},
           {55,1,4,4,3,37,3,1,24,0,3,1,3,7,56,2,1,5,9,16,0,1,1,1,2,0,8,3,0,0,5},
           {8,1,7,1,2,25,0,0,6,0,40,13,3,9,27,11,4,11,82,6,0,1,1,2,2,0,1,8,0,0,17},
           {35,1,27,1,3,31,0,1,28,0,5,1,1,11,56,4,26,18,2,10,0,0,0,1,0,0,20,21,0,0,4},
           {1,4,4,4,11,2,6,3,2,0,8,5,5,5,1,5,7,14,7,0,0,1,0,8,3,2,0,0,0,9,1},
           {2,0,0,0,0,2,0,0,2,0,0,0,0,0,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
           {4,1,4,1,3,1,0,2,3,0,4,3,3,4,18,5,3,4,2,2,1,0,0,1,0,0,0,0,0,0,0},
           {3,0,0,0,0,7,0,0,10,0,2,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0},
           {12,0,0,0,0,23,0,0,13,0,2,0,0,6,0,0,0,0,7,1,0,0,0,0,1,0,0,1,0,0,0},
           {5,0,0,0,0,11,0,0,14,0,1,2,0,2,2,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0},
           {3,0,0,0,0,8,0,0,6,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
           {0,1,9,1,3,12,0,2,4,7,3,6,6,3,2,10,3,9,4,1,0,16,0,1,2,0,0,0,0,0,0},
           {0,2,4,1,1,2,0,2,2,0,6,0,3,13,2,4,1,11,3,0,0,0,0,1,4,0,0,0,1,3,1},
           {0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1,9,0,0,0,0,0,0,0,0,0,0,0,0},
           {0,2,1,2,1,0,0,3,1,0,1,0,1,1,1,3,1,1,7,0,0,0,1,1,0,4,0,0,0,0,0},
           {1,3,9,1,3,3,1,5,3,2,3,3,4,6,3,6,3,6,10,0,0,2,1,4,1,1,0,0,1,1,1}
           };

        public int[,] verhnie_granici =
           {{2,12,35,8,14,7,6,15,7,7,19,27,19,45,3,11,26,31,27,3,1,10,6,7,10,1,0,0,2,6,9},
           {5,0,0,0,0,9,1,0,6,0,0,6,0,2,21,0,8,1,0,6,0,0,0,0,0,1,11,0,0,0,2},
           {35,1,5,3,3,32,0,2,17,0,7,10,3,9,58,6,6,19,6,7,0,1,1,2,4,1,18,1,2,0,3},
           {7,0,0,0,3,3,0,0,5,0,1,5,0,1,50,0,7,0,0,2,0,0,0,0,0,0,0,0,0,0,0},
           {25,0,3,1,1,29,1,1,13,0,1,5,1,13,22,3,6,8,1,10,0,0,1,1,1,0,5,1,0,0,1},
           {2,9,18,11,27,7,5,10,6,15,13,35,24,63,7,16,39,37,33,3,1,8,3,7,3,3,0,0,1,1,2},
           {5,1,0,0,6,12,0,0,5,0,0,0,0,6,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
           {35,1,7,1,5,3,0,0,4,0,2,1,2,9,9,1,3,1,0,2,0,0,0,0,0,0,4,0,0,0,4},
           {4,6,22,5,10,21,2,23,19,11,19,21,20,32,8,13,11,29,29,3,1,17,3,11,1,1,0,0,1,3,17},
           {1,1,4,1,3,0,1,2,4,0,5,1,2,7,9,7,3,10,2,0,0,0,1,3,2,0,0,0,0,0,0},
           {24,1,4,1,0,4,1,1,26,0,1,4,1,2,66,2,10,3,7,10,0,0,1,0,0,0,0,0,0,0,0},
           {25,1,1,1,1,33,2,1,36,0,1,2,1,8,30,2,0,3,1,6,0,4,0,1,0,0,2,30,0,4,9},
           {18,2,4,1,1,21,1,2,23,0,3,1,3,7,19,5,2,5,3,9,1,0,0,2,0,0,5,1,1,0,3},
           {54,1,2,3,3,34,0,0,58,0,3,0,1,24,67,2,1,9,9,7,1,0,5,2,0,0,36,3,0,0,5},
           {1,28,84,32,47,15,7,18,12,29,19,41,38,30,9,18,43,50,39,3,2,5,2,12,4,3,0,0,2,3,2},
           {7,0,0,0,0,15,0,0,4,0,0,9,0,1,46,0,41,1,0,6,0,0,0,0,0,0,2,0,0,0,2},
           {55,1,4,4,3,37,3,1,24,0,3,1,3,7,56,2,1,5,9,16,0,1,1,1,2,0,8,3,0,0,5},
           {8,1,7,1,2,25,0,0,6,0,40,13,3,9,27,11,4,11,82,6,0,1,1,2,2,0,1,8,0,0,17},
           {35,1,27,1,3,31,0,1,28,0,5,1,1,11,56,4,26,18,2,10,0,0,0,1,0,0,20,21,0,0,4},
           {1,4,4,4,11,2,6,3,2,0,8,5,5,5,1,5,7,14,7,0,0,1,0,8,3,2,0,0,0,9,1},
           {2,0,0,0,0,2,0,0,2,0,0,0,0,0,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
           {4,1,4,1,3,1,0,2,3,0,4,3,3,4,18,5,3,4,2,2,1,0,0,1,0,0,0,0,0,0,0},
           {3,0,0,0,0,7,0,0,10,0,2,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0},
           {12,0,0,0,0,23,0,0,13,0,2,0,0,6,0,0,0,0,7,1,0,0,0,0,1,0,0,1,0,0,0},
           {5,0,0,0,0,11,0,0,14,0,1,2,0,2,2,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0},
           {3,0,0,0,0,8,0,0,6,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
           {0,1,9,1,3,12,0,2,4,7,3,6,6,3,2,10,3,9,4,1,0,16,0,1,2,0,0,0,0,0,0},
           {0,2,4,1,1,2,0,2,2,0,6,0,3,13,2,4,1,11,3,0,0,0,0,1,4,0,0,0,1,3,1},
           {0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1,9,0,0,0,0,0,0,0,0,0,0,0,0},
           {0,2,1,2,1,0,0,3,1,0,1,0,1,1,1,3,1,1,7,0,0,0,1,1,0,4,0,0,0,0,0},
           {1,3,9,1,3,3,1,5,3,2,3,3,4,6,3,6,3,6,10,0,0,2,1,4,1,1,0,0,1,1,1}
           };

        public int summa = 0; //сумма всех вер-ей, высчитано в екселе

        public Random random = new Random();
        public CharGenerator_1()
        {
            count_syms = syms.Length;
            syms_array = syms.ToCharArray();
        }

        public void Ubrat_Nuli()
        {
            //так как нули на общем суммарном отрезке не выводятся,
            //мы добавляем во всей матрице +1.

            for (int i = 0; i < count_syms; i++)  
                for (int j = 0; j < count_syms; j++)
                {
                    veroyatnost[i, j] += 1;
                    summa += 1;
                }
                    
        }
        public void Diapazon_Summ()
        {
            //расчёт верхних границ диапазона чисел
            for (int i = 0; i < count_syms; i++)  //столбец
            {
                for (int j = 0; j < count_syms; j++)  //строка
                {
                    verhnie_granici[i, j] = 0; //очищаем матрицу

                    if (j == 0 && i == 0)
                    {
                        verhnie_granici[i, j] = veroyatnost[0, 0];
                    }
                    else if (j == 0)
                    {
                        verhnie_granici[i, j] = verhnie_granici[i - 1, count_syms - 1] + veroyatnost[i, j];
                    }
                    else verhnie_granici[i, j] = verhnie_granici[i, j - 1] + veroyatnost[i, j];

                    //Console.Write(verhnie_granici[i, j] + " ");
                }
                //Console.WriteLine();
            }
            summa = verhnie_granici[count_syms - 1, count_syms - 1];
        }

        public string getSym()
        {
            int random_symv = random.Next(0, summa);
            int i = 0;
            int j = 0;
            
            //ищем диапазон
            for (i = 0; i < count_syms-1; i++)
            {
                for (j = 0; j < count_syms-1; j++)
                {
                    if (random_symv < verhnie_granici[i,j])
                        break;
                }
                if (random_symv < verhnie_granici[i, j])
                    break;
            }
            string c = String.Concat(syms_array[j],syms_array[i]);
            return c;
            }

        public void sum()
        {
            Console.WriteLine(summa);
        }
    }

public class CharGenerator_2
    {
        public string syms = "абвгдежзийклмнопрстуфхцчшщыьэюя";
        public char[] syms_array; //массив из символов
        public int count_syms; //количество букав

        // массив слов
        public string[] slova = { "и","в","не","на","я","он",
            "быть","что","с","а","как","то","она","к","этот",
            "это","по","но","они","мы","свой","который","из",
            "весь","у","за","от","все","о","так","же","вы",
            "для","мочь","ты","год","один","его","тот","человек",
            "только","такой","бы","себя","сказать","еще","мой",
            "или","говорить","до","время","уже","когда","другой",
            "наш","да","если","знать","вот","сам","ни","день","дело",
            "при","стать","чтобы","самый","жизнь","очень","нет",
            "во","даже","два","рука","ее","первый","ли","под",
            "со","кто","где","новый","слово","какой","раз","теперь",
            "их","идти","без","после","иметь","там","ничто","должен",
            "большой","видеть","место","хотеть","можно","глаз" };
        //массив частоты использования слов
        public int[] chastota = { 14345348, 11335734, 6545609, 5737426, 
            4998377, 4947719, 4843591, 4675069, 4253772, 2815475, 2505731, 
            2261069, 2227685, 2185239, 2109612, 2097072, 2089119, 2062425, 
            1915969, 1772879, 1718580, 1703197, 1669273, 1616065, 1572078, 
            1487183, 1467970, 1368273, 1361776, 1357693, 1297018, 1163388, 
            1150019, 1139491, 1118783, 1111801, 1110290, 1103656, 1096373, 
            1084431, 1004688, 962928, 948112, 935991, 920200, 899685, 817174,
            807411, 801050, 794052, 768251, 757837, 756600, 753279, 717777, 
            654661, 647885, 644576, 618116, 618081, 596498, 585302, 583032, 
            563150, 557706, 556761, 551223, 531854, 525880, 525422, 518561, 
            514289, 510161, 491628, 485912, 477689, 454761, 440182, 439392, 
            437200, 435603, 431285, 427548, 425011, 418547, 403544, 402082, 
            402002, 399192, 395383, 394195, 389633, 388024, 377820, 376185, 
            374580, 370104, 369179, 361755, 358913 };
        //массив верхних границ
        public int[] veroyatnost = new int[100];


        public int summa = 0; //сумма всех вер-ей, высчитано в екселе

        public Random random = new Random();
        public CharGenerator_2()
        {
            count_syms = syms.Length;
            syms_array = syms.ToCharArray();
        }


        //чтобы не работать с миллионами, уменьшим числа
        public void Mimnimizacia_veroyatnostnih_chisel()
        {
            int min = chastota[99];
            for (int i = 0; i < 100; i++)
            {
                chastota[i] -= min;
            }
        }

        public void Diapazon_Summ()
        {
            veroyatnost[0] = chastota[0];
            for (int i = 1; i < 100; i++)
            {
                veroyatnost[i] = veroyatnost[i - 1] + chastota[i];
            }
            summa = veroyatnost[99];
        }

        public string getSym()
        {
            int random_symv = random.Next(0, summa);
            int j = 0;

            for (j = 0; j < count_syms; j++)
            {
                if (random_symv < veroyatnost[j])
                    break;
            }
            return slova[j];

        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            string name_file = "gen-1.txt";
            StreamWriter writer = new StreamWriter(name_file, true);


            CharGenerator_1 gen = new CharGenerator_1();
            gen.Ubrat_Nuli();
            gen.Diapazon_Summ();
            //gen.sum();
            for (int i = 0; i < 500; i++)
            {
                string ch = gen.getSym();
                Console.Write(ch);
                writer.Write(ch);
            }
            Console.WriteLine();
            writer.Close();



            name_file = "gen-2.txt";
            writer = new StreamWriter(name_file, true);
            CharGenerator_2 gen_2 = new CharGenerator_2();
            gen_2.Mimnimizacia_veroyatnostnih_chisel();
            gen_2.Diapazon_Summ();

            for (int i = 0; i < 1000; i++)
            {
                string ch = String.Concat(gen_2.getSym()," ");
                Console.Write(ch);
                writer.Write(ch);
            }
            Console.WriteLine();
            writer.Close();

            Console.Read();
            
        }
    }
}

