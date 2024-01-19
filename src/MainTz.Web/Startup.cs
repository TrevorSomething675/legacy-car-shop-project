﻿using Microsoft.EntityFrameworkCore;
using MainTz.Web.Configurations;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using MainTz.Web.Middleware;
using MainTz.Web.Mappings;
using System.Reflection;
using FluentValidation;

namespace MainTz.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppOptionsConfiguration();
            services.AddDbContextFactory<MainContext>();
            services.AddAppAuth();
            services.AddAppMinioConfiguration();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddDistributedMemoryCache();
            services.AddAppServices();
            services.AddAppRepositories();
            services.AddAppAutoMapper();
            
			services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHsts();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
                {
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                    if (!context.Brands.Any())
                    {
                        var brand = new BrandEntity
                        {
                            Name = "brand1",
                            Models = new List<ModelEntity>
                            {
                                new ModelEntity {Name = "Model1" }
                            }
                        };
                        context.Brands.Add(brand);
                        context.SaveChanges();
                    }
                    if (!context.Roles.Any())
                    {
                        var roles = new List<RoleEntity>
                        {
                            new RoleEntity { RoleName = "Admin" },
                            new RoleEntity { RoleName = "Manager" },
                            new RoleEntity { RoleName = "User" }
                        };
                        context.Roles.AddRange(roles);
                        context.SaveChanges();
                    }

                    if (!context.Users.Any())
                    {
                        var users = new List<UserEntity>
                        {
                            new UserEntity
                            {
                                Name = "Admin",
                                Email = "Admin@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "Admin").FirstOrDefault(),
                                Notifications = new List<NotificationEntity>
                                {
                                    new NotificationEntity
                                    {
                                        Header = "testHeader1",
                                        Description = "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1",
                                        IsRead = false
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader2",
                                        Description = "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2",
                                        IsRead = false
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader3",
                                        Description = "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3",
                                        IsRead = false
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader4",
                                        Description = "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4",
                                        IsRead = true
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader5",
                                        Description = "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5",
                                        IsRead = true
                                    },
                                }
                            },
                            new UserEntity
                            {
                                Name = "Manager",
                                Email = "Manager@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "Manager").FirstOrDefault()
                            },
                            new UserEntity
                            {
                                Name = "User",
                                Email = "User@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "User").FirstOrDefault(),
                                Notifications = new List<NotificationEntity>
                                {
                                    new NotificationEntity
                                    {
                                        Header = "testHeader1",
                                        Description = "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1"
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader2",
                                        Description = "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2"
                                    },
                                }
                            },
                        };
                        context.Users.AddRange(users);
                        context.SaveChanges();
                    }
                    if (!context.Cars.Any())
                    {
                        //var cars = new List<CarEntity>();

                        //for(int i = 0; i < 15; i++)
                        //{
                        //    var car = new CarEntity
                        //    {
                        //        Name = $"TestName{i}",
                        //        Color = "red",
                        //        Price = i * 100,
                        //        IsFavorite = false,
                        //        Images = new List<ImageEntity>
                        //        {
                        //            new ImageEntity
                        //            {
                        //                Name = "pic1",
                        //                Path = "cars-image-bucket/Avatr-11-image-1.jpg"
                        //            }
                        //        },
                        //        IsVisible = true,
                        //        Description = $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                        //        $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                        //        $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                        //        $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                        //        $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                        //        $"Decscriptin{i}Decscriptin{i}Decscriptin{i}",
                        //        Model = context.Models.FirstOrDefault()

                        //    };
                        //    cars.Add(car);
                        //};
                        //#region imageDataAppend
                        //cars[0].Images = new List<ImageEntity> { new ImageEntity {
                        //    Name = "pic1",
                        //    Path = "cars-image-bucket/Avatr-11-image-1.jpg",
                        //    }
                        //};
                        //cars[1].Images = new List<ImageEntity> { new ImageEntity {
                        //    Name = "pic1",
                        //    Path = "cars-image-bucket/BMW-X1-image-1.jpg",
                        //    }
                        //};
                        //cars[2].Images = new List<ImageEntity> { new ImageEntity {
                        //    Name = "pic1",
                        //    Path = "cars-image-bucket/BMW-X3-image-1.jpg",
                        //    }
                        //};
                        //cars[3].Images = new List<ImageEntity> { new ImageEntity {
                        //    Name = "pic1",
                        //    Path = "cars-image-bucket/Changan-Alsvin-image-1.jpg",
                        //    }
                        //};
                        //#endregion

                        //context.Cars.AddRange(cars);
                        //context.SaveChanges();
                        //var adminUser = context.Users.FirstOrDefault(user => user.Role.RoleName == "Admin");
                        //var favorCar = context.Cars.FirstOrDefault();
                        //adminUser.Cars.Add(favorCar);
                        context.SaveChanges();
                    }
                }
            }
            app.UseHttpsRedirection();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseRouting();
            app.UseMiddleware<JwtHeaderMiddleware>();
            app.UseMiddleware<JwtRefreshMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseAppAuth();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
	            name: "default",
	            pattern: "{controller=Car}/{action=GetCars}");
			});
        }
    }
}