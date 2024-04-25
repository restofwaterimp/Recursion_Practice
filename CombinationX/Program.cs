

using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static void Main(string[] args)
    {

        Console.Write(numberOfWaysX(25, new int[] { 3, 4, 11, 18, 20 }));
    }


    public static int numberOfWaysX(int x , int[] intArr)
    {

        return numberOfWaysXHelper(x, intArr, 0);
    }


    public static int numberOfWaysXHelper(int x , int[] intArr, int sum)
    {
        int count = 0;
        foreach(var i in intArr)
        {
            if ((sum + i) > x)
            {
                break;
            }else if((sum + i) == x){
                count++;
                break;
            }
            else
            {
                count += numberOfWaysXHelper(x, intArr, sum + i);
            }
        }
        return count;
    }
}