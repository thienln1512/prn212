using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var categories = new List<Category>();
            try
            {
                categories.Add(new Category(1, "Beverages"));
                categories.Add(new Category(2, "Condiments"));
                categories.Add(new Category(3, "Confections"));
                categories.Add(new Category(4, "Dairy Products"));
                categories.Add(new Category(5, "Grains/Cereals"));
                categories.Add(new Category(6, "Meat/Poultry"));
                categories.Add(new Category(7, "Produce"));
                categories.Add(new Category(8, "Seafood"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return categories;
        }
    }
}







