using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganisation.Services
{
    public class FirebaseStorageProvider
    {
        public static async Task<string> StoreProfilePicture(Stream image, Models.User user)
        {
            try
            {
                string url = await new FirebaseStorage("studentorganisati.appspot.com")
                    .Child("user")
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

        public static async Task<string> GetProfilePicture(Models.User user)
        {
            try
            {
                string url = await new FirebaseStorage("studentorganisati.appspot.com")
                    .Child("user")
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
    }
}
