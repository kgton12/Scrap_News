using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace MyNamespace
{
    class Scrap_News
    {
        static void Main(string[] args)
        {//https://www.youtube.com/watch?v=CA7Hafn3Pow&ab_channel=RaphaelTerciniDev
            //http://www.andrealveslima.com.br/blog/index.php/2016/09/28/trabalhando-com-sqlite-no-c-e-vb-net/


            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("user-data-dir-" + path + "ChromeDriver\\Cache");
            ChromeDriver driver = new ChromeDriver(path, options);
            driver.Navigate().GoToUrl("https://olhardigital.com.br/");

            //var title = driver.FindElements(By.XPath("//div[@class='post-title']"));
            var title = driver.FindElements(By.XPath("//a[@class='card-post type1 img-effect1']")); //GetAttribute("href");//Where(el => el.Text.Contains("href"));
            //var title = driver.FindElements(By.ClassName("post-title"));
            //var title = driver.FindElements(By.XPath("//*[@id='body']/main/section[4]//div[@class='post-title']"));


            List<string> list_url = new List<string>();


            foreach (var item in title)
            {
                list_url.Add(item.GetAttribute("href"));
                
                Console.WriteLine($"- > " + item.GetAttribute("href"));
            }

            //Insere_url(list_url, "OD");
            Procura_news_OD(driver);

        }
        public static System.Data.SQLite.SQLiteConnection Conecta_banco()
        {
            var CurrentDirectory = Directory.GetCurrentDirectory();

            var conn = new System.Data.SQLite.SQLiteConnection($"Data Source=" + CurrentDirectory + "\\banco.db;");
            
            conn.Open();
            
            return conn;
        }
        public static void Insere_url(List<string> list, string origem)
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
        public static void Procura_news_OD(ChromeDriver driver)
        {
  
        }
        
    }

}