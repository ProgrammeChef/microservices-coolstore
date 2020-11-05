using System;
using System.Threading.Tasks;
using N8T.Infrastructure.App.Dtos;
using ShoppingCartService.Domain.Exception;
using ShoppingCartService.Domain.Gateway;
using ShoppingCartService.Domain.Service;

namespace ShoppingCartService.Infrastructure.Extensions
{
    public static class CartDtoExtensions
    {
        public static async Task<CartDto> InsertItemToCartAsync(this CartDto cart,
            int quantity, Guid productId, IProductCatalogService productCatalogService)
        {
            var item = new CartItemDto {Quantity = quantity, ProductId = productId};

            var product = await productCatalogService.GetProductByIdAsync(productId);
            if (product is not null)
            {
                item.ProductName = product.Name;
                item.ProductPrice = product.Price;
                item.ProductImagePath = product.ImageUrl;
                item.ProductDescription = product.Description;
                item.InventoryId = product.Inventory.Id;
                item.InventoryLocation = product.Inventory.Location;
                item.InventoryWebsite = product.Inventory.Website;
                item.InventoryDescription = product.Inventory.Description;
            }

            cart.Items.Add(item);

            return cart;
        }

        public static async Task<CartDto> CalculateCartAsync(this CartDto cart,
            IProductCatalogService productCatalogService,
            IShippingGateway shippingGateway,
            IPromoGateway promoGateway)
        {
            if (cart.Items.Count > 0)
            {
                cart.CartItemTotal = 0.0D;
                foreach (var cartItemTemp in cart.Items)
                {
                    var product = await productCatalogService.GetProductByIdAsync(cartItemTemp.ProductId);
                    if (product is null)
                    {
                        throw new ProductNotFoundException(cartItemTemp.ProductId);
                    }

                    cart.CartItemPromoSavings += cartItemTemp.PromoSavings * cartItemTemp.Quantity;
                    cart.CartItemTotal += product.Price * cartItemTemp.Quantity;
                }

                shippingGateway.CalculateShipping(cart);
            }

            promoGateway.ApplyShippingPromotions(cart);

            cart.CartTotal = cart.CartItemTotal + cart.ShippingTotal;

            return cart;
        }
    }
}
