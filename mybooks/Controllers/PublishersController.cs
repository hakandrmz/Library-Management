using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using mybooks.ActionResults;
using mybooks.Data.Models;
using mybooks.Data.Services;
using mybooks.Data.ViewModels;
using mybooks.Exceptions;

namespace mybooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublisherController> _logger;

        public PublisherController(PublishersService publishersService, ILogger<PublisherController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy,string searchString,int pageNumber)
        {

            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers");
                var result = _publishersService.GetAllPublishers(sortBy,searchString,pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry we could not load the publishers");
            }

            
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message},Publisher name: {ex.PublisherName}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherById(id);
            if (_response != null)
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};

                //return new CustomActionResult(_responseObj);
                return Ok(_response);
            } else
            {
                //return NotFound();
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is coming from publishers controller")
                //};

                //return new CustomActionResult(_responseObj);
                return NotFound();
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (ArithmeticException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
