using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy.Json;
using SudokuSolver.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace SudokuSolver.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {
        //exp: https://localhost:44389/api/GetPuzzle
        [HttpGet("/api/GetPuzzle")]
        public ActionResult GetPuzzle()
        {
            return Ok(new { Consumes = "application/json", Values = "moshe" });

        }
        //https://localhost:44389/api/GetList
        [HttpGet("/api/GetList")]
        public string GetList()
        {
            Stopwatch stopWatch = new Stopwatch();
            
            stopWatch.Start();
            Board list = new Board() {Name="board1"};
            stopWatch.Stop();
            
             list.GeneratedTime = $"Elapsed time is {stopWatch.ElapsedMilliseconds} ms";
            var result = JsonConvert.SerializeObject(list);


            return result;
        }









    }
}