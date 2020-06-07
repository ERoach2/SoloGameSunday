using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloGameSundayPicker
{
    /// <summary>
    /// Extention to lists for random shuffles
    /// </summary>
    public static class ListShuffleExtension
    {
        private static CryptoRandomNumberGen rng = new CryptoRandomNumberGen();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(0,n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }//END class ListShuffleExtension()
}//END namespace
