using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net;

namespace MyNamespace
{
    class Scrap_News
    {
        static void Main(string[] args)
        {//https://www.youtube.com/watch?v=CA7Hafn3Pow&ab_channel=RaphaelTerciniDev
         //http://www.andrealveslima.com.br/blog/index.php/2016/09/28/trabalhando-com-sqlite-no-c-e-vb-net/


            /*string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("user-data-dir-" + path + "ChromeDriver\\Cache");
            ChromeDriver driver = new ChromeDriver(path, options);
            driver.Navigate().GoToUrl("https://olhardigital.com.br/");
            var title = driver.FindElements(By.XPath("//a[@class='card-post type1 img-effect1']"));


            List<string> list_url = new List<string>();   

            foreach (var item in title)
            {
                list_url.Add(item.GetAttribute("href"));
                
                Console.WriteLine($"- > " + item.GetAttribute("href"));
            }

            //Insere_url_DB(list_url, "OD");
            //Procura_news_OD(driver);
            driver.Quit();*/   



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

            //Insere_url_DB(list_url, "OD");
            Processa_url();
            

        }
        public static System.Data.SQLite.SQLiteConnection Conecta_banco()
        {
            var CurrentDirectory = Directory.GetCurrentDirectory();

            var conn = new System.Data.SQLite.SQLiteConnection($"Data Source=" + CurrentDirectory + "\\banco.db;");
            
            conn.Open();
            
            return conn;
        }
        public static void Insere_url_DB(List<string> list, string origem)
        {
            var conn = Conecta_banco();

            using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
            {
                foreach (var item in list)
                {
                    comm.CommandText = $"INSERT INTO url_news (url,site) VALUES ('{item}','{origem}' )";
                    comm.ExecuteNonQuery();
                }                
            }
        }
        public static void Processa_url()
        {
            var conn = Conecta_banco();

            using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
            {
                comm.CommandText = $"SELECT * FROM url_news";
               
                using (var reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["site"].ToString() == "OD")
                        {
                            Carrega_news_OD(reader["url"].ToString());
                        }                
                    }
                }
            }

        }
        public static void Carrega_news_OD(string url)
        {
            //titulo
            //*[@id="singleMain"]/div[1]/div[1]/div[1]/h1

            //noticia
            //*[@id="singleMain"]/div[1]/div[1]/div[3]/article

            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = hw.Load(url);
            string titulo = "";
            string noticia = "";

            HtmlNodeCollection content = doc.DocumentNode.SelectNodes("//*[@id='singleMain']/div[1]/div[1]/div[1]/h1");

            foreach (HtmlNode link in content)
            {
                titulo= link.InnerHtml;
            }

            content = doc.DocumentNode.SelectNodes("//*[@id='singleMain']/div[1]/div[1]/div[3]/article/div[1]/p");

            foreach (HtmlNode link in content)
            {
                noticia = $"{noticia} {link.InnerText}";
            }

            //implementar update. <-------------------

            Console.ReadLine();
        }
    }
}