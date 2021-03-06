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
            news.Date = news.Date.AddHours(3);
            await _cloud.Collection(NewsModel.CollectionPath)
                        .Document(news.Title.Replace(" ", "_"))
                        .SetAsync(news);
            return true;
        }

        public static async Task<List<NewsModel>> GetAll()
        {
            IQuerySnapshot query = await _cloud
                                    .Collection(NewsModel.CollectionPath)
                                    .WhereGreaterThanOrEqualsTo("date", DateTime.Now)
                                    .OrderBy("date")
                                    .GetAsync();

            IEnumerable<NewsModel> news = query.ToObjects<NewsModel>();
            return new List<NewsModel>(news);
        }

        public static async Task<bool> Delete(NewsModel news)
        {
            await _cloud.Collection(NewsModel.CollectionPath)
                        .Document(news.Title.Replace(" ", "_"))
                        .DeleteAsync();

            return true;
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
