using HtmlAgilityPack;
using System;
using System.Data.SQLite;

namespace MyNamespace
{    
    class Scrap_News
    {       
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando processo.");

            Captura_url();
            Processa_url();
            Console.WriteLine("Processo finalizada.");
        }
        public static void Insere_url_DB(List<string> list)
        {
            using (SQLiteConnection c = new SQLiteConnection(Retorna_sql_conn()))
            {
                c.Open();
                Console.WriteLine("Cadastrando URLs.");
                foreach (var item in list)
                { 
                    string sql = $"INSERT INTO url_news (url) VALUES ('{item}' )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void Processa_url()
        {
            using (SQLiteConnection c = new SQLiteConnection(Retorna_sql_conn()))
            {
                c.Open();
                string sql = $"SELECT * FROM url_news";
                List<string> list_url = new List<string>();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {                           
                           list_url.Add(reader["url"].ToString());                              
                        }
                    }
                }
                Carrega_news(list_url);
            }
        }
        public static void Carrega_news(List<string> urls)
        {
            Console.WriteLine("Atualizando titulo e noticia.");
            foreach (var url in urls)
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = new HtmlDocument();
                doc = hw.Load(url);
                string titulo = "";
                string noticia = "";

                //*[@id="singleMain"]/div[1]/div[3]
                HtmlNodeCollection content = doc.DocumentNode.SelectNodes("//*[@id='singleMain']/div[1]/div[3]/div/h1");

                foreach (HtmlNode link in content)
                {
                    titulo = link.InnerText;
                }

                content = doc.DocumentNode.SelectNodes(@"//*[@id='singleMain']/div[2]/div[1]/div[1]/article");

                foreach (HtmlNode link in content)
                {
                    noticia = $"{noticia} {link.InnerText}";
                }

                noticia = noticia.Replace("'", "");
                noticia = noticia.Replace("”", "");
                noticia = noticia.Replace("\"", "");
                noticia = noticia.Replace("&nbsp", "");
                noticia = noticia.Replace("\n", "");
                noticia = noticia.Replace("\t", "");
                noticia = noticia.Replace("         ", "");
                
                using (SQLiteConnection c = new SQLiteConnection(Retorna_sql_conn()))
                {
                    c.Open();
                    string sql = $" UPDATE url_news SET    titulo = '{titulo}',   noticia = '{noticia}'  WHERE url = '{url}' ";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static string Retorna_sql_conn()
        {
            var CurrentDirectory = Directory.GetCurrentDirectory();
            string conn = $"Data Source=" + CurrentDirectory + "\\banco.db;";
            return conn;
        }
        public static void Captura_url()
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = hw.Load("https://olhardigital.com.br/");
            List<string> list_url = new List<string>();
            HtmlNodeCollection URL = doc.DocumentNode.SelectNodes("//a[@class='card-post type1 img-effect1']");

            foreach (HtmlNode link in URL)
            {
                // Get the value of the HREF attribute
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                list_url.Add(hrefValue);
            }
            Insere_url_DB(list_url);
        }
    }
}