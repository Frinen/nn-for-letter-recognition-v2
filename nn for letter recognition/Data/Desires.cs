using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nn_for_letter_recognition.Data
{
    public class Desires
    {
        public static Dictionary<int, List<double>> DesiresDictionary;

        static Desires()
        {
            DesiresDictionary = new Dictionary<int, List<double>>
            {
                {0, new List<double>{
                    1.0,0.0,0.0,0.0,0.0,0.0
                }},
                {1, new List<double>{
                    0.0,1.0,0.0,0.0,0.0,0.0
                }},
                {2, new List<double>{
                    0.0,0.0,1.0,0.0,0.0,0.0
                }},
                {3, new List<double>{
                    0.0,0.0,0.0,1.0,0.0,0.0
                }},
                {4, new List<double>{
                    0.0,0.0,0.0,0.0,1.0,0.0
                }},
                {5, new List<double>{
                    0.0,0.0,0.0,0.0,0.0,1.0
                }},

            };
        }

        public static Dictionary<int, List<double>> GetDictionary()
        {
            return DesiresDictionary;
        }
        public static void GetLetter(List<double> list)
        {
            var t = DesiresDictionary.ContainsValue(list);

        }
    }
}
