using System;
using System.Collections.Generic;
using System.Linq;

namespace _58024_III_4_1
{
    class Dictionary
    {
        string _source;
        List<string> _dict = new List<string>();
        List<string> _full = new List<string>();
        public List<string> Dict
        {
            get { return _dict; }
        }
        public Dictionary(string source)
        {
            _source = source;
            CreateDictionary();
        }
        List<string> CreateDictionary()
        {
            List<char> chars = _source.Distinct().ToList();
            chars.ForEach(c =>
            {
                _dict.Add(c.ToString());
                _full.Add(c.ToString());
            });
            return _dict;
        }
        public List<string> Update(string var)
        {
            if (!_full.Contains(var))
            {
                _full.Add(var);
            }
            return _full;
        }
        public int IndexOf(string var)
        {
            return _full.IndexOf(var);
        }
        public void Print(int var)
        {
            if (var == 1)
            {
                _full.ForEach(x => Console.Write(x + " "));
            }
            else if (var == 2)
            {
                _dict.ForEach(x => Console.Write(x + " "));
            }
            else
            {
                _full.ForEach(x => Console.Write(x + " "));
            }
        }
        public int Length()
        {
            return _full.Count;
        }
        public bool Contains(string var)
        {
            return _full.Contains(var);
        }
    }
    class CompressedCode
    {
        List<string> _code;
        public CompressedCode()
        {
            _code = new List<string>();
        }
        public List<string> Add(int var)
        {
            _code.Add(var.ToString());
            return _code;
        }
        public int Length()
        {
            return _code.Count;
        }
        public void Print()
        {
            foreach (string var in _code) Console.Write(var + " ");
        }
        public SourceCode Decompress(Dictionary dict)
        {
            SourceCode decompressed = new SourceCode();
            // load initial dictionary as a list of strings for proper indexing
            List<string> dictionary = dict.Dict;
            do
            {
                try
                {
                    string first = dictionary[int.Parse(_code[0]) - 1];
                    string second = dictionary[int.Parse(_code[1]) - 1];
                    decompressed.Add(first);
                    dictionary.Add(first + second[0]);
                    _code.RemoveAt(0);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    string first = dictionary[int.Parse(_code[0]) - 1];
                    decompressed.Add(first);
                    dictionary.Add(first + first[0]);
                    _code.RemoveAt(0);
                }
            }
            while (_code.Count > 1);
            string last = dictionary[int.Parse(_code[0]) - 1];
            decompressed.Add(last);

            return decompressed;
        }
    }
    class SourceCode
    {
        string _source;
        int _n = 1;
        public SourceCode(string source)
        {
            _source = source;
        }
        public SourceCode()
        {
            _source = "";
        }
        public int Length()
        {
            return _source.Length;
        }
        string First()
        {
            if (_n > 1)
            {
                return _source.Substring(0, _n - 1);
            }
            else
                return _source.Substring(0, 1);
        }
        string Next()
        {
            return _source.Substring(0, _n);
        }
        public string Add(string var)
        {
            _source += var;
            return _source;
        }
        public void Print()
        {
            foreach (char var in _source) Console.Write(var);
        }
        public CompressedCode Compress(Dictionary dictionary)
        {
            CompressedCode resultCode = new CompressedCode();
            do
            {
                if (_n <= _source.Length)
                {
                    if (dictionary.Contains(Next()))
                    {
                        _n++;
                    }
                    else
                    {
                        dictionary.Update(Next());
                        resultCode.Add(dictionary.IndexOf(First()) + 1);
                        _source = _source.Remove(0, _n - 1);
                        _n = 1;
                    }
                }
                else
                {
                    dictionary.Update(First());
                    resultCode.Add(dictionary.IndexOf(First()) + 1);
                    _source = _source.Remove(0, _n - 1);
                    _n = 1;
                }
            }
            while (_source.Length > 0);
            return resultCode;
        }
    }
    internal class Program
    {
        static string toBeCompressed;
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Please enter the string you want to compress: \n");
                toBeCompressed = Console.ReadLine();

            } while (toBeCompressed == "");

            SourceCode source = new SourceCode(toBeCompressed);
            Dictionary dictionary = new Dictionary(toBeCompressed);

            Console.WriteLine("\nYour string length is " + source.Length());
            Console.WriteLine("\nLets see the initial dictionary: ");
            dictionary.Print(2);

            CompressedCode resultCode = source.Compress(dictionary);

            Console.WriteLine("\n--------------");
            Console.WriteLine("\nI give you the compressed output: \n");
            resultCode.Print();
            Console.WriteLine("\nCompressed code length is " + resultCode.Length());
            Console.WriteLine("\n--------------");
            Console.WriteLine("Now lets review the full dictionary created while compressing your string:\n");
            dictionary.Print(1);
            Console.WriteLine("\n--------------");
            Console.WriteLine("\nAnd finally to decompression: \n");

            SourceCode decompressed = resultCode.Decompress(dictionary);
            decompressed.Print();

            Console.ReadLine();
        }
    }
}

