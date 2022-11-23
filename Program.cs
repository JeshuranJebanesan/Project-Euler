//The following has been written to solve all of Euler's Project on projecteuler.net/archives
//The goal is not to brute force finish as fast as I can but to solve the questions elegantly and also to learn and experiment with different ways of solving the same problem

namespace Project_Euler;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(q3());
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

    //Answer is 461373
    static int q2() {
        //Method 1 
        //Uses basic brute force method
        //Instead of checking if term is even, could have a counter capped at 3 as even terms occur every 3 terms
        //As odd + even = odd, even + odd = odd, then odd + odd = even
        /*
        int a = 1, b = 0, total = 0;

        while ((a += b) < 4000000) {
            b = a - b;
            if (a % 2 == 0) {
                total += a;
            }
            Console.WriteLine(a.ToString() + " " + b.ToString() + " " + total.ToString());
        }

        return total;
        */

        //Method 2
        //Uses new sequence that is made of every 3 terms in fib sequence
        /*
        int a = 0, b = 2, total = 0;

        while ((a += 3*a + b) < 4000000) {
            b = (a - b) / 4;
            total += a;
        }

        return total;
        */

        //Method 3
        //Uses mathematics to find the characteristic equation of this variant of the fibonacci sequence where every 3 terms (all even terms are sampled)
        //Characteristic equation is x^2 - 4x - 1 = 0 and roots are x = 2 +- √5
        //Then U n = a (2 + √5)^n + b (2 - √5)^n
        //Plugging in values, we get that a = 2 - (2 + √5)/√5 and b = (2 + √5)/√5
        int recurrenceSolution(int i) {
            return (int)(((2 - (2 + Math.Sqrt(5))/Math.Sqrt(5)) * Math.Pow(2 + Math.Sqrt(5), i)) + ((2 + Math.Sqrt(5))/Math.Sqrt(5)) * Math.Pow(2 - Math.Sqrt(5), i));
        }

        int total = 0;

        for (int a = 2; recurrenceSolution(a) < 4000000; a++) {
            total += recurrenceSolution(a);
        }

        return total;
    }

    //Answer is 6857
    static long q3() {
        //Method 1
        //Uses basic brute force
        //This brute force has a helper function to determine if a number is prime adding the number if so to a list of primes
        //The complete list of primes will be calculated by finding the factors after the sqrt of the number and checking whether they too are prime
        bool isPrime(long n) {
            List<long> primes = new List<long>();
            bool isPrime;

            for (int i = 2; i <= Math.Sqrt(n); i++ ) {
                isPrime = true;
                foreach (int prime in primes) {
                    if (i % prime == 0) {
                        isPrime = false;
                        break;
                    }
                }
                primes.Add(i);
            }

            isPrime = true;
            foreach (int prime in primes) {
                if (n % prime == 0) { return false; }
            }
            return isPrime;
        }
        List<long> primes = new List<long>();

        for (long i = 2; i <= (600851475143); i++ ) {
            if (isPrime(i) && (600851475143 % i == 0)) {
                primes.Add(i);
                Console.WriteLine(i);
            }
        } 

        return primes.Last();
    }
}
