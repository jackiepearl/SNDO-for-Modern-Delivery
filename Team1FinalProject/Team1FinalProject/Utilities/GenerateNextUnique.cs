using Team1FinalProject.DAL;
using System;
using System.Linq;

//JACKIE - added this class to generate next book unique num for when managers create new books
namespace Team1FinalProject.Utilities
{
    public static class GenerateNextUnique
    {
        public static Int32 GetNextUnique(AppDbContext db)
        {
            Int32 intMaxUnique;
            Int32 intNextUnique;

            intMaxUnique = db.Books.Max(b => b.UniqueNum); //this is the highest number in the database right now

            //add one to the current max to find the next one
            intNextUnique = intMaxUnique + 1;

            //return the value
            return intNextUnique;
        }

    }
}