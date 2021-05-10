using Plugin.CloudFirestore;
using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    public class NewsProvider
    {
        private static IFirestore _cloud = CrossCloudFirestore.Current.Instance;
        public static async Task<bool> Create(NewsModel news)
        {
            await _cloud.Collection(NewsModel.CollectionPath)
                        .Document(news.Title.Replace(" ", "_"))
                        .SetAsync(news);
            return true;
        }

        public static async Task<List<NewsModel>> GetAll()
        {
            IQuerySnapshot query = await _cloud
                                    .Collection(NewsModel.CollectionPath)
                                    .WhereGreaterThanOrEqualsTo("date", DateTime.UtcNow)
                                    .OrderBy("date")
                                    .GetAsync();

            IEnumerable<NewsModel> news = query.ToObjects<NewsModel>();
            return new List<NewsModel>(news);
        }

        public static async Task<bool> Update(NewsModel news)
        {
            await _cloud.Collection(NewsModel.CollectionPath)
                        .Document(news.Title.Replace(" ", "_"))
                        .UpdateAsync(news);

            return true;
        }
    }
}
