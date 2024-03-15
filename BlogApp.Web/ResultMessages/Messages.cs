namespace BlogApp.Web.ResultMessages;

public class Messages
{
    public static class Article
    {
        public static string Add(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarılı bir şekilde eklendi";
        }
        
        public static string Update(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarılı bir şekilde güncellendi";
        }
    }    
}