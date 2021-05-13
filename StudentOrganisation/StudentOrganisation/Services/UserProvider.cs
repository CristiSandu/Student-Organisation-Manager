using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    class UserProvider
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

        public static async Task<bool> UpdateFirestoreUser(Models.User user)
        {
            await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .Document(user.Id)
                                     .UpdateAsync(user);
            
            return true;
        }


        public static async Task<List<Models.User>> GetFirestoreUserFromPath(string path)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereEqualsTo("path", path)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public static async Task<Dictionary<string, int>> CountPerRole()
        {
            // Junior = 0, Member = 1, Mentor = 2, Admin = 3
            List<string> str = new List<string> { "Mobile", "Limbaje", "AI","IoT","Azure","Gaming"};
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string s in str)
            {
                List<Models.User> lst = await GetFirestoreUserFromPath(s);
                dict[s] = lst.Count;
            }
            return dict;
        }

        public static async Task<Models.User> SetUserPresent(Models.User user)
        {
            user.IsPresent = true;
            
            bool result = await UpdateFirestoreUser(user);
            if (!result)
            {
                return null;
            }
            return user;
        }

        public static async Task<List<Models.User>> GetFirestoreAllUser()
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
