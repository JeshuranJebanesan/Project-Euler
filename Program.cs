//The following has been written to solve all of Euler's Project on projecteuler.net/archives
//The goal is not to brute force finish as fast as I can but to solve the questions elegantly and also to learn and experiment with different ways of solving the same problem
//My first method will usually be a brute force method, but I will not move onto the next question without finding a solution I deem nice
//I will write hypothetical solutions for review if I have already solved the problem but wish to go more in depth later
// Every 5 problems I will review the previous 5 solutions and see if I can update them even more
//C# with .NET version 7.0.100

namespace Project_Euler;

class Program
{
    static void Main(string[] args)
    {
    }

    #region Q1
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
    //Review GCD and LCM
    #endregion
    #region Q2
    //Answer is 4613732
    static int q2Limit = 4000000;
    static int q2 () {
        //Method 1 
        //Uses basic brute force method
        //Instead of checking if term is even, could have a counter capped at 3 as even terms occur every 3 terms
        //As odd + even = odd, even + odd = odd, then odd + odd = even
        /*
        int a = 1, b = 0, total = 0;

        while ((a += b) < q2Limit) {
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

        while ((a += 3*a + b) < q2Limit) {
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

        for (int a = 2; recurrenceSolution(a) < q2Limit; a++) {
            total += recurrenceSolution(a);
        }

        return total;
    }
    //Review GCD and LCM
    #endregion
    #region Q3
    //Answer is 6857
    static long q3() {
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
        long q3Limit = 600851475143;
        //Method 1
        //Uses basic brute force
        //This brute force has a helper function to determine if a number is prime adding the number if so to a list of primes
        //The complete list of primes will be calculated by finding the factors after the sqrt of the number and checking whether they too are prime
        /*
        List<long> primes = new List<long>();

        for (long i = 2; i <= (q3Limit); i++ ) {
            if (isPrime(i) && (q3Limit % i == 0)) {
                primes.Add(i);
                Console.WriteLine(i);
            }
        } 

        return primes.Last();
        */

        long q3(int count, long final) {
        //Method 2
        //Method 1 is way too slow; a better method might be a recursive method dividing a number by primes till the number equals 1
        //As every number has a prime factorisation if the count starts off at the first prime, the number's largest prime will be the return value
        if (isPrime(count) && final / count == 1 && final % count == 0) { return count; }
        else if (isPrime(count) && final % count == 0) { return q3(count, final / count); }
        else return q3(count + 1, final);
        }

        return q3(2, q3Limit);

        //Hypothetical Method 1
        //Use a prime finding sieve or some other advanced algorithm
    }
    //Review data types and the different types and sizes of Int e.g Int32 Int64 and long
    //Review prime finding algorithms
    #endregion
    #region Q4
    //Answer is 906609
    static int q4() {
        //Method 1
        //Starts from the largest 3 digit numbers working its way down
        //I thought this would ensure the largest 3 digit numbers being multiplied giving the largest palindrome but I was wrong
        //As the program used to have a return in the for loops, the first palindrome found would be returned as I assumed both x and y would be their largest values
        //However this is only true for x as an edge case 580085 = 995 * 583. Even though x is larger than the x in the answer, y is significantly smaller.
        //In the answer, 906609 = 993 * 913
        int maxBound = 99;
        int largestPalindrome = 0;

        for (int x = 999; x > maxBound; x--) {
            for (int y = 999; y > 99; y--) {
                 if (isPalindrome(x * y)) {
                    if (x * y > largestPalindrome) {
                        largestPalindrome = x * y;
                        maxBound = y;
                    }
                }
            }
        }
        return largestPalindrome;

        bool isPalindrome(int x) {
            string y = x.ToString();
            int length = y.Length;
            return y.Substring(0, length) == flip(y).Substring(0, length);
        }

        string flip(string x) {
            if (x.Length == 1) { return x; }
            else return flip(x.Substring(1)) + x.Substring(0, 1);
        }
    }
    #endregion
    #region Q5
    //Answer is 232792560
    static long q5() {
        //Method 1
        //Uses basic gcd and lcm formula to calculate lcm
        //Applies lcm onto every element in the list
        long lcmcount = 1;

        long gcd(long x, long y) {
            if (x == 0) {return y;}
            return gcd(y % x, x);
        }

        long lcm(long x, int y) {
            return x*y / gcd(x, y);
        }
        
        for (int i = 2; i < 21; i++) {
            Console.WriteLine(i + " " + lcmcount);
            lcmcount = lcm(lcmcount, i);
        }

        return lcmcount;

        //Hypothetical Method 1
        //Make an array where each element stores a list of its prime factors or have an array of primes
        //If one term in the sequence 1..20, has a higher number of certain primes than the other, that number is updated
        //At the end all primes are multiplied
    }
    //Research sizes of ints, the question answer was wrong initially as there was overflow
    #endregion
    #region Q6
    //Answer is 25164150
    static int q6() {
        //Method 1
        //Use common polynomial sum equations to solve the question efficiently
        long linSum(int m) {
            return (m + 1) * m / 2; 
        }
        long squareSum(int m) {
            return m * (m+1) * (2*m + 1) / 6;
        }
        int q6Limit = 100;
        return (int)Math.Pow(linSum(q6Limit), 2) - (int)squareSum(q6Limit);
    }
    #endregion
    #region Q7
    //Answer is 104743
    static long q7() {
        //Method 1
        //Uses most basic prime finding algorithm I could think of
        //Could also research and use sieves e.g Eratosthenes, Atkins ...
        int q7Limit = 10001;
        int primeCount = 0; 
        int primesCount;
        List<long> primes = new List<long>();

        for ( long i = 2; ; i++ ) {
            for (primesCount = 0; primesCount != primes.Count(); primesCount++ ) {
                if (i % primes[primesCount] == 0) {
                    break;
                }
            }

            if (primesCount == primes.Count()) {
            primeCount++;
            if (primeCount == q7Limit) {return i;}
            primes.Add(i);
            }
        }
    }
    //Had problem trying to break out of nested loops
    //To solve I had a separate variable, primescount keep track of whether the first loop was iterated through fully or if it broke
    //If it broke once, I would break again out of the second loop
    //Additional ways to solve breaking out of nested loops involve changing the local iterator value, use of goto
    #endregion
    #region Q8
    //Answer is 23514624000
    static long q8() {
        //Method 1 uses basic brute force algorithm iterating through every string in q8 of length 13, comparing its size when multiplied to a store of the current biggest answer so far
        string q8 = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
        int q8Limit = 13;
        long counter;
        long q8Ans = 0;
        int asciiVal = 48;

        for (int i = 0; i <= q8.Length - q8Limit; i++) {
            counter = 1;
            for (int j = 0; j < q8Limit; j++) {
                Console.WriteLine(counter + " " + i + " " + j + " " + (i+j) + " " + (q8[i+j]-asciiVal));
                counter *= (q8[i+j]-asciiVal);
            }

            if (counter > q8Ans) { q8Ans = counter;}
        }
        return q8Ans;

        //Hypothetical Method 1
        //There is a lot of repetition in Method 1. Namely iterating through every group of 13 digits will use most digits 1000 times. 
        //We can remove all numbers of q8 that are within a range of 13 numbers of any number 0 as any group of numbers multiplied by 0 returns 0
        //This will return a few lists that we can an answer finding algorithm

        //Hypothetical Method 2
        //We can have a pointer at 0 and a pointer at 12 multiplying all numbers between the pointers
        //If the pointer at the front encounters a 0, the pointers will have their value increased by 13 (as long as no new 0s are within the new string) as 0 ruins the multiplication
        //If the number leaving the string is larger then the number entering, the new string isn't considered for multiplication as it is obviously smaller 
        //However the numbers leaving and entering will be stored up to a length of 13
        //e.g if 3 leaves and 2 enters, then the new number is smaller but if the next number to leave is 1 and the new entree 9, (3*1 > 2*9) then the new biggest number is calculated
        //However this code while more efficient is unnecessary for such a simple problem.
    }
    //Had some problems with char conversion to int
    //Bugfixed well and spotted problem intelligently and solved by converting char to int to ascii then minusing the ascii constant for numbers(48)
    //Could also convert char to string then convert to int
    #endregion
}