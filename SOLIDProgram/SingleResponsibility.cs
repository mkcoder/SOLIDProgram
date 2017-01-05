using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOLIDProgram
{
    public class Product
    {
        public static int ProductId { get; private set; }
        public int SKU { get; set; }
        public Boolean SaleItem { get; set; }
        public decimal Cost { get; set; }
        public Category Category { get; set; }
        public String Name { get; set; }

        public Product()
        {
            ProductId = ++ProductId;
        }

        public override string ToString()
        {
            return $"[ {SKU} -- {Name} -- ${Cost} ==> {SaleItem} ]";
        }
    }

    public class Category
    {
        public CategoryEnum CateogryId { get; private set; }
        public String Name { get; private set; }

        public enum CategoryEnum
        {
            CLOTHES,
            JEWLERY,
            SHOES,
            FOOD,
            ELECTRONICS,
            BEAUTY
        }

        public static class CategoryFactory
        {
            private static readonly List<Category> categories = new List<Category>()
            {
                new Category() { CateogryId = CategoryEnum.CLOTHES, Name = "clothes" },
                new Category() { CateogryId = CategoryEnum.JEWLERY, Name = "jewlery" },
                new Category() { CateogryId = CategoryEnum.SHOES, Name = "shoes" },
                new Category() { CateogryId = CategoryEnum.FOOD, Name = "food" },
                new Category() { CateogryId = CategoryEnum.ELECTRONICS, Name = "electronic" },
                new Category() { CateogryId = CategoryEnum.BEAUTY, Name = "beauty" }
            };

            public static Category getCategoryFor(CategoryEnum category) => categories.Find(categories => categories.CateogryId == category);
        }

    }

    public class Cart
    {
        public int CartId { get; set; }
        public List<Product> ShoppingCart { get; set; } = new List<Product>();
        public decimal Total { get; set; }


        internal void addItemToCart(Product product)
        {
            ShoppingCart.Add(product);
            Total += product.Cost;
        }

        internal void printCart()
        {
            Console.WriteLine("-----");
            foreach (var item in ShoppingCart)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class Shopper
    {
        public Cart Cart { get; private set; }
        public String Name { get; set; }

        public Shopper(Cart cart, String name)
        {
            this.Cart = cart;
            this.Name = name;
        }

        public void addItemToCart(Product product) => Cart.addItemToCart(product);

        internal void whatInMyCart() => Cart.printCart();        
    }

    public static class Checkout
    {
        public static readonly List<Shopper> _queue = new List<Shopper>();
        public static void enterLine(Shopper shopper) => _queue.Add(shopper);
        
        public static void startCheckout()
        {
            while (true)
            {
                lock (_queue)
                {
                    Shopper shopper = _queue.FirstOrDefault();
                    if (shopper == null) return;
                    shopper.whatInMyCart();
                    Console.WriteLine($"{shopper.Name} purchased {shopper.Cart.Total}");
                    _queue.Remove(shopper);
                }
            }
        }        
    }


    /**
    * summary     
    **/
    public class SingleResponsibility : IDemo
    {       
        public void demo()
        {
            Thread thread = new Thread(new ThreadStart(Checkout.startCheckout));
            thread.Start();

            List<Product> products = getListOfProducts();
            Shopper muhammad = new Shopper(new Cart(), "muhammad");
            muhammad.addItemToCart(products[3]);
            muhammad.addItemToCart(products[3]);
            muhammad.addItemToCart(products[5]);
            muhammad.addItemToCart(products[2]);
            muhammad.addItemToCart(products[1]);

            Shopper bob = new Shopper(new Cart(), "bob");
            bob.addItemToCart(products[4]);

            Checkout.enterLine(muhammad);
            Checkout.enterLine(bob);

        }

        private List<Product> getListOfProducts()
        {
            return new List<Product>()
            {
                new Product() { SKU=1234,Category=Category.CategoryFactory.getCategoryFor(Category.CategoryEnum.CLOTHES), Cost = 12.23m, SaleItem=false,Name="black short sleeve shirt" },
                new Product() { SKU=798,Category=Category.CategoryFactory.getCategoryFor(Category.CategoryEnum.CLOTHES), Cost = 1.99m, SaleItem=false,Name="blue long sleeve shirt" },
                new Product() { SKU=1644,Category=Category.CategoryFactory.getCategoryFor(Category.CategoryEnum.FOOD), Cost = 2.99m, SaleItem=false,Name="sugar cookies" },
                new Product() { SKU=3698,Category=Category.CategoryFactory.getCategoryFor(Category.CategoryEnum.BEAUTY), Cost = 10.52m, SaleItem=false,Name="face moisturizer" },
                new Product() { SKU=78633,Category=Category.CategoryFactory.getCategoryFor(Category.CategoryEnum.SHOES), Cost = 8.99m, SaleItem=false,Name="damn daniel" },
                new Product() { SKU=798544,Category=Category.CategoryFactory.getCategoryFor(Category.CategoryEnum.JEWLERY), Cost = 6.03m, SaleItem=false,Name="gold neckles" }
            };
        }
    }

}
