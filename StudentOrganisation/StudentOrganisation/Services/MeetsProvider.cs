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

        public static async Task<List<MeetsModel>> GetForAperiod(DateTime start, DateTime end)
        {
            IQuerySnapshot query = await _cloud
                                    .Collection(MeetsModel.CollectionPath)
                                    .WhereGreaterThanOrEqualsTo("date", start)
                                    .WhereLessThan("date", end)
                                    .GetAsync();

            IEnumerable<MeetsModel> meet = query.ToObjects<MeetsModel>();
            return new List<MeetsModel>(meet);
        }

        public static async Task<Dictionary<int, int>> CountPerYear(int year)
        {
            List<int> list = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12};
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (int month in list)
            {
                DateTime lastPer = new DateTime(year, month, 1);
                lastPer.AddMonths(1);
                DateTime firsPer = new DateTime(year, month, 1);
                List<MeetsModel> lst = await GetForAperiod(firsPer, lastPer);
                dict[month] = lst.Count;
            }
            return dict;
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

        public static async Task<bool> Delete(MeetsModel meet)
        {
            await _cloud.Collection(MeetsModel.CollectionPath)
                        .Document(meet.Title.Replace(" ", "_"))
                        .DeleteAsync();

            return true;
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
