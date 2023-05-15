using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ProductMangnt_App
{
    class ProductManagement
    {        
        public void AddNewProduct(SqlConnection con)
        {
            string name = " ";
            string description = " ";
            int quantity = 0;
            int price = 0;
            try
            {
                Console.WriteLine("Enter ProductName: ");
                name = Console.ReadLine();
                Console.WriteLine("Enter ProductDescription: ");
                description = Console.ReadLine();
                Console.Write("Enter Quantity: ");
                quantity = Convert.ToInt16(Console.ReadLine());
                Console.Write("Enter price: ");
                price = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter correct details to Add Product");
            }

            SqlDataAdapter adp = new SqlDataAdapter("Select * from Products", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            var row = ds.Tables[0].NewRow();
            row["ProductName"] = name;
            row["ProductDiscp"] = description;
            row["Quantity"] = quantity;
            row["Price"] = price;

            ds.Tables[0].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);

            Console.WriteLine("Product added successfully");
            
        }
        public void getProduct(SqlConnection con)
        {
            int id = 0;
            try
            {
                Console.WriteLine("Enter the Id to update Product:");
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter only Numbers form which Id you want to Viewproduct");
            }

            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Products where ProductId ={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds == null)
            {
                Console.WriteLine("Product with specified id does not exists");
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        Console.WriteLine($"{column.ColumnName}: {row[column]}");
                    }
                }
            }
        }
        public void ViewAllProducts(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Products", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds == null)
            {
                Console.WriteLine("Product does not exists");
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        Console.WriteLine($"{column.ColumnName}: {row[column]}");
                    }
                }
            }
        }
        public void UpdateProduct(SqlConnection con)
        {
            int id = 0;
            try
            {
                Console.WriteLine("Enter the Id to update Product:");
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter only Numbers form which Id you want to update");
            }

            string selectQuery = $"SELECT * FROM Products where ProductId = {id}";
            
            SqlDataAdapter adp = new SqlDataAdapter(selectQuery, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Console.WriteLine("Enter ProductName: ");
                string newname = Console.ReadLine();
                Console.WriteLine("Enter ProductDescription: ");
                string newdescription = Console.ReadLine();
                Console.Write("Enter Quantity: ");
                int newquantity = Convert.ToInt16(Console.ReadLine());
                Console.Write("Enter price: ");
                int newprice = Convert.ToInt32(Console.ReadLine());

                ds.Tables[0].Rows[0]["ProductName"] = newname;
                ds.Tables[0].Rows[0]["ProductDiscp"] = newdescription;
                ds.Tables[0].Rows[0]["Quantity"] = newquantity;
                ds.Tables[0].Rows[0]["Price"] = newprice;

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);

                Console.WriteLine("Product updated successfully");
            }
            else
            {
                Console.WriteLine($"No Product found with Id = {id}");
            }

        }
        public void DeleteProduct(SqlConnection con)
        {
            int id = 0;
            try
            {
                Console.WriteLine("Enter Product Id you want to delete");
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter only Numbers form which Id you want to delete");
            }
            string selectQuery = $"select * FROM Products where ProductId = {id}";

            SqlDataAdapter adp = new SqlDataAdapter(selectQuery, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Rows[0].Delete();

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);

                Console.WriteLine("Product Deleted successfully");
            }
            else
            {
                Console.WriteLine($"No Product found with Id = {id}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data source=IN-DQ3K9S3; Initial Catalog=ProductManagement; Integrated Security=true");
            ProductManagement app = new ProductManagement();

            while (true)
            {
                Console.WriteLine("Welcome To ProductManagement App");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. View Product");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product");

                int Choice = 0;
                try
                {
                    Console.WriteLine("Enter Your choice: ");
                    Choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter only Numbers from(1-5)");
                }
                switch (Choice)
                {
                    case 1:
                        {
                            app.AddNewProduct(con);
                            break;
                        }
                    case 2:
                        {
                            app.getProduct(con);
                            break;
                        }
                    case 3:
                        {
                            app.ViewAllProducts(con);
                            break;
                        }
                    case 4:
                        {
                            app.UpdateProduct(con);
                            break;
                        }
                    case 5:
                        {
                            app.DeleteProduct(con);
                            break;
                        }        
                }
                Console.WriteLine();
            }
        }
    }
}