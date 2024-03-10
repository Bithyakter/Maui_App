using IcecreamMAUI.Api.Data;
using IcecreamMAUI.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IcecreamMAUI.Api.Services
{
   public class IcecreamService(DataContext context)
   {
      private readonly DataContext _context;

      public async Task<IcecreamDto[]> GetIcecreamAsync() =>
         await _context.Icecreams.AsNoTracking()
         .Select(i =>
         new IcecreamDto(
               i.Id,
               i.Name,
               i.Image,
               i.Price,
               i.Options
            .Select(o => new IcecreamOptionSto(o.Flavor, o.Topping))
            .ToArray()
            )
         ).ToArrayAsync();
   }
}