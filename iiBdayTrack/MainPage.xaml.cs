using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.IO;
using System.Reflection;


namespace iiBdayTrack
{
    public partial class MainPage : ContentPage
    {

        List<Friend> friendList = new List<Friend>();
        public MainPage()
        {
            InitializeComponent();

            LoadFriendFile();
        }


        /// <summary>
        /// Load and parse the friend file data
        /// </summary>
        private void LoadFriendFile()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("iiBdayTrack.friends.txt");

            // Temporary string
            string[] tempFriend;

            // Create a list for friend names
            List<string> friendNames = new List<string>();

            using (StreamReader reader = new StreamReader(stream))
            {
                // While we're not at the end of the file data
                while (!reader.EndOfStream)
                {
                    Friend friend = new Friend();
                    
                    // Read a single line out of the text file using a comma as a delimiter
                    // Readline goes until it hits a hard return
                    tempFriend = reader.ReadLine().Split(',');

                    friend.Name = tempFriend[0];
                    
                    friend.Age = int.Parse(tempFriend[1]);
                    friend.PhoneNumber = tempFriend[2];
                    friend.Personality = tempFriend[3];
                    friend.Rating = int.Parse(tempFriend[4]);

                    // Name is first element, add to friend names list
                    friendNames.Add(friend.Name);
                    friendList.Add(friend);
                }
            }

            // Populate picker with friend names
            PckrFriends.ItemsSource = friendNames;
        }

        /// <summary>
        ///  Display results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDetails_Clicked(object sender, EventArgs e)
        {
            Friend friend = new Friend();

            if (PckrFriends.SelectedIndex > -1)
            {
                foreach (Friend fr in friendList)
                {
                    if (fr.Name == PckrFriends.SelectedItem.ToString())
                    {
                        friend = fr;
                    }
                }

                DisplayAlert(friend.Name, $"Age: {friend.Age}\n\n"+
                    $"Phone Number: {friend.PhoneNumber}\n\n"+
                    $"Personality: {friend.Personality}\n\n"+
                    $"Coolness: {friend.Rating}"
                    , "close");
            }
            else
            {
                DisplayAlert("invalid input", "please select a friend first", "close");
            }
        }
    }
}
