using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1 {

    public class PromotionComp {
        public string Name { get; set; }
        public List<string> Location { get; set; }
        public PromotionComp(string name, List<string> location) {
            Name = name;
            Location = location;
        }

        public PromotionComp(string str) {
            string[] strings = str.Split(':');
            Name = strings[0];
            string[] loc = strings[1].Split(',');
            Location = new List<string>();
            foreach(string a in loc) {
                Location.Add(a);
            }
        }
    }

    public class ListPromotion {

        public List<PromotionComp> promotion;

        public ListPromotion() {
            promotion = new List<PromotionComp>();
        }


        public ListPromotion(List<PromotionComp> arr) {
            promotion = arr;
        }

        public void Add(PromotionComp cell) {
            promotion.Add(cell);
        }

        public string Search(string loc) {

            string comp = null;
            string[] strings = loc.Split('/');
            Regex regex;
            if(loc == "")
                return "Пустое поле";

            for(int i = 0; i < strings.Length; i++)
                foreach(var item in promotion) {
                    foreach(var l in item.Location) {
                        string t = strings[i] + "$";
                        regex = new Regex(@t);
                        MatchCollection matches = regex.Matches(l);
                        if(matches.Count != 0) {
                            comp += item.Name + "\n";
                        }
                    }
                    if(comp == null)
                       return "Таких нету или неверно введена локация";
                }
            //if(comp == null)
            //return "Таких нету или неверно введена локация";
            return comp;
        }
    }

    class LoadFileList {
        public static List<string> readPromList() {
            string text = "";
            List<string> p = new List<string>();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"list\Яндекс.Директru.txt");
            using(StreamReader fs = new StreamReader(path)) {
                while(true) {
                    string temp = fs.ReadLine();
                    if(temp == null) break;
                    p.Add(temp);
                }
            }
            return p;
        }

        public static string readPromStr() {
            string text = "";
            List<string> p = new List<string>();
            //using(StreamReader fs = new StreamReader("\Яндекс.Директru.txt")) {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"List\Яндекс.Директru.txt");
            using(StreamReader fs = new StreamReader(path)) {
                while(true) {
                    string temp = fs.ReadLine();
                    if(temp == null) break;
                    text += temp + "\n";
                }
            }
            return text;
        }
    }

}

