using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CollectionType
{

    [Serializable]
    class CollectionType<T>
    {
        T[] userList;
        int count;
        T nullik;
        public int Count { get; set; }
        public T this[int index]
        {
            set
            {

                if (index >= count)
                {
                    Array.Resize(ref userList, index + 2);
                    count = index + 1;
                }
                userList[index] = value;
            }
            get
            {
                if (index >= count)
                {
                    return nullik;
                }
                return userList[index];
            }
        }


        public CollectionType()
        {

            count = 0;
            userList = new T[count + 1];

        }

        ~CollectionType() { }

        public void Add(T obj)
        {
            userList[count] = obj;
            count++;
            Array.Resize(ref userList, count + 2);
        }


        public void Remove(int number)
        {

            if (number < count && number >= 0)
            {
                for (int i = number; i < count; i++)
                {
                    userList[i] = userList[i + 1];
                }
                count--;
            }

        }

        public T GetInfo(int number)
        {
            T obj;
            var val = default(T);

            if (number < count && number >= 0)
            {
                obj = userList[number];
                return obj;
            }

            return val;
        }

        public void ReWrite()
        {
            for (int i = 0; i < count; i++)
                Console.WriteLine("Value: " + userList[i]);
            Console.WriteLine();
        }

        public int SizeOfCollection()
        {
            return count;
        }

    }

    class GeometryShape : IComparable<GeometryShape>
    {
        public string GeomName { set; get; }
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public GeometryShape() { }
        public GeometryShape(string GeomName, float X, float Y, int ID)
        {
            this.GeomName = GeomName;
            this.X = X;
            this.Y = Y;
            this.ID = ID;
        }


        public int CompareTo(GeometryShape obj)
        {

            if (this.X * this.Y > obj.X * obj.Y)
                return -1;
            if (this.X * this.Y < obj.X * obj.Y)
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            return String.Format("{4} Название: {0}\tДлина: {1}\tШирина: {2}\tПлощадь: {3}",
                this.GeomName, this.X, this.Y, this.X * this.Y, this.ID);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CollectionType<int>[] cl = new CollectionType<int>[10];
            int count = 3;
            int sumCount = 0;
            int max = -1;
            int min = 11;


            for (int i = 0; i < 10; i++)
                cl[i] = new CollectionType<int>();


            try
            {
                Stream myStream1 = File.OpenRead("kek.txt");
                var ser1 = new BinaryFormatter();

                for (int i = 0; i < cl.Length; i++)
                {

                    cl[i] = ser1.Deserialize(myStream1) as CollectionType<int>;
                }
                myStream1.Close();
            }catch(IOException ex )
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();

            int k = cl[0][4];
            //cl[0].ReWrite();

            string str;
            bool bl = true;
            while (bl)
            {

                Console.WriteLine("ВВЕДТЕ ОПЕРАЦИЮ НАД КОЛЕКЦИЕЙ(Exit, Add, Remove, ReWrite, GetInfo, Serialize)");
                str = Console.ReadLine();
                switch (str)
                {
                    case "Add":
                        {

                            Console.WriteLine("ВВЕДИТЕ НОМЕР КОЛЛЕКЦИИ");
                            int a = int.Parse(Console.ReadLine());

                            if (a >= 0 && a <= 10)
                            {
                                try
                                {
                                    Console.WriteLine("СОДЕРЖИМОЕ ОБЪЕКТА");
                                    var obj = int.Parse(Console.ReadLine());
                                    cl[a].Add(obj);
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Ошибка" + ex.Message);
                                }
                            }

                            break;
                        }
                    case "Remove":
                        {

                            Console.WriteLine("ВВЕДИТЕ НОМЕР Коллекции");
                            int a = int.Parse(Console.ReadLine());
                            if (a >= 0 && a <= 10)
                            {

                                try
                                {
                                    Console.WriteLine("ВВЕДИТЕ НОМЕР ОБЪЕКТА");
                                    int b = int.Parse(Console.ReadLine());
                                    cl[a].Remove(b);
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Ошибка" + ex.Message);
                                }
                            }
                            break;
                        }
                    case "GetInfo":
                        {

                            Console.WriteLine("ВВЕДИТЕ НОМЕР Коллекции");
                            int a = int.Parse(Console.ReadLine());
                            if (a >= 0 && a <= 10)
                            {
                                try
                                {
                                    Console.WriteLine("ВВЕДИТЕ НОМЕР ОБЪЕКТА");
                                    int b = int.Parse(Console.ReadLine());
                                    cl[a].GetInfo(b);
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Ошибка " + ex.Message);
                                }
                            }
                            break;
                        }
                    case "ReWrite":
                        {
                            Console.WriteLine("ВВЕДИТЕ НОМЕР Коллекции");
                            int a = int.Parse(Console.ReadLine());
                            if (a >= 0 && a <= 10)
                            {
                                cl[a].ReWrite();
                            }
                            break;
                        }
                    case "Serialize":
                        {
                            Stream myStream = File.OpenWrite("kek.txt");
                            var ser = new BinaryFormatter();

                            for (int i = 0; i < cl.Length; i++)
                            {
                                ser.Serialize(myStream, cl[i]);
                            }
                            myStream.Close();
                            break;
                        }
                    case "Exit":
                        {
                            bl = false;

                            break;
                        }
                    default:
                        Console.WriteLine("default");
                        break;

                }
            }



            foreach (var v in cl)
            {
                Console.WriteLine("Size OF COLLECTIONS " + v.SizeOfCollection());
                if (v.SizeOfCollection() == count)
                    sumCount++;
            }

            Console.WriteLine();
            Console.WriteLine("sumcount " + sumCount);
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                if (cl[i].SizeOfCollection() >= max)
                    max = cl[i].SizeOfCollection();
            }

            Console.WriteLine("Max size of Collection: " + max);

            for (int i = 0; i < 10; i++)
            {
                if (cl[i].SizeOfCollection() <= min)
                    min = cl[i].SizeOfCollection();
            }

            Console.WriteLine("Min size of Collection: " + min);

            Console.WriteLine();


            List<GeometryShape> ls = new List<GeometryShape>();
            List<GeometryShape> ls2 = new List<GeometryShape>();

            ls.Add(new GeometryShape("Kvadrat", 20, 20, 1));
            ls.Add(new GeometryShape("Pri1", 30, 40, 2));
            ls.Add(new GeometryShape("Pri22", 80, 40, 3));


            ls2.Add(new GeometryShape("Pri5", 90, 40, 6));
            ls2.Add(new GeometryShape("Pri63", 60, 40, 7));

            string writePath = @"C:\users\Sanya\hta.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    foreach (var a in ls)
                        sw.WriteLine(a);

                    foreach (var a in ls2)
                        sw.WriteLine(a);
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var lk = ls.Where(p => p.ID >= 1).OrderBy(p => p.X).Select(p => "*" + p.GeomName + "*");
            foreach (var a in lk)
                Console.WriteLine(a);

            var lk2 = ls.Where(p => p.ID >= 2).Select(p => ++p.ID).Max();
            Console.WriteLine(lk2);

            var lk3 = ls.OrderBy(P => P.Y).Take(2).Min();
            Console.WriteLine(lk3);

        }


    }
}

