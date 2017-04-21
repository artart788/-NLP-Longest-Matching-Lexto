using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Trie;

namespace LextoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var counter = new myCounterList();
            var reportCounter = "";
            var fileName = new List<string>();
            fileName.Add("index1.txt");

            LongLexTo Tokenizer = new LongLexTo("Self");

            if (System.IO.File.Exists("unknown.txt"))
            {
                System.IO.StreamReader unknownFile = System.IO.File.OpenText("unknown.txt");
                Tokenizer.addDict(unknownFile);
            }

            ArrayList typeList;
            int begin, end, type;


            var fsDict = File.ReadAllLines("lexitron.txt"); //Original Lexitron Dictionary
            //var fsDict = File.ReadAllLines("lexitronEXT.lex"); //Enchanced Version
            //var fsDict = File.ReadAllLines("lexitronMerge.txt"); //Merged Version

            foreach (var s in fsDict)
            {
                Tokenizer.addDict(s);
            }
            var toReturn = String.Empty;

            foreach(var sss in fileName)
            {
                var listText = File.ReadAllLines(sss);
                bool flag = false;
                bool flag2 = false;
                foreach (var eachline in listText)
                {
                    var ignore = false;
                    if(eachline=="" || eachline[0] == '่' || eachline[0] == '้' || eachline[0] == '๊' || eachline[0] == '๋')
                    {
                        if(eachline == "")
                        {

                        }else
                        {
                            ignore = true;
                        }
                    }
                    if (ignore)
                    {
                        Tokenizer.wordInstance(eachline.Substring(1));
                    }
                    else
                    {
                        Tokenizer.wordInstance(eachline);
                    }
                    
                    typeList = Tokenizer.getTypeList();
                    begin = Tokenizer.first();
                    int i = 0;
                    String result = "<s>|";
                    while (Tokenizer.hasNext())
                    {
                        end = Tokenizer.next();
                        type = (short)typeList[i];
                        var word = eachline.Substring(begin, end - begin);

                        //** REPORT CUT WORD **//
                        if (type == 0)
                        {
                            counter.AddElement(word);
                            reportCounter += word + "|";
                            flag = true;
                            flag2 = true;
                        }
                        //** REPORT CUT WORD **//
                        if(!flag)
                            result += word + "|";
                        flag = false;
                        begin = end;
                        ++i;
                    }
                    
                    result += "|</s>";

                    //REPORT COT WORD
                    if (flag2)
                    {
                        reportCounter += " ===> ";
                        reportCounter += result + "\n";
                        flag2 = false;
                    }
                    
                    //REPORT COT WORD

                    toReturn += result + "\n";
                }
            }

            var lines = toReturn.Split('\n');

            File.WriteAllText("output.txt", toReturn.Replace("||", "|"), Encoding.UTF8);
            File.WriteAllText("report_cut_word.txt", counter.getReport(), Encoding.UTF8);
            File.WriteAllText("report_counter.txt", reportCounter, Encoding.UTF8);
        }
    }
}
