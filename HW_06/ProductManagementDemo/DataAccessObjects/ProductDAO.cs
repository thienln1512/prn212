﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static List<Product> listProducts;

        public ProductDAO()
        {
            Product chai = new Product(1, "Chai", 3, 12, 18);
            Product chang = new Product(2, "Chang", 1, 23, 19);
            Product aniseed = new Product(3, "Aniseed Syrup", 2, 23, 10);
            listProducts = new List<Product> { chai, chang, aniseed };

            // Ví dụ thêm các sản phẩm khác (hiện đang comment lại)
            //Product chef        = new Product(4,  "Chef Anton's Cajun Seasoning",     2, 34, 22);
            //Product chefMix     = new Product(5,  "Chef Anton's Gumbo Mix",           2, 45, 34);
            //Product grandma     = new Product(6,  "Grandma's Boysenberry Spread",     2, 21, 25);
            //Product uncle       = new Product(7,  "Uncle Bob's Organic Dried Pears",  7, 22, 30);
            //Product northwoods  = new Product(8,  "Northwoods Cranberry Sauce",       2, 10, 40);
            //Product mishi       = new Product(9,  "Mishi Kobe Niku",                  6, 12, 97);
            //Product ikura       = new Product(10, "Ikura",                            8, 13, 32);
            //listProducts = new List<Product> { chai, chang, aniseed, chef, chefMix, grandma, uncle, northwoods, mishi, ikura };
        }

        // Lấy danh sách tĩnh đã khởi tạo từ constructor
        public List<Product> GetProducts()
        {
            return listProducts;
        }

        // Lấy danh sách thực từ database qua Entity Framework
        //public static List<Product> GetProductsFromDb()
        //{
        //    var listProducts = new List<Product>();
        //    try
        //    {
        //        using var db = new MyStoreContext();
        //        listProducts = db.Products.ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        // TODO: xử lý logging nếu cần
        //    }
        //    return listProducts;
        //}

        public void SaveProduct(Product p)
        {
            listProducts.Add(p);
        }

        public void UpdateProduct(Product product)
        {
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    p.ProductId = product.ProductId;
                    p.ProductName = product.ProductName;
                    p.UnitPrice = product.UnitPrice;
                    p.UnitsInStock = product.UnitsInStock;
                    p.CategoryId = product.CategoryId;
                }
            }
        }

        public void DeleteProduct(Product product)
        {
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    listProducts.Remove(p);
                    break;
                }
            }
        }

        public Product GetProductById(int id)
        {
            foreach (Product p in listProducts)
            {
                if (p.ProductId == id)
                    return p;
            }
            return null;
        }
    }
}
