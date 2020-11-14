using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using VatsimLibrary.VatsimClientV1;
using VatsimLibrary.VatsimDb;

namespace api
{
    public class PilotsEndpoint
    {
        public static async Task CallsignEndpoint(HttpContext context)
        {

            string responseText = null;
            string callsign = context.Request.RouteValues["callsign"] as string;
            switch((callsign ?? "").ToLower())
            {
                case "aal1":
                    responseText = "Callsign: AAL1";
                    break;
                default:
                    responseText = "Callsign: INVALID";
                    break;
            }

            if(callsign != null)
            {
                await context.Response.WriteAsync($"{responseText} is the callsign");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }


        /* NOTE: All of these require that you first obtain a pilot and then search in Positions */

        public static async Task AltitudeEndpoint(HttpContext context)
        {       
            //Gets pilot's callsign and gives the altitude of the given callsign. 
            string responseText = null;
            string pilot = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext())
            {
                if(pilot != null)
                {
                    Console.WriteLine($"{pilot}");
                                
                    var _altitude = await db.Positions.Where(f => f.Callsign == ((pilot ?? "").ToUpper())).ToListAsync();
                    responseText = $"The altitude of {pilot} is {_altitude[0].Altitude}";
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }            
        }
        
        public static async Task GroundspeedEndpoint(HttpContext context)
        {
            //Gets pilot's groundspeed given their callsign  
            string responseText = null;
            string pilot = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext())
            {
                if(pilot != null)
                {
                    Console.WriteLine($"{pilot}");
                                
                    var _groundspeed = await db.Positions.Where(f => f.Callsign == ((pilot ?? "").ToUpper())).ToListAsync();
                    responseText = $"The ground speed of {pilot} is {_groundspeed[0].Groundspeed}";
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }  

            
        }

        public static async Task LatitudeEndpoint(HttpContext context)        
        {
            //Gets pilot's Latitude given their callsign  
            string responseText = null;
            string pilot = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext())
            {
                if(pilot != null)
                {
                    Console.WriteLine($"{pilot}");
                                
                    var _latitude = await db.Positions.Where(f => f.Callsign == ((pilot ?? "").ToUpper())).ToListAsync();
                    responseText = $"The latitude of {pilot} is {_latitude[0].Latitude}";
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            } 
        }

        public static async Task LongitudeEndpoint(HttpContext context)
        {
            //Gets pilot's Longitude given their callsign  
            string responseText = null;
            string pilot = context.Request.RouteValues["callsign"] as string;

            using(var db = new VatsimDbContext())
            {
                if(pilot != null)
                {
                    Console.WriteLine($"{pilot}");
                                
                    var _longitude = await db.Positions.Where(f => f.Callsign == ((pilot ?? "").ToUpper())).ToListAsync();
                    responseText = $"The longitude of {pilot} is {_longitude[0].Longitude}";
                    await context.Response.WriteAsync($"RESPONSE: {responseText}");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            } 
        }
        
    }
}