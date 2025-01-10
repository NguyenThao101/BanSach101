using BanSach.Components.Model;

namespace BanSach.Components.IService
{
    public interface IItemManagement
    {
        Task<List<Product>> GetAllItem();
        Task<Product> CreateItem(Product Product);
        Task DeleteItem(Product Product);
        Task EditItem(Product Product);
        Task<Product?> GetItemById(int ProductId);
        Task<List<Category>> GetAllCategogy();
        Task<Category?> GetCategoryById(int categoryId);
        Task<Category> CreateCategory(Category category);
        Task DeleteCategory(Category category);
        Task EditCategory(Category category);
        Task<List<Img>> GetAllImg();
        Task<Img> CreateImg(Img img);
        Task EditImg(Img img);
        Task<List<Img>> GetLastFourImgsAsync();
        Task<List<Img>> GetAllImgsDescending();
        Task DeleteImg(Img img);
    }
}
