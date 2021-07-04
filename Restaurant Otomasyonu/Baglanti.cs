namespace Restaurant
{
    using System.Data;
    using System.Data.SqlClient;
   public class Baglanti
    {
       
    public static SqlConnection bag()

      {
          SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=ADS;Integrated Security=true");

          if (cnn.State == ConnectionState.Closed) cnn.Open();
          //{
          //    cnn.Open();
          //}
        
          return cnn;
         
      }

        //public static SqlConnection bag()
        //{
        //    SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Integrated Security=True; Initial Catalog=ADS");
        //        con.Open();
        //        if (con.State == ConnectionState.Open)
        //        {

        //            return con;
        //        }
        //        return null;
        //}
        
    }
}
