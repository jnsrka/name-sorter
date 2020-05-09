using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string name;
                string separator = " ";
                var list = new List<KeyValuePair<string, string>>();

                using (var sr = new StreamReader("unsorted-names-list.txt"))
                {
                    while ((name = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            string[] nameArray = name.Split(separator);
                            string lastName = nameArray[nameArray.Length - 1];
                            IEnumerable<string> givenName = nameArray.SkipLast(1);

                            list.Add(new KeyValuePair<string, string>(lastName, string.Join(separator, givenName)));
                        }
                    }
                }

                using (var file = new StreamWriter("sorted-names-list.txt"))
                {
                    foreach (KeyValuePair<string, string> sortedName in list.OrderBy(x => x.Key))
                    {
                        var fullName = string.Concat(sortedName.Value, separator, sortedName.Key);
                        file.WriteLine(fullName);
                        Console.WriteLine(fullName);
                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Error occurred in reading file or writing to file:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
