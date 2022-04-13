using HtmlAgilityPack;
using System;
using System.Data.SQLite;

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

            Insere_url_DB(list_url, "OD");
            Procura_news_OD(driver);
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
            Insere_url_DB(list_url, "OD");
            Processa_url();       
        }
        public static void Insere_url_DB(List<string> list, string origem)
        {
            using (SQLiteConnection c = new SQLiteConnection(Retorna_sql_conn()))
            {
                c.Open();
                foreach (var item in list)
                { 
                    string sql = $"INSERT INTO url_news (url,site) VALUES ('{item}','{origem}' )";
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
                List<string> list_url_OD = new List<string>();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["site"].ToString() == "OD")
                            {
                                list_url_OD.Add(reader["url"].ToString());
                                //Carrega_news_OD(reader["url"].ToString());
                            }
                        }
                    }
                }
                Carrega_news_OD(list_url_OD);
            }
        }
        public static void Carrega_news_OD(List<string> urls)
        {

            foreach (var url in urls)
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = new HtmlDocument();
                doc = hw.Load(url);
                string titulo = "";
                string noticia = "";

                HtmlNodeCollection content = doc.DocumentNode.SelectNodes("//*[@id='singleMain']/div[1]/div[3]/div/h1");//*[@id='singleMain']/div[1]/div[1]/div[1]/h1

                foreach (HtmlNode link in content)
                {
                    titulo = link.InnerHtml;
                }
                //content = doc.DocumentNode.SelectNodes("//*[@id='singleMain']/div[2]/div[1]/div[1]/article/div[1]/p");//*[@id="singleMain"]/div[2]/div[1]/div[1]/article/div[2]
                
                content = doc.DocumentNode.SelectNodes(@"//div[@class='post-content']");
                
                //estudar para n pegar mais o caminho xpath
                //$('.post-content').innerText
                foreach (HtmlNode link in content)
                {
                    noticia = $"{noticia} {link.InnerText}";
                }

                noticia = noticia.Replace("'", "");
                noticia = noticia.Replace("”", "");
                noticia = noticia.Replace("\"", "");
                noticia = noticia.Replace("&nbsp", "");


                using (SQLiteConnection c = new SQLiteConnection(Retorna_sql_conn()))
                {
                    c.Open();
                    string sql = $" UPDATE url_news SET    titulo = {titulo},   noticia = {noticia}  WHERE url = '{url}' ";
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
    }
}