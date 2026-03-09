using System;

class Program {
   static void Main() {
       for (int i = 0; i < 5; i++)   {
          Console.WriteLine(i);
          Console.ReadLine();

            for (int z = 0; z < 5; z++)   {
              Console.WriteLine(i+" "+z);
              Console.ReadLine();
            }
        }
   }
}
