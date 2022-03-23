using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder
{
    class Program
    {
        public static void Main()
        {
            var matrix = InsertedMatrix();
            var wordSteam = InsertedWords();
            var result = new WordFinder(matrix).Find(wordSteam);
            Console.WriteLine("These are the words found in the matrix:");
            Console.WriteLine(string.Join(" - ", result));
        }

        private static string[] InsertedMatrix()
        {
            Console.WriteLine("Please enter a first row for the matrix. All rows must be of the same length.");
            //Based on the first row entered in the matrix I use its length to determinate the length of the matrix list
            var firstRow = Console.ReadLine();
            List<string> matrixStr = new()
            {
                firstRow
            };
            for (var i = 1; i < firstRow.Length; i++)
            {
                Console.WriteLine("Please enter another row for the matrix. The length must be of " + firstRow.Length + " characters.");
                var xRow = Console.ReadLine().ToLower();

                if (xRow.Length != firstRow.Length)
                {
                    Console.WriteLine("The length of the row must be of " + firstRow.Length + " characters. Please try again");
                    i--;
                    continue;
                }

                if (!string.IsNullOrEmpty(xRow))
                    matrixStr.Add(xRow);
            }
            return matrixStr.ToArray();
        }

        private static string[] InsertedWords()
        {
            List<string> wordSteamStr = new();
            //I allow a max of 15 words to search for practical purposes (the system will retrieve a max of 10 found words)
            for (var i = 1; i < 15; i++)
            {
                Console.WriteLine("Please enter a word to search. When you are done, type 'finish'. Max of words allowed: 15");
                var word = Console.ReadLine().ToLower();

                if (word.CompareTo("finish") == 0)
                    break;

                if (!string.IsNullOrEmpty(word))
                    wordSteamStr.Add(word);

            }
            return wordSteamStr.ToArray();
        }

        public class WordFinder
        {
            private readonly HashSet<string> matrix;

            public WordFinder(IEnumerable<string> matrix)
            {
                this.matrix = new HashSet<string>(matrix);
            }

            public IEnumerable<string> Find(IEnumerable<string> wordSteam)
            {
                //I join every row in the matrix separated by an empty space to make a single list
                var leftToRightSearchStr = string.Join(string.Empty, matrix);

                //I split every character in every row to make the columns of the matrix
                var charMatrix = matrix.Select(row => row.ToCharArray()).ToArray();

                //I use a StringBuilder to make the columns of the matrix
                var topToBottomSearchStrBuilder = new StringBuilder();
                for (var i = 0; i < charMatrix.Length; i++)
                {
                    for (var j = 0; j < charMatrix[i].Length; j++)
                    {
                        topToBottomSearchStrBuilder.Append(charMatrix[j][i]);
                    }
                }
                //Now I have the columns as a single list to search my words
                var topToBottomSearchStr = topToBottomSearchStrBuilder.ToString();

                var foundList = new HashSet<string>();
                //I look for my words from left to right
                foundList.UnionWith(wordSteam.Where(search => leftToRightSearchStr.Contains(search)));
                //I look for my words from top to bottom
                foundList.UnionWith(wordSteam.Where(search => topToBottomSearchStr.Contains(search)));

                //I order the words found by the most repeated ones and return the first 10 in the list
                var orderedList = foundList.GroupBy(x => x).OrderByDescending(x => x.Count()).Distinct().Take(10);

                orderedList = orderedList.Distinct().Take(10);

                var results = new HashSet<string>();

                foreach (var item in orderedList)
                {
                    results.Add(item.Key);
                }

                return results;
            }
        }
    }
}
