using techBar.Models;

namespace techBar.Data
{
	public class EcomDbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope=applicationBuilder.ApplicationServices.CreateScope())
			{
				var context=serviceScope.ServiceProvider.GetService<EcomDbContext>();

				context.Database.EnsureCreated();

				//Sellers
				if(!context.Sellers.Any())
				{
					context.Sellers.AddRange(new List<Sellers>()
					{
						new Sellers()
						{
							logo = "https://images.pexels.com/photos/6888761/pexels-photo-6888761.jpeg",
							SellerName = "Mukesh Kumar",
							Description = "Mukesh is the owner of a small business. \nHe sells products like shoes as well as electronic gadgets. \nLocated in Kitchener, ON"
                        },
						new Sellers()
						{
							logo = "https://images.pexels.com/photos/7552577/pexels-photo-7552577.jpeg",
							SellerName = "Alexandra Botez",
							Description = "Alexandra is a full-time Application developer and part-time product seller; she ownes 'DezShop,' \nWhere she sells Samsung product accessories and mobile phones."
                        },
						new Sellers()
						{
							logo = "https://images.pexels.com/photos/6205769/pexels-photo-6205769.jpeg",
							SellerName = "Lin Yang",
							Description = "Lin Yang is one of our oldest sellers; \nshe also sells products in Canada and exports them outside Canada."
                        },
						new Sellers()
						{
							logo = "https://images.pexels.com/photos/8422729/pexels-photo-8422729.jpeg", 
							SellerName = "David beckham",
							Description = "David is running his company which make Apple products accessories."
                        },
						new Sellers()
						{
							logo = "https://images.pexels.com/photos/8475165/pexels-photo-8475165.jpeg",
							SellerName = "Paul Williams",
							Description = "Paul is known for its on-time delivery. With guaranteed quality products."
                        },
					});
					context.SaveChanges();
				}
				//Product
				if (!context.Products.Any())
				{
					context.Products.AddRange(new List<Product>()
					{
						new Product()
						{
							FullName = "Google Pixel 6",
							Bio = "For 5G and network compatible-to ensure that this phone can be used with your carrier, please check to make sure that the phone supports the 5G-enabled frequency used by your carrier.",
							ProfilePitctureURL = "https://m.media-amazon.com/images/I/81KYXXysrUL._AC_SX679_.jpg"

                        },
						new Product()
						{
							FullName = "Asus VG248QG 24” Screen",
							Bio = "Blue Light Filter,Flicker-Free,Height Adjustment,Pivot Adjustment,Swivel Adjustment,Tilt Adjustment,Eye care,G-SYNC Compatible",
							ProfilePitctureURL = "https://m.media-amazon.com/images/I/81MLtyTxeAL._AC_SX679_.jpg"
                        },
						new Product()
						{
							FullName = "Apple Watch Ultra",
							Bio = "WHY APPLE WATCH ULTRA — Rugged and capable, built to meet the demands of endurance athletes, outdoor adventurers and water sports enthusiasts — with a specialized band for each. Up to 36 hours of battery life, plus all the Apple Watch features that help you stay healthy, safe and connected.",
							ProfilePitctureURL = "https://m.media-amazon.com/images/I/81gw2b5Lo4L._AC_SX679_.jpg"
                        },
						new Product()
						{
							FullName = "Samsung Galaxy Watch5 Pro 45mm",
							Bio = "Stay up to date on your wellness with the Galaxy Watch5 Series. Get a more accurate heart rate thanks to an improved, curved Samsung BioActive Sensor that gets closer to your skin.",
							ProfilePitctureURL = "https://m.media-amazon.com/images/I/61Sl+xoVHoL._AC_SX679_.jpg"
                        },
						new Product()
						{
							FullName = "Microsoft Surface Pro 7",
							Bio = "Next-Gen, best-in-class laptop with the versatility of a studio and tablet, so you can type, touch, draw, write, work, and play more naturally",
							ProfilePitctureURL = "https://m.media-amazon.com/images/I/71kBlSKi3eL._AC_SX679_.jpg"
                        }
					});
					context.SaveChanges();
				}

				//Manufacturer
				if (!context.Manufacturers.Any())
				{
					context.Manufacturers.AddRange(new List<Manufacturer>()
					{
						new Manufacturer()
						{
							FullName = "Apple",
							Bio = "One of oldest and leading tech company",
							ProfilePitctureURL = "https://images.unsplash.com/photo-1614312385003-dcea7b8b6ab6?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1326&q=80"

                        },
						new Manufacturer()
						{
							FullName = "Samsung",
							Bio = "Together for tomorrow",
							ProfilePitctureURL = "https://images.samsung.com/is/image/samsung/assets/global/about-us/brand/logo/720_600_1.png?$720_N_PNG$"
                        },
                        new Manufacturer()
                        {
                            FullName = "Asus",
                            Bio = "Connectiong People",
                            ProfilePitctureURL = "https://seeklogo.com/images/A/Asus-logo-A5705FDB70-seeklogo.com.png"
                        },
						new Manufacturer()
						{
							FullName = "Microsoft",
							Bio = "We believe in what people make possible",
							ProfilePitctureURL = "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RWCZER?ver=1433&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"
                        },
						new Manufacturer()
						{
							FullName = "Google",
							Bio = "Make life easier with a little help from our products.",
							ProfilePitctureURL = "https://companieslogo.com/img/orig/GOOG-0ed88f7c.png?t=1633218227"
                        }
                    });
					context.SaveChanges();
				}

				//ProductCategory
				if (!context.ProductCategories.Any())
				{
					context.ProductCategories.AddRange(new List<ProductsCategory>()
					{
						new ProductsCategory()
						{
							Name = "Asus VG248QG 24” Screen",
							Description = "Eye protection, G-Sync, wirless connect",
							Price = 389.88,
							ImageURL = "https://m.media-amazon.com/images/I/81MLtyTxeAL._AC_SX679_.jpg",
							StartDate = DateTime.Now.AddDays(-10),
							EndDate = DateTime.Now.AddDays(10),
							SellerId = 3,
							ManufacturerId = 3,
							ProductCategory = Enums.ProductCategory.ComputerAccessories
						},
						new ProductsCategory()
						{
                            Name = "Google Pixel 6",
                            Description = "First 5G phone, Google Pixel 6",
                            Price = 899.97,
                            ImageURL = "https://m.media-amazon.com/images/I/81KYXXysrUL._AC_SX679_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            SellerId = 3,
                            ManufacturerId = 3,
                            ProductCategory = Enums.ProductCategory.ComputerAccessories
                        },
						new ProductsCategory()
						{
                            Name = "Apple Watch Ultra",
                            Description = "WHY APPLE WATCH ULTRA",
                            Price = 999.99,
                            ImageURL = "https://m.media-amazon.com/images/I/81gw2b5Lo4L._AC_SX679_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            SellerId = 3,
                            ManufacturerId = 3,
                            ProductCategory = Enums.ProductCategory.ComputerAccessories
                        },
						new ProductsCategory()
						{
                            Name = "Samsung Galaxy Watch5 Pro 45mm",
                            Description = "Stay up to date on your wellness with the Galaxy Watch5 Series.",
                            Price = 788.88,
                            ImageURL = "https://m.media-amazon.com/images/I/61Sl+xoVHoL._AC_SX679_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            SellerId = 3,
                            ManufacturerId = 3,
                            ProductCategory = Enums.ProductCategory.ComputerAccessories
                        },
						new ProductsCategory()
						{
                            Name = "Microsoft Surface Pro 7",
                            Description = "best-in-class laptop with the versatility of a studio and tablet.",
                            Price = 1288,
                            ImageURL = "https://m.media-amazon.com/images/I/71kBlSKi3eL._AC_SX679_.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            SellerId = 3,
                            ManufacturerId = 3,
                            ProductCategory = Enums.ProductCategory.ComputerAccessories
                        },
						new ProductsCategory()
						{
                            Name = "iPhone 14 pro",
                            Description = "6.1 Inch Amolad display, more than 100 million colors.",
                            Price = 1799,
                            ImageURL = "https://www.interdiscount.ch/static-shops/products/720/d0f7e052cddc1185494fa4d2037f9122f3e5.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            SellerId = 3,
                            ManufacturerId = 3,
                            ProductCategory = Enums.ProductCategory.ComputerAccessories
                        }
					});
					context.SaveChanges();
				}

				if (!context.Product_Categories.Any())
				{
					context.Product_Categories.AddRange(new List<Product_Category>()
					{
						new Product_Category()
						{
							ProductId = 1,
							CategoryId = 1
						},
						new Product_Category()
						{
							ProductId = 3,
							CategoryId = 1
						},

						 new Product_Category()
						{
							ProductId = 1,
							CategoryId = 2
						},
						 new Product_Category()
						{
							ProductId = 4,
							CategoryId = 2
						},

						new Product_Category()
						{
							ProductId = 1,
							CategoryId = 3
						},
						new Product_Category()
						{
							ProductId = 2,
							CategoryId = 3
						},
						new Product_Category()
						{
							ProductId = 5,
							CategoryId = 3
						},


						new Product_Category()
						{
							ProductId = 2,
							CategoryId = 4
						},
						new Product_Category()
						{
							ProductId = 3, 
							CategoryId = 4
						},
						new Product_Category()
						{
							ProductId = 4, 
							CategoryId = 4
						},


						new Product_Category()
						{
							ProductId = 2, 
							CategoryId = 5
						},
						new Product_Category()
						{
							ProductId = 3, 
							CategoryId = 5
						},
						new Product_Category()
						{
							ProductId = 4, 
							CategoryId = 5
						},
						new Product_Category()
						{
							ProductId = 5, 
							CategoryId = 5
						},


						new Product_Category()
						{
							ProductId = 3, 
							CategoryId = 6
						},
						new Product_Category()
						{
							ProductId = 4, 
							CategoryId = 6
						},
						new Product_Category()
						{
							ProductId = 5, 
							CategoryId = 6
						},
					});
					context.SaveChanges();
				}
			}
		}
	}
}
