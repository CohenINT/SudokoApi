using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy.Json;
using SudokuSolver.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SudokuSolver.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {

       [HttpGet("/api/GetPuzzle")]
        public ActionResult GetPuzzle()
        {
            return Ok(new { Consumes = "application/json", Values = "moshe" });

        }

        [HttpGet("/api/GetList")]
        public string GetList()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            dynamic list = new Board() { Length=81,Name="board1"};
            var result = ser.Serialize(list);

            return result;
        }






    }
}