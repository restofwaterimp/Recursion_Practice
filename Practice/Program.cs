// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Transactions;

//foreach (int i in ProduceEvenNumbers(9))
//{
//    Debug.Write(i);
//    Debug.Write(" ");
//    Console.Write(i);
//    Console.Write(" ");
//}
//// Output: 0 2 4 6 8

//IEnumerable<int> ProduceEvenNumbers(int upto)
//{
//    for (int i = 0; i <= upto; i += 2)
//    {
//        yield return i;
//    }
//}





internal class Program
{
    public static void Main(string[] args)
    {
        var a = Permutation("ABCDE", 4);
        List<string> result = new List<string>();
        foreach (var b in a)
        {
            string str = "";
            foreach (var c in b)
            {
                str += c.ToString();
            }
            result.Add(str);
        }

        
        Console.WriteLine(result.Count);
    }

    /// <summary>
    /// Permutation 順列(重複あり)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iterable">葉入れ悦</param>
    /// <param name="r">nPr の rの部分</param>
    /// <returns></returns>
    static IEnumerable<IEnumerable<T>> Permutation<T>(IEnumerable<T> iterable, int r = -1)
    {
        //入力を配列に変更
        var pool = iterable.ToList();
        //配列の長さ
        int n = pool.Count;

        r = r < 0 ? n : r;
        //総数より、抽出が大きい場合は終了
        if (r > n) yield break;
        /*全長*/
        var indices = Enumerable.Range(0, n).ToList();
        /*後ろから*/
        var cycles = Enumerable.Range(n - r + 1, r).ToList();
        cycles.Reverse();
        //変化なし版
        yield return indices.Take(r).Select(i => pool[i]);

        int indicesLength = indices.Count;
        int cyclesLength = cycles.Count;

        while (true)
        {
            bool breakCheck = false;
            for (int i = r - 1; i >= 0; i--)
            {
                cycles[i] -= 1;
                if (cycles[i] == 0)
                {
                    /*iの値を最後尾に持っていき、i以降をひとつ前に寄せる*/
                    var tmp = indices[i];
                    for (int k = i; k < indicesLength - 1; k++)
                    {
                        indices[k] = indices[k + 1];
                    }
                    indices[indicesLength - 1] = tmp;
                    cycles[i] = n - i; // 順列でとる値を復活(元に戻す)
                    /*
                     * 確認のための出力
                    foreach (var c in indices)
                    {
                        Console.Write(c);
                    }
                    Console.Write("  ");

                    foreach (var p in cycles)
                    {
                        Console.Write(p);
                    }
                    Console.WriteLine(" ★");
                    */

                }
                else
                {
                    /*指定の番号との入れ変え*/
                    int j = cycles[i];
                    var tmp = indices[i];
                    // C# 8.0 空しか ^を利用したindexの表現はできないらしい。
                    //indices[i] = indices[indicesLength - j];
                    indices[i] = indices[^j];
                    indices[^j] = tmp;
                    /* 確認用
                    foreach(var c in indices)
                    {
                        Console.Write(c);
                    }
                    Console.Write("  ");

                    foreach(var p in cycles)
                    {
                        Console.Write(p);
                    }

                    Console.WriteLine();
                    */
                    yield return indices.Take(r).Select(i => pool[i]);
                    breakCheck = true;
                    break;
                }
            }
            if (!breakCheck) yield break;
           //yield break;
        }
    }

}