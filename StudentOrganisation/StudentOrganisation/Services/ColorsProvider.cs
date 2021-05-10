using Plugin.CloudFirestore;
using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    class ColorsProvider
    {
        private static IFirestore _cloud = CrossCloudFirestore.Current.Instance;

        public static async Task<string> GetColorFor(string title)
        {
            IDocumentSnapshot document = await _cloud.Collection(ColorsModel.CollectionPath)
                                        .Document(title)
                                        .GetAsync();
            ColorsModel color = document.ToObject<ColorsModel>();
            return color.Color;
        }


    }
}
