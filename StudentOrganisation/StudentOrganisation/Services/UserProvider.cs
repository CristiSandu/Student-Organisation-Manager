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
                                     .WhereArrayContains("path", path)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public static async Task<List<Models.User>> GetJuniorsForMentor(Models.User user)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereEqualsTo("mentor", user.Id)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public static async Task<bool> AddStarForUser(Models.User user,int count )
        {
            user.Stars += count;
            await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .Document(user.Id)
                                     .UpdateAsync(user);

            return true;
        }

        public static async Task<List<Models.User>> GetFirestoreUserFromRole(int role)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereEqualsTo("role", role)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public static async Task<List<Models.User>> GetFirestoreUserFromHighlit(string highlit)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereArrayContains("highlists", highlit)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }


        public static async Task<List<Models.User>> GetFirestoreUserFromHighlitPerPath(string highlit, string path)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereArrayContains("path", path)
                                     .WhereArrayContains("highlists", highlit)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public static async Task<List<Models.User>> GetUsersWithMoreStars(int count)
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .OrderBy("stars", true)
                                     .LimitTo(count)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
        }

        public static async Task<Dictionary<string, int>> CountPerPath()
        {
            List<string> str = new List<string> { "Mobile", "Limbaje", "AI", "IoT", "Azure", "Gaming" };
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int max = 0;
            string keyMax = "1";
            foreach (string s in str)
            {
                List<Models.User> lst = await GetFirestoreUserFromPath(s);
                dict[s] = lst.Count;
                if (dict[s] > max)
                {
                    max = dict[s];
                    keyMax = s;
                }
            }
            //dict[keyMax.ToLower()] = max;
            return dict;
        }

        public static async Task<Dictionary<string, int>> CountPerHighlitsPerPath(string path)
        {
            List<string> str = new List<string> { "MVP", "Alpha", "Beta", "Gold" };
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string s in str)
            {
                List<Models.User> lst = await GetFirestoreUserFromHighlitPerPath(s, path);
                dict[s] = lst.Count;
            }
            return dict;
        }

        public static async Task<Dictionary<string, int>> CountPerHighlits()
        {
            List<string> str = new List<string> { "MVP", "Alpha", "Beta", "Gold" };
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int max = 0;
            string keyMax = "1";
            foreach (string s in str)
            {
                List<Models.User> lst = await GetFirestoreUserFromHighlit(s);
                dict[s] = lst.Count;
                if (dict[s] > max)
                {
                    max = dict[s];
                    keyMax = s;
                }
            }
            //dict[keyMax.ToLower()] = max;

            return dict;
        }


        public static async Task<Dictionary<string, int>> CountPerRole()
        {
            // Junior = 0, Member = 1, Mentor = 2, Admin = 3
            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<Models.User> lst = await GetFirestoreUserFromRole(0);
            dict["Junior"] = lst.Count;
            lst = await GetFirestoreUserFromRole(1);
            dict["Member"] = lst.Count;
            lst = await GetFirestoreUserFromRole(2);
            dict["Mentor"] = lst.Count;
            lst = await GetFirestoreUserFromRole(3);
            dict["Admin"] = lst.Count;
            return dict;
        }

        public async static Task<int> CountAll()
        {
            // Junior = 0, Member = 1, Mentor = 2, Admin = 3
            List<Models.User> users = await GetFirestoreAllUser();
            return users.Count;
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

        public static async Task<List<Models.User>> GetAllPresent()
        {
            IQuerySnapshot query = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection(Models.User.CollectionPath)
                                     .WhereEqualsTo("is_present",true)
                                     .GetAsync();

            IEnumerable<Models.User> users = query.ToObjects<Models.User>();
            return new List<Models.User>(users);
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