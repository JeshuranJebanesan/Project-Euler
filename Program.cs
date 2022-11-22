//The following has been written to solve all of Euler's Project on projecteuler.net/archives
//The goal is not to brute force finish as fast as I can but to solve the questions elegantly and also to learn and experiment with different ways of solving the same problem

namespace Project_Euler;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(q2());
    }

    //Answer is 233168
    static int q1() {
        //Method 1
        //Using basic for loop
        //Hashset is unnecessary
        /*
        int total = 0;

        for ( int i = 1; i < 1000; i++) {
            if (i % 3 == 0 || i % 5 == 0) {total += i;}
        }

        return total;
        */

        //Method 2
        //Using list comprehension and lambda function from haskell
        //foldr equivalent is aggregate
        //c# equivalent uses LINQ and IEnumerables on which more study is needed 
        return Enumerable.Range(1, 999).Where(x => x % 3 == 0 || x % 5 == 0).Sum();

        //Hypothetical Method 1
        //Use arithmetic sum formula a (a(3) + a(5) - a(15)) with bigO(1)
    }

    static int q2 () {
        int a = 1, b = 0, total = 0;

        while ((a += b) < 4000000) {
            b = a - b;
            if (a % 2 == 0) {
                total += a;
            }
            Console.WriteLine(a.ToString() + " " + b.ToString() + " " + total.ToString());
        }

        return total;
    }
}
