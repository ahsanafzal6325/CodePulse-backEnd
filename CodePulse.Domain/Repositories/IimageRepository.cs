using CodePulse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Repositories
{
    public interface IimageRepository
    {
        Task<BlogImage> Upload(BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}
