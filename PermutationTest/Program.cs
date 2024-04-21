using System;
using System.Collections.Generic;
using System.Linq;

public static class Permutations
{
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> iterable, int r = -1)
    {
        var pool = iterable.ToList();
        var n = pool.Count;
        r = r < 0 ? n : r;
        if (r > n)
            yield break;

        var indices = Enumerable.Range(0, n).ToList();
        var cycles = Enumerable.Range(n, n - r + 1).ToList();

        yield return indices.Take(r).Select(i => pool[i]);

        while (n > 0)
        {
            for (var i = r - 1; i >= 0; i--)
            {
                cycles[i]--;
                if (cycles[i] == 0)
                {
                    indices[i..] = indices[(i + 1)..].Concat(indices[i..(i + 1)]);
                    cycles[i] = n - i;
                }
                else
                {
                    var j = cycles[i];
                    (indices[i], indices[^j]) = (indices[^j], indices[i]);
                    yield return indices.Take(r).Select(i => pool[i]);
                    break;
                }
            }
            else
            {
                yield break;
            }
        }
    }

    public static void Main()
    {
        // Example usage:
        var result = GetPermutations("ABCD", 2);
        foreach (var perm in result)
        {
            Console.WriteLine(string.Join(" ", perm));
        }
    }
}
