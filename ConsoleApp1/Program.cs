using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* PI pI = new PI();
             PI.pi();
             Monte monte = new Monte();
             monte.Montecarlo0();*/
            CodPurf codPurf = new CodPurf();
            codPurf.reshenie();

        }
    }
    class PI
    {
        static public void pi()
        {
            double n = 1000;
            int k = 0;
            double s;
            double x;
            double y;
            Random random = new Random();

            for (int i = 0; i <= n; i++)
            {
                x = random.NextDouble();
                y = random.NextDouble();

                if (((x - 1) * (x - 1)) + ((y - 1) * (y - 1)) <= 1)
                {
                    k += 1;
                }
            }
            s = 4 * k / n;
            Console.WriteLine(s);
        }
    }
    class Monte
    {
        public void Montecarlo0()
        {
            double n = 100000;
            double k = 0.0;
            double s;
            double a = 5.0;
            double b = 8.5;
            double x;
            double y;
            Random random = new Random();

            for (int i = 0; i <= n; i++)
            {
                x = random.NextDouble() * b;
                y = random.NextDouble() * a;
                if (x / 3 < y && x * (10 - x) / 5 > y)
                {
                    k++;
                }
            }
            s = a * b * k / n;
            Console.WriteLine(s);
            Console.ReadKey();
        }
        public void Montecarlo1()
        {
            double n = 100000;
            double k = 0.0;
            double s;
            double a = 5.0;
            double b = 8.5;
            double x;
            double y;
            Random random = new Random();

            for (int i = 0; i <= n; i++)
            {
                x = random.NextDouble() * b;
                y = random.NextDouble() * a;
                if (Math.Sin(x) < y && x * (10 - x) / 5 > y)
                {
                    k++;
                }
            }
            s = a * b * k / n;
            Console.WriteLine(s);
            Console.ReadKey();
        }

    }

    class CodPurf
    {
        public int minim = 0;
        int indexMin = 0;
        public int[,] readFile()
        {
            string[] arr1;
            string[] arr2;
            string file1 = "";
            string file2 = "";
            using (StreamReader streamReader = new StreamReader("1.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    file1 = streamReader.ReadLine();
                    file2 = streamReader.ReadLine();

                }
            }
            arr1 = file1.Split(' ');
            arr2 = file2.Split(' ');

            int[,] cod = new int[2, arr2.GetLength(0)];


            for (int j = 0; j < arr2.Length -1 ; j++)
            {
                cod[0, j] = Convert.ToInt32(arr1[j]);
                cod[1, j] = Convert.ToInt32(arr2[j]);
            }

            return cod;

        }
        public void reshenie()
        {
            int[,] arr = readFile();
            int[] drewo = new int[arr.GetLength(1)];
            int[] list = new int[arr.GetLength(1)];
            int[] listClon = new int[arr.GetLength(1)];
            int[,] decodirovanie = new int[2, arr.GetLength(1)];
            List<int> coding = new List<int>();
            List<int> codingClone = new List<int>();


            for (int i = 0; i < arr.GetLength(1); i++)
            {
                drewo[i] = arr[0, i];
                list[i] = arr[1, i];
                listClon[i] = arr[1, i];
            }

            indexMin = minimum(list);

            for (int i = 0; list.Sum() > 0; i++)
            {


                if (i == indexMin)
                {
                    if (povtor(drewo, list[i], list) == true)
                    {

                        coding.Add(drewo[i]);
                        drewo[i] = -1;
                        list[i] = -1;
                        i = -1;
                        minim = 0;
                        indexMin = minimum(list);

                    }
                    else
                    {
                        indexMin = minimum(list);
                    }
                }
            }
            for (int i = 0; i < coding.Count; i++)
            {
                Console.WriteLine(coding[i]);
            }
            codingClone = coding;

            for (int i = 0; proverkanapoloh(listClon); i++)
            {
                if(i == codingClone.Count())
                {
                    break;
                }
                decodirovanie[0, i] = codingClone[i];
               
                
                decodirovanie[1, i] = minimallistdecod(ref codingClone, ref listClon);
                codingClone[i] = -1;
            }

            for(int i = 0;i< decodirovanie.GetLength(0);i++)
            {
                for(int j = 0;j< decodirovanie.GetLength(1);j++)
                {
                    if(i ==0)
                    {
                        Console.Write(decodirovanie[i, j] + " ");
                    }
                   
                    if(i ==1)
                    {
                        Console.Write(decodirovanie[i, j] + " ");
                    }
                    if(j ==7)
                    {
                        break;
                    }
                    
                }
                Console.WriteLine();
            }
            using (StreamWriter streamWriter = new StreamWriter("2.txt"))
            {
                for (int i = 0; i < decodirovanie.GetLength(0); i++)
                {
                    for (int j = 0; j < decodirovanie.GetLength(1); j++)
                    {
                        if (i == 0)
                        {
                            streamWriter.Write(decodirovanie[i, j] + " ");
                        }

                        if (i == 1)
                        {
                            streamWriter.Write(decodirovanie[i, j] + " ");
                        }
                        if (j == 7)
                        {
                            break;
                        }

                    }
                    streamWriter.WriteLine();
                }
            }



        }
        public bool proverkanapoloh(int []listClon)
            {
                for(int i = 0;i < listClon.Length;i++)
                {
                    if (listClon[i] > 0)
                    {
                        return true;
                    }

                }
                return false;
            }

        public int minimallistdecod(ref List<int> codingClone, ref int[] listClon)
        {
            int count = 0;
            int min = 0;
            for (int i = 0; i < listClon.Length;i++)
            {
                min = minimalfordecod(listClon);
                for (int j = 0;j < codingClone.Count;j++)
                {
                    
                    if (min == codingClone[j])
                    {
                        count++;
                    }
                }
                if(count == 0)
                {
                    minim = 0;
                    delite(min, ref listClon);
                    return min;
                }
                count = 0;
            }
            return 0;
        }
        public void delite(int min, ref int[] listClon)
        {
            for(int i = 0;i < listClon.Length;i++)
            {
                if(min == listClon[i])
                {
                    listClon[i] = -1;
                }
            }
        }
        public int minimalfordecod(int []listclone)
        {
            int index = 0;
            int min = listclone.Max();
            
            for (int i = 0; i < listclone.Length; i++)
            {
                if (listclone[i] > 0)
                {
                    if (min > listclone[i])
                    {
                        if (listclone[i] > minim)
                        {
                            index = i;
                            minim = listclone[i];
                            min = listclone[i];
                        }

                    }
                }

            }
            return min;
        }
       

        public int minimum(int []list)
        {
            
            
            
            int index = 0;
            int min = list.Max();
            for(int i  = 0; i < list.Length; i++)
            {
                if(list.Max() == list[i])
                {
                    index = i;
                }
            }
            for(int i = 0; i < list.Length ; i++) 
            {
                if (list[i] > 0)
                {
                    if (min > list[i])
                    {
                        if (list[i] > minim)
                        {
                            index = i;
                            minim = list[i];
                            min = list[i];
                        }

                    }
                }
                
            }
            return index;
        }
        public int minimumclone(int[] list)
        {



            int index = 0;
            int min = list.Max();
            for (int i = 0; i < list.Length; i++)
            {
                if (list.Max() == list[i])
                {
                    index = i;
                }
            }
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] > 0)
                {
                    if (min > list[i])
                    {
                        if (list[i] > minim)
                        {
                            index = i;
                            minim = list[i];
                            min = list[i];
                        }

                    }
                }

            }
            return min;
        }
        public bool povtor(int[]drevo, int a, int[]list)
        {
            int svyse = 0;
            for(int i = 0; i <drevo.Length;i++)
            {
                
                    if (drevo[i] == a)
                    {
                    svyse = drevo[indexMin];
                      if (svyzlist(drevo, list[i]) == false || svyzdrevo(a,drevo,list) == true|| sviselist(list, svyse)== false)
                       {
                        return false;
                       }
                        
                    }
                
                
            }
            return true;
        }
        public bool svyzlist(int[]drevo, int list)
        {
            
            for(int i = 0; i < drevo.Length;i++)
            {
                if(list == drevo[i])
                {
                    return false;
                }
            }
            return true;
        }
        public bool svyzdrevo(int drevo, int []drevo1, int []list)
        {
            
            int svis;
            for (int i = 0; i < list.Length; i++)
            {
                if(drevo == list[i])
                {
                    svis = drevo1[i];
                    for( int j = 0; j < drevo1.Length; j++)
                    {
                        if (drevo1[j] == svis)
                        {
                            
                            
                                return true;
                            
                        }
                    }
                }
            }
            return false;
        }
        public bool sviselist(int[] list, int svise)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if(svise == list[i])
                {
                    return false;
                }
            }
            return true;
        }
        

    }


}
