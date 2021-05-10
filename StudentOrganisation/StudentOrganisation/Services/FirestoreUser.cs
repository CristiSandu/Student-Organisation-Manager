using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    class FirestoreUser
    {
        public static async Task<bool> CreateUserFirestore(Models.User user)
        {
            await CrossCloudFirestore.Current.Instance.
                    Collection(Models.User.CollectionPath).Document(user.Id)
                         .SetAsync(user);
            return true;
        }

        public static async Task<Models.User> GetFirestoreUser(string id)
        {
            IDocumentSnapshot document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection(Models.User.CollectionPath)
                                        .Document(id)
                                        .GetAsync();
            Models.User user = document.ToObject<Models.User>();
            return user;
        }

        public async Task<bool> UpdateFirestoreUser(Models.User user)
        {
            await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .Document(user.Id)
                                     .UpdateAsync(user);
            
            return true;
        }


        public async Task<List<Models.User>> GetFirestoreUserFromPath(string path)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereEqualsTo("path", path)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public async Task<List<Models.User>> GetFirestoreAllUser()
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }
    }
}
