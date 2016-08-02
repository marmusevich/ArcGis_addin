using System.Collections.Generic;
using System.Text;

namespace SharedClasses
{
    //транслитировать строку на украинском языке
    class TranslitUaToLat
    {
        // соответствия для первой буквы в слове
        private static Dictionary<string, string> m_FirstTransliter = null; 
        // соответствия остальных букв
        private static Dictionary<string, string> m_Transliter = null;
        
        //иницилизация словарей
        private static void PrepareTranslit()
        {
            m_FirstTransliter = new Dictionary<string, string>();
            m_FirstTransliter.Add("Є", "Ye");
            m_FirstTransliter.Add("є", "ye");
            m_FirstTransliter.Add("Й", "Y");
            m_FirstTransliter.Add("й", "y");
            m_FirstTransliter.Add("Ї", "Yi");
            m_FirstTransliter.Add("ї", "yi");
            m_FirstTransliter.Add("Ю", "Yu");
            m_FirstTransliter.Add("ю", "yu");
            m_FirstTransliter.Add("Я", "Ya");
            m_FirstTransliter.Add("я", "ya");

            m_Transliter = new Dictionary<string, string>();
            m_Transliter.Add("а", "a");
            m_Transliter.Add("А", "A");
            m_Transliter.Add("б", "b");
            m_Transliter.Add("Б", "B");
            m_Transliter.Add("в", "v");
            m_Transliter.Add("В", "V");
            m_Transliter.Add("г", "h");
            m_Transliter.Add("Г", "H");
            m_Transliter.Add("ґ", "g");
            m_Transliter.Add("Ґ", "G");
            m_Transliter.Add("д", "d");
            m_Transliter.Add("Д", "D");
            m_Transliter.Add("е", "e");
            m_Transliter.Add("Е", "E");
            m_Transliter.Add("є", "ie");
            m_Transliter.Add("Є", "Ie");
            m_Transliter.Add("ж", "zh");
            m_Transliter.Add("Ж", "Zh");
            m_Transliter.Add("з", "z");
            m_Transliter.Add("З", "Z");
            m_Transliter.Add("и", "y");
            m_Transliter.Add("И", "Y");
            m_Transliter.Add("і", "i");
            m_Transliter.Add("І", "I");
            m_Transliter.Add("ї", "i");
            m_Transliter.Add("Ї", "I");
            m_Transliter.Add("й", "i");
            m_Transliter.Add("Й", "I");
            m_Transliter.Add("к", "k");
            m_Transliter.Add("К", "K");
            m_Transliter.Add("л", "l");
            m_Transliter.Add("Л", "L");
            m_Transliter.Add("м", "m");
            m_Transliter.Add("М", "M");
            m_Transliter.Add("н", "n");
            m_Transliter.Add("Н", "N");
            m_Transliter.Add("о", "o");
            m_Transliter.Add("О", "O");
            m_Transliter.Add("п", "p");
            m_Transliter.Add("П", "P");
            m_Transliter.Add("р", "r");
            m_Transliter.Add("Р", "R");
            m_Transliter.Add("с", "s");
            m_Transliter.Add("С", "S");
            m_Transliter.Add("т", "t");
            m_Transliter.Add("Т", "T");
            m_Transliter.Add("у", "u");
            m_Transliter.Add("У", "U");
            m_Transliter.Add("ф", "f");
            m_Transliter.Add("Ф", "F");
            m_Transliter.Add("х", "kh");
            m_Transliter.Add("Х", "Kh");
            m_Transliter.Add("ц", "ts");
            m_Transliter.Add("Ц", "Ts");
            m_Transliter.Add("ч", "ch");
            m_Transliter.Add("Ч", "Ch");
            m_Transliter.Add("ш", "sh");
            m_Transliter.Add("Ш", "Sh");
            m_Transliter.Add("щ", "shch");
            m_Transliter.Add("Щ", "Shch");
            m_Transliter.Add("ю", "iu");
            m_Transliter.Add("Ю", "Iu");
            m_Transliter.Add("я", "ia");
            m_Transliter.Add("Я", "Ia");
            // пунк 2 в примечаниях приказа
            m_Transliter.Add("ь", "");
            m_Transliter.Add("Ь", "");
            m_Transliter.Add("'", "");
        }


        // транслитирировать строку
        // input - строка на украинском языке
        // возвращает строку латинницей
        public static string Convert(string input)
        {
            if (input == null)
                return "";

            if (m_Transliter == null || m_FirstTransliter == null)
                PrepareTranslit();

            StringBuilder ans = new StringBuilder();
            
            // сюда вставить замену для сокращений улица, переулок и т.д.

            // специальное буквосочетание, пунк 1 в примечаниях приказа
            input = input.Replace("Зг", "Zgh");
            input = input.Replace("зг", "zgh");

            for (int i = 0; i < input.Length; i++)
            {
                if (((i == 0) || (i > 0 && (input[i - 1] == ' ' || input[i - 1] == '\n'))) && m_FirstTransliter.ContainsKey(input[i].ToString()))
                {
                    //первая буква
                    ans.Append(m_FirstTransliter[input[i].ToString()]);
                }
                else if (m_Transliter.ContainsKey(input[i].ToString()))
                {
                    //остальные буквы
                    ans.Append(m_Transliter[input[i].ToString()]);
                }
                else
                {
                    //соответствия нет, передать как есть
                    ans.Append(input[i].ToString());
                }
            }
            return ans.ToString();


        }



    }
}
