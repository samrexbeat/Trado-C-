using tradoAPI.Data;
using tradoAPI.Models;

namespace tradoAPI.Repo.Abstract
{
    public interface IproductRepo
    {
        bool Add(Products model);
        
    }
}
