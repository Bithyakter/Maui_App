namespace IcecreamMAUI.Shared.Dtos
{
   public record struct IcecreamOptionSto(string Flavor, string Topping);

   public record IcecreamDto(int Id, string Name, string Image, double Price, IcecreamOptionSto[] options);
}