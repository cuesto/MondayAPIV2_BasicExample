using MondayV2API_BasicExample.MondayEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MondayAPIV2_BasicExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiToken = "";
            string apiRoot = "https://api.monday.com/v2/";
            
            // using statement used here to dispose of the client (but it's recommended to reuse HttpClient as much as possible in a real situation)
            // see https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/
            using (var client = new MondayClient(apiToken, apiRoot)) 
            {
                var service = new MondayService(client);

                // get all boards
                List<Board> boards = (List<Board>)service.GetBoards().Where(x => x.Id == "5140759834").ToList();
                Console.WriteLine("-- Boards --");
                boards.ForEach(x => Console.WriteLine($"Board: {x.Name}"));

                //boards = boards.Where(x=>x.Id == "5140759834").ToList();

                if (boards.Any())
                {
                    // get items for the first board in the list
                    Board board = service.GetBoardWithItems(boards[0].Id);
                    Console.WriteLine($"\n-- Board {boards[0].Id} Items --");
                    foreach (var boardItem in board.Items)
                    {
                        Console.WriteLine($"-- {boardItem.Id} {boardItem.Name}");
                    }

                    // update a column value with mutation
                    Console.WriteLine($"\n-- Changing a column value --");
                    //var itemCreated = service.CreateTextColumnValue(boards[0].Id, "B0023");
                    //var itemId = itemCreated["create_item"]["id"].Value;
                    var itemId = "5211344191";

                    //var columnChange0 = service.ChangeTextColumnValue(boards[0].Id, itemId , "numbers","1049");
                    //var columnChange1 = service.ChangeTextColumnValue(boards[0].Id, itemId, "date4", "2022-07-21T12:00:00.000Z");
                    
                    var columnChange1 = service.ChangeTextColumnValue(boards[0].Id, itemId, "connect_boards", "{\\\"changed_at\\\":\"2023-09-11T16:28:54.741Z\",\"linkedPulseIds\":[{\"linkedPulseId\":4912847929}]}");

                    Console.WriteLine($"The response body was: {columnChange1}");
                }
            }
            Console.Read();
        }
    }
}
