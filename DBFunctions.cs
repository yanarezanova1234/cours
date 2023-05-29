using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinFormsApp1
{
    internal static class DBFunctions
    {
        public static async Task addUser(string name, string password)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                User user = new User
                {
                    Username = name,
                    Password = password,
                    CreatedAt = DateTime.Now
                };
                db.Users.AddRange(user);
                await db.SaveChangesAsync();

            }
            
        }
        public static async Task SaveLog(string log)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                Log log1 = new Log
                {
                    History= log
                };
                db.Logs.AddRange(log1);
                await db.SaveChangesAsync();

            }

        }
        public static async Task UpdateLog(string log)
        {
            using (var context = new russiancheckersContext())
            {
                var lastRecord = context.Logs
                    .OrderByDescending(x => x.IdGame) // Предполагается, что у вас есть поле Id для сортировки
                    .FirstOrDefault();

                // Используйте lastRecord для выполнения операций с последней записью
                if (lastRecord != null)
                {
                    // Доступ к полям последней записи
                    var fieldValue = lastRecord.History = log;
                    // ...
                }
                await context.SaveChangesAsync();
            }

        }
        public static bool IsUserExists(string username)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                if (db != null && db.Users != null)
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    return user != null;
                }
                return false;
            }
                
        }

        public static async Task SaveEnd(string username, string winner, DateTime start)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                HistoyOfGame histoy = new HistoyOfGame
                {
                    Player = username,
                    Winner = winner,
                    StartDateTime = start,
                    EndDateTime = DateTime.Now
                };
                db.HistoyOfGames.AddRange(histoy);
                await db.SaveChangesAsync();

            }
        }
        public static string GetHashedPassword(string username)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {
                if (db != null && db.Users != null)
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    if (user != null)
                    {
                        return user.Password;
                    }
                }
                return null;
            }
        }

    }
}
