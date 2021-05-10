using Plugin.CloudFirestore;
using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    public class MeetsProvider
    {
        private static IFirestore _cloud = CrossCloudFirestore.Current.Instance;
        public static async Task<bool> Create(MeetsModel meet)
        {
            await _cloud.Collection(MeetsModel.CollectionPath)
                        .Document(meet.Title.Replace(" ", "_"))
                        .SetAsync(meet);
            return true;
        }

        public static async Task<List<MeetsModel>> GetAll(int role)
        {
            IQuerySnapshot query = await _cloud
                                    .Collection(MeetsModel.CollectionPath)
                                    .WhereGreaterThanOrEqualsTo("date", DateTime.UtcNow)
                                    .WhereGreaterThanOrEqualsTo("role", role)
                                    .OrderBy("date")
                                    .GetAsync();

            IEnumerable<MeetsModel> meet = query.ToObjects<MeetsModel>();
            return new List<MeetsModel>(meet);
        }

        public static async Task<List<MeetsModel>> GetMeetsWhereUserWasPresent(User user)
        {
            IQuerySnapshot query = await _cloud
                                    .Collection(MeetsModel.CollectionPath)
                                    .WhereArrayContains("attendance_list", user.Id)
                                    .OrderBy("date")
                                    .GetAsync();

            IEnumerable<MeetsModel> meet = query.ToObjects<MeetsModel>();
            return new List<MeetsModel>(meet);
        }

        public static async Task<MeetsModel> AddUserInAttendance(User user,MeetsModel meet)
        {
            meet.AttendanceList.Add(user.Id);
            bool res = await Update(meet);
            if (!res)
            {
                return null;
            }
            return meet;
        }

        public static async Task<bool> Update(MeetsModel meet)
        {
            await _cloud.Collection(MeetsModel.CollectionPath)
                        .Document(meet.Title.Replace(" ", "_"))
                        .UpdateAsync(meet);

            return true;
        }
    }
}
