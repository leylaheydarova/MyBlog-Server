using MyBlog.App.DTOs.Favorite;
using MyBlog.App.Exceptions;
using MyBlog.App.Models;
using MyBlog.App.Repositories.Interfaces;
using MyBlog.App.Services.Interfaces;

namespace MyBlog.App.Services.Implements
{
    public class FavoriteItemService : IFavoriteItemService
    {
        IFavoriteItemRepository _itemRepository;
        IBlogRepository _blogRepository;
        IFavoriteRepository _favoriteRepository;

        public FavoriteItemService(IFavoriteItemRepository itemRepository, IFavoriteRepository favoriteRepository, IBlogRepository blogRepository)
        {
            _itemRepository = itemRepository;
            _favoriteRepository = favoriteRepository;
            _blogRepository = blogRepository;
        }

        public async Task CreateAsync(int blogId, int userId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId, false);
            if (blog == null) throw new NotFoundException("blog");

            var favorite = await _favoriteRepository.GetUserFavoriteListAsync(userId, false);
            if (favorite == null) throw new NotFoundException("favorite list");

            var item = new FavoriteItem()
            {
                BlogId = blogId,
                FavoriteId = favorite.Id,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };

            await _itemRepository.AddAsync(item);
            await _itemRepository.SaveAsync();
        }

        public async Task<FavoriteGetDto> GetFavorite(int userId)
        {
            var favorite = await _favoriteRepository.GetUserFavoriteListAsync(userId, false, "FavoriteItems.Blog");
            if (favorite == null) throw new NotFoundException("favorite list");

            var itemDtos = new List<FavoriteItemGetDto>();
            itemDtos = favorite.FavoriteItems.Select(item => new FavoriteItemGetDto
            {
                Id = item.Id,
                BlogId = item.BlogId,
                BlogText = item.Blog.Text,
                BlogTitle = item.Blog.Title,
                FavoriteId = item.FavoriteId
            }).ToList();



            var dto = new FavoriteGetDto()
            {
                Id = favorite.Id,
                UserId = favorite.UserId,
                ItemGetDtos = itemDtos
            };
            return dto;

        }

        public async Task RemoveAsync(int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId, true);
            if (item == null) throw new NotFoundException("item");
            _itemRepository.Remove(item);
            await _itemRepository.SaveAsync();
        }
    }
}
