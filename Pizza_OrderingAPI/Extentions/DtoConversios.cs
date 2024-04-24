using Pizza_OrderingAPI.Entities;
using PizzaOrdering.Models.Dtos;
using System.Drawing;

namespace Pizza_OrderingAPI.Extentions
{
    public static class DtoConversios
    {
        public static IEnumerable<ProductMenuDto> ConvertToDto(this IEnumerable<ProductMenu> ProductMenus)
        {
            return (from menu in ProductMenus
                    select new ProductMenuDto
                    {
                        PizzaId = menu.PizzaId,
                        PizzaName = menu.PizzaName,
                        Description = menu.Description,
                        CategoryId = menu.ItemCategory?.Id, 
                        CategoryName = menu.ItemCategory?.CategoryName, 
                        ImageURL = menu.ImageURL,
                        PizzaNum=menu.PizzaNum,
                    
                        Sizes = menu.Sizes.Select(x => new ProductSizeDto
                        {
                            SizeId = x.SizeId,
                            Name = x.Name,
                            Price = x.Price,
                            //Quantity = x.Quantity,

                        }).ToList()
                    });
        }
        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ItemCategory> productCategories)
        {
            return (from productCategory in productCategories
                    select new ProductCategoryDto
                    {
                        Id = productCategory.Id,
                        Name = productCategory.CategoryName,
                    }).ToList();
        }
        public static ProductMenuDto ConvertToDto(this ProductMenu menu)
        {
            return new ProductMenuDto
            {
                PizzaId = menu.PizzaId,
                PizzaName = menu.PizzaName,
                Description = menu.Description,
                CategoryId = menu.ItemCategory?.Id,
                CategoryName = menu.ItemCategory?.CategoryName,
                ImageURL = menu.ImageURL,
                PizzaNum = menu.PizzaNum,             
                Sizes = menu.Sizes.Select(x => new ProductSizeDto
                {
                    SizeId = x.SizeId,
                    Name = x.Name,
                    Price = x.Price,
                    //Quantity = x.Quantity,
                }).ToList()
            };
        }

        public static ExtraItemDto ConvertToDto(this ExtraItem item)
        {
            return new ExtraItemDto
            {
                ExtraItemId = item.ExtraItemId,
                Name = item.Name,
                Price = item.Price
            };
        }
        public static IEnumerable<ExtraItemDto> ConvertToDto(this IEnumerable<ExtraItem> ExtraItem)
        {
            return(from Extritem in ExtraItem select new ExtraItemDto
            {
                ExtraItemId = Extritem.ExtraItemId,
                Name = Extritem.Name,
                Price = Extritem.Price,

            }).ToList();
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> CartItems, IEnumerable<ProductMenu> products, IEnumerable<ProductSize> productSize)
        {
            return (from cartItem in CartItems
                    join product in products on cartItem.ProductId equals product.PizzaId
                    join size in productSize on product.PizzaId equals size.PizzaId
                    select new CartItemDto
                    {
                        Id = cartItem.CartItemId,
                        ProductId = cartItem.ProductId,
                        ProductName = product.PizzaName,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        CartId = cartItem.CartId,
                        //Qty = size.Quantity,
                        Price = size.Price,
                        PizzaSize = size.Name,
                        //TotalPrice = size.Price * size.Quantity,
                        SizeId = size.SizeId,


                    }).ToList();
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, IEnumerable<ProductSize> productSizes, ProductMenu product)
        {
            var productSize = productSizes.FirstOrDefault(ps => ps.PizzaId == cartItem.ProductId);
            if (productSize == null)
            {
                // Handle the case where product size is not found
                throw new Exception("Product size not found.");
            }

            return new CartItemDto
            {
                Id = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                ProductName = product.PizzaName,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                CartId = cartItem.CartId,
                //Qty = productSize.Quantity,
                PizzaSize = productSize.Name,
                Price = productSize.Price,
                //TotalPrice = productSize.Price * productSize.Quantity,
            };
        }
        public static CartItemDto ConvertToDto(this CartItem cartItem, ProductMenu product, ProductSize productSize, IEnumerable<ExtraItem> extraItems)
        {

            return new CartItemDto
            {
                Id = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                CartId = cartItem.CartId,
                Qty = cartItem.Quantity,

                ProductName = product.PizzaName,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,

                Price = productSize.Price,
                TotalPrice = productSize.Price * cartItem.Quantity,
                PizzaSize = productSize.Name,
                SizeId = productSize.SizeId,

                ExtraItems = extraItems.Select(e => new ExtraItemDto
                {
                    ExtraItemId = e.ExtraItemId,
                    Name = e.Name,
                    Price = e.Price
                }).ToList()

                //ExtraItems = extraItems  // Assign the list of extra items to the ExtraItems property
            };
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, ProductMenu product, ProductSize productSize)
            {
                return new CartItemDto
                {
                    Id = cartItem.CartItemId,
                    ProductId = cartItem.ProductId,
                    CartId = cartItem.CartId,
                    ProductName = product.PizzaName,
                    ProductDescription = product.Description,
                    ProductImageURL = product.ImageURL,
                    Price = productSize.Price,
                    Qty = cartItem.Quantity,
                    TotalPrice = productSize.Price * cartItem.Quantity,
                    PizzaSize = productSize.Name,
                    SizeId = productSize.SizeId,
                };
            }
     


        public static ProductSizeDto ConvertToDto( this ProductSize productSize)
        {
            return new ProductSizeDto
            {
                Name = productSize.Name,
                Price = productSize.Price,
                //Quantity = productSize.Quantity,
                SizeId = productSize.SizeId,
                
            };
        }



        public static CartItemDto ConvertToDto( this CartItem cartItem)
        {
            return new CartItemDto
            {
                Id = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                CartId = cartItem.CartId,
                Qty = cartItem.Quantity,
                SizeId = cartItem.SizeId,
                //TotalPrice = cartItem.Price * cartItem.Quantity,
            };
        }




    }
}

