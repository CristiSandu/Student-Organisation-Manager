using Plugin.CloudFirestore;
using StudentOrganisation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    public class LinksProvider
    {
        private static IFirestore _cloud = CrossCloudFirestore.Current.Instance;
        public static async Task<bool> Create(LinksModel link)
        {
            await _cloud.Collection(LinksModel.CollectionPath)
                        .Document(link.Title.Replace(" ", "_"))
                        .SetAsync(link);
            return true;
        }

        public static async Task<List<LinksModel>> GetAll()
        {
            IQuerySnapshot query = await _cloud
                                    .Collection(LinksModel.CollectionPath)
                                    .OrderBy("title")
                                    .GetAsync();

            IEnumerable<LinksModel> links = query.ToObjects<LinksModel>();
            return new List<LinksModel>(links);
        }

        public static async Task<bool> Update(LinksModel link)
        {
            await _cloud.Collection(LinksModel.CollectionPath)
                        .Document(link.Title.Replace(" ", "_"))
                        .UpdateAsync(link);

            return true;
        }
    }
}
