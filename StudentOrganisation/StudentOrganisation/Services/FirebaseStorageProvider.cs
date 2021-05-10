using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    public class FirebaseStorageProvider
    {
        private static FirebaseStorage _storage = new FirebaseStorage("studentorganisati.appspot.com");

        public static async Task<string> StoreProfilePictureUrl(Stream image, Models.User user)
        {
            try
            {
                string url = await _storage.Child("user")
                    .Child(user.Id)
                    .Child("profile.jpg")
                    .PutAsync(image);
                return url;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<string> GetProfilePictureUrl(Models.User user)
        {
            try
            {
                string url = await _storage.Child("user")
                    .Child(user.Id)
                    .Child("profile.jpg")
                    .GetDownloadUrlAsync();
                return url;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<string> GetBadgeUrl(string highlists)
        {
            try
            {
                string url = await _storage.Child("badge")
                    .Child(highlists + ".png")
                    .GetDownloadUrlAsync();
                return url;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
