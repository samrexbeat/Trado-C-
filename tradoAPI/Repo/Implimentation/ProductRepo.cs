using tradoAPI.Data;
using tradoAPI.Models;
using tradoAPI.Repo.Abstract;

namespace tradoAPI.Repo.Implimentation
{
    public class ProductRepo: IproductRepo 
    {
		private readonly ProductDbContext productDbContext;

		public ProductRepo(ProductDbContext productDbContext)
		{
			this.productDbContext = productDbContext;
		}
        public bool Add(Products model)
        {
			try
			{
				productDbContext.Products.Add(model);
				productDbContext.SaveChanges();
				return (true);
			}
			catch (Exception ex)
			{

				return false;
			}
        }

    }
}
