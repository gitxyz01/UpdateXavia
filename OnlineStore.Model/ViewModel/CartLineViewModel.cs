using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class CartLineViewModel
    {
        public ecom_Products Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        public List<CartLineViewModel> LineCollection { get; set; }
        public Cart()
        {
            LineCollection = new List<CartLineViewModel>();
        }
        public void Add(ecom_Products product, int quantity)
        {
            var model = LineCollection.FirstOrDefault(x => x.Product.Id == product.Id);
            if (model == null)
            {
                CartLineViewModel line = new CartLineViewModel() { Product = product, Quantity = quantity };
                LineCollection.Add(line);
            }
            else
            {
                model.Quantity += 1;
            }
        }

        public void Delete(ecom_Products product)
        {
            var line = LineCollection.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line != null)
            {
                LineCollection.Remove(line);
            }

        }
        public void Update(ecom_Products product, int quantity)
        {
            var line = LineCollection.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line != null)
            {
                line.Quantity = quantity;
            }
        }
        public decimal? ComputerTotal()
        {
            decimal? total = 0;
            foreach (var item in LineCollection)
            {
                    total += item.Quantity * item.Product.Price;
            }
            return total;
        }
        public void Clear()
        {
            LineCollection.Clear();
        }
        public decimal ComputerTotalQuantity()
        {
            return LineCollection
                .Sum(x => x.Quantity)
                ;
        }
    }
}
