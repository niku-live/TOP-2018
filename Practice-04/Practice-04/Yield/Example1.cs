using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_04.Yield
{
    public class Example1
    {
        IEnumerable<string> TestMethod(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i.ToString();
            }
            yield return "Final";
        }

        public void RunExample()
        {
            var enumerator = TestMethod(5).GetEnumerator();

            var list = TestMethod(5).ToList();

            foreach (var item in TestMethod(5))
            {
                Console.WriteLine(item);
            }
        }
    }
}
