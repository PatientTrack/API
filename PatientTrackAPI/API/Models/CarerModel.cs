using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class CarerModel
    {
        private static PatientTrackDBEntities dataContext = new PatientTrackDBEntities();

        public static List<Carer> GetAllCarers()
        {
            try
            {
                var query = from carer in dataContext.Carers
                            select carer;
                return query.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return null;
            }
        }

        public static Carer SearchCarerByName(string carerName)
        {
            try
            {
                var query = from carer in dataContext.Carers
                            where carer.CarerName.Contains(carerName)
                            select carer;
                return query.First();
            }            
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return null;
            }
        }

        public static Carer GetCarer(int CarerID)
        {            
            try
            {
                var query = from carer in dataContext.Carers
                            where carer.CarerID == CarerID
                            select carer;
                return query.First();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return null;
            }
        }

        public static List<Carer> InsertCarer(Carer c)
        {            
            try
            {
                dataContext.Carers.Add(c);
                dataContext.SaveChanges();
                return GetAllCarers();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return null;
            }
        }

        public static List<Carer> UpdateCarer(Carer c)
        {
            try
            {
                var car = (from carer in dataContext.Carers
                           where carer.CarerID == c.CarerID
                           select carer).SingleOrDefault();
                car.CarerName = c.CarerName;
                car.CarerEmail = c.CarerEmail;
                car.CarerPwd = c.CarerPwd;
                dataContext.SaveChanges();
                return GetAllCarers();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return null;
            }
        }

        public static List<Carer> DeleteCarer(Carer c)
        {
            try
            {
                var car = (from carer in dataContext.Carers
                           where carer.CarerID == c.CarerID
                           select carer).SingleOrDefault();
                dataContext.Carers.Remove(car);
                dataContext.SaveChanges();
                return GetAllCarers();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return null;
            }
        }
    }
}