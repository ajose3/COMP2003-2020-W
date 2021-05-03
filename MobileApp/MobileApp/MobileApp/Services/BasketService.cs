using MobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.Services
{
    public static class BasketService
    {
        static SQLiteAsyncConnection db;
        public static async Task Init()
        {
            if (db != null)
            {
                //await db.DropTableAsync<BasketProduct>();
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Basket.db");
            
            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<BasketProduct>();
        }
        public static async Task AddProduct(Product product)
        {
            await Init();
            BasketProduct bProduct = new BasketProduct(product,1);
            //check if products exist
            string query = "SELECT * FROM BasketProduct where id = @id";
            query = query.Replace("@id", bProduct.Id.ToString());
            var getProductsbyId =  await db.QueryAsync<BasketProduct>(query);
            int total = getProductsbyId.Count;
            if (total>=1)
            {
                int quantity = getProductsbyId[0].Quantity + 1;
                query = "Update BasketProduct set quantity = @quantity where id = @id";
                query = query.Replace("@id", bProduct.Id.ToString()).Replace("@quantity", quantity.ToString());
                await db.QueryAsync<BasketProduct>(query);
            }
            else
            {
                query = "Insert INTO BasketProduct (Id,Name,Description, Price, ImageUrl, Stock, Quantity) values (@id,'@name','@desc',@price,'@Img',@stock,@quantity);";
                query = query.Replace("@basketId", bProduct.BasketId.ToString()).Replace("@id", bProduct.Id.ToString()).Replace("@name", bProduct.Name).Replace("@desc", bProduct.Description).Replace("@price", bProduct.Price.ToString()).Replace("@Img", bProduct.ImageUrl).Replace("@stock", bProduct.Stock.ToString()).Replace("@quantity", bProduct.Quantity.ToString());

                await db.QueryAsync<BasketProduct>(query);
            }
        }
        public static async Task RemoveProduct(Product product)
        {
            await Init();
            //check if products exist
            string query = "SELECT * FROM BasketProduct where id = @id";
            query = query.Replace("@id", product.Id.ToString());
            var getProductsbyId = await db.QueryAsync<BasketProduct>(query);
            int total = getProductsbyId.Count;
            if (total >= 1)
            {
                int quantity = getProductsbyId[0].Quantity;
                if (quantity > 1)
                {
                    quantity -= 1;
                    query = "Update BasketProduct set quantity = @quantity where id = @id";
                    query = query.Replace("@id", product.Id.ToString()).Replace("@quantity", quantity.ToString());
                    await db.QueryAsync<BasketProduct>(query);
                }
                else
                {
                    query = "DELETE FROM BasketProduct where id = @id";
                    query = query.Replace("@id", product.Id.ToString());
                    await db.QueryAsync<BasketProduct>(query);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error Product Not in basket", null, "OK");
            }
        }
        public static async Task<IEnumerable<BasketProduct>> GetProducts()
        {
            await Init();

            var basket = await db.Table<BasketProduct>().ToListAsync();

            return basket;
        }
        public static async Task ClearProducts()
        {
            await Init();

            await db.DeleteAllAsync<BasketProduct>();
        }
    }
}
