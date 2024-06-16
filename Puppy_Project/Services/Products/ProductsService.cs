﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Puppy_Project.Dbcontext;
using Puppy_Project.Models;
using Puppy_Project.Models.ProductDTO;


namespace Puppy_Project.Services.Products
{
    public class ProductsService: IProductsService
    {
        private readonly IMapper _mapper;
        private readonly PuppyDb _puppyDb;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsService(PuppyDb puppyDb,IMapper mapper, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _puppyDb = puppyDb;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<List<outProductDTO>> GetProducts()
        {
            try
            {
                var tmpProductlist = _puppyDb.ProductsTb.Include(cg=>cg.Category);
                if(tmpProductlist == null)
                {
                    return new List<outProductDTO>();
                }
                var productlist =await tmpProductlist.Select(p => new outProductDTO
                {
                    Id = p.Id,
                    Type = p.Type,
                    Img = $"{_configuration["HostUrl:images"]}/Products/{p.Img}",
                    Name = p.Name ,
                    Detail = p.Detail,
                    About = p.About,
                    Price = p.Price,
                    Ctg = p.Category.Ctg
                }).ToListAsync();
                return productlist;
            }catch(Exception ex)
            {
                return new List<outProductDTO>();
            }   
        }

        public async Task<int> TotalDataCountByCategory(string category)
        {
            try
            {
                var totaldata = _puppyDb.ProductsTb.Include(p => p.Category).Where(p => p.Category.Ctg.ToLower() == category.ToLower());
                return totaldata.Count();
            }catch( Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<outProductDTO>> GetProductsByCategory(string category, int pageNo, int pageSize)
        {
            try
            {
                int initial = 1;
                int skipdata = (pageNo - initial) * pageSize;
                var tmpProductlist = _puppyDb.ProductsTb.Include(cg => cg.Category).Where(p=>p.Category.Ctg.ToLower() == category.ToLower());
                if (tmpProductlist == null)
                {
                    return new List<outProductDTO>();
                }
                var productlist = await tmpProductlist.Skip(skipdata).Take(pageSize).Select(p => new outProductDTO
                {
                    Id = p.Id,
                    Type = p.Type,
                    Img = $"{_configuration["HostUrl:images"]}/Products/{p.Img}",
                    Name = p.Name,
                    Detail = p.Detail,
                    About = p.About,
                    Price = p.Price,
                    Ctg = p.Category.Ctg
                }).ToListAsync();
                return productlist;
            }
            catch (Exception ex)
            {
                return new List<outProductDTO>();
            }
        }

        public async Task<outProductDTO> GetProductById(int id)
        {
            try
            {
                var tmpProduct =  _puppyDb.ProductsTb.Include(cg => cg.Category).FirstOrDefault(p=>p.Id == id);
                if (tmpProduct == null)
                {
                    return null;
                }
                var product = new outProductDTO
                {
                    Id = tmpProduct.Id,
                    Type = tmpProduct.Type,
                    Img = $"{_configuration["HostUrl:images"]}/Products/{tmpProduct.Img}",
                    Name = tmpProduct.Name,
                    Detail = tmpProduct.Detail,
                    About = tmpProduct.About,
                    Price = tmpProduct.Price,
                    Ctg = tmpProduct.Category.Ctg
                };
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<List<outProductDTO>> GetProductsByPage(int pageNo,int pageSize)
        {
            try
            {
                int initial = 1;
                int skipdata = (pageNo - initial) * pageSize;
                var tmpProductlist = _puppyDb.ProductsTb.Include(cg => cg.Category);
                if (tmpProductlist == null)
                {
                    return new List<outProductDTO>();
                }
                var productlist = await tmpProductlist.Skip(skipdata).Take(pageSize).Select(p => new outProductDTO
                {
                    Id = p.Id,
                    Type = p.Type,
                    Img = $"{_configuration["HostUrl:images"]}/Products/{p.Img}",
                    Name = p.Name,
                    Detail = p.Detail,
                    About = p.About,
                    Price = p.Price,
                    Ctg = p.Category.Ctg
                }).ToListAsync();
                return productlist;
            }catch(System.Exception ex)
            {
                return new List<outProductDTO>();
            }
        }


        public async Task<bool> AddProduct(AddProductDTO product, IFormFile image)
        {
            try
            {
                bool isItemExist = await _puppyDb.ProductsTb.AnyAsync(p=>p.Name.ToLower() == product.Name.ToLower());
                bool isValid = await _puppyDb.CategoryTB.AnyAsync(c => c.Id == product.Category_id);
                if (!isValid || isItemExist)
                {
                    return false;
                }
                string productImage = null;
                if (image != null && image.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    productImage = fileName;
                }
                var productdto = _mapper.Map<Product>(product);
                productdto.Img = productImage;
                _puppyDb.ProductsTb.Add(productdto);
                _puppyDb.SaveChanges();
                return isValid;
        }catch(Exception ex)
            {
                return false;
            }

}


        public async Task<bool> UpdateProduct(int id, AddProductDTO product,IFormFile image) 
        {
            try
            {
                var isItemFounded = await _puppyDb.ProductsTb.SingleOrDefaultAsync(p => p.Id == id);
                if (isItemFounded == null)
                {
                    return false;
                }
                string productImage = null;
                if (image != null && image.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    productImage = fileName;
                }
                
                _mapper.Map(product, isItemFounded);
                isItemFounded.Img = productImage ?? isItemFounded.Img;
                await _puppyDb.SaveChangesAsync();
                return true;
            }catch( Exception ex)
            {
                return false;
            }   
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var isItemExist = await _puppyDb.ProductsTb.SingleOrDefaultAsync(p => p.Id == id);
                if (isItemExist == null)
                {
                    return false;
                }
                _puppyDb.ProductsTb.Remove(isItemExist);
                _puppyDb.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            } 
        }
    }
}
