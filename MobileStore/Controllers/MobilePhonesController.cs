using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Models;
using MobileStore.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobilePhonesController : ControllerBase
    {
        private readonly IMobilePhoneService _mobilePhoneService;
        private readonly IMediaService _mediaService;

        public MobilePhonesController(IMobilePhoneService mobilePhoneService, IMediaService mediaService)
        {
            _mobilePhoneService = mobilePhoneService;
            _mediaService = mediaService;
        }

        /// <summary>
        /// Return all the mobile phones
        /// </summary>
        /// <response code="200">Get successfully</response>
        /// <response code="404">Mobile phones not found</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<MobilePhone>>> GetMobilePhones()
        {
            var response = await _mobilePhoneService.Get();

            if (!response.Any())
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Return mobile phone by id
        /// </summary>
        /// <response code="200">Get successfully</response>
        /// <response code="404">Mobile phone with this id not found</response>

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<MobilePhone>> GetMobilePhone(int id)
        {
            var response = await _mobilePhoneService.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Update a mobile phone
        /// </summary>
        /// <response code="200">Updated successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Mobile phone with this id not found</response>
        /// <response code="500">An error occurred while processing request</response>
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutMobilePhone(int id, MobilePhoneRequest request)
        {
            if (!await MobilePhoneExists(id))
            {
                return NotFound();
            }

            var response = await _mobilePhoneService.Update(id, request);

            if (response == null)
            {
                return StatusCode(500);
            }

            return Ok(response);
        }

        /// <summary>
        /// Create a mobile phone
        /// </summary>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="409">Mobile phone with this name already exists</response>
        /// <response code="500">An error occurred while processing request</response>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<MobilePhone>> PostMobilePhone(MobilePhoneRequest request)
        {
            var mobilePhoneWithSameNameExists = await _mobilePhoneService.GetByName(request.Name) != null;

            if (mobilePhoneWithSameNameExists)
            {
                return StatusCode(409);
            }

            var response = await _mobilePhoneService.Create(request);

            if (response == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201, response);
        }

        /// <summary>
        /// Delete a mobile phone
        /// </summary>
        /// <response code="204">Deleted successfully</response>
        /// <response code="404">Mobile phone with this id not found</response>
        /// <response code="500">An error occurred while processing request</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMobilePhone(int id)
        {
            if (!await MobilePhoneExists(id))
            {
                return NotFound();
            }

            var response = await _mobilePhoneService.Delete(id);

            if (response == null)
            {
                return StatusCode(500);
            }

            return StatusCode(204);
        }

        /// <summary>
        /// Add media to a mobile phone
        /// </summary>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        [HttpPost("{mobilePhoneId}/media")]
        [Produces("application/json")]
        public async Task<ActionResult<Media>> AddMedia(int mobilePhoneId, IFormFile media)
        {
            var mobilePhone = await _mobilePhoneService.GetById(mobilePhoneId);

            if (mobilePhone == null)
            {
                return NotFound();
            }

            var request = new MediaRequest()
            {
                MobilePhoneId = mobilePhoneId,
                File = media
            };

            var response = await _mediaService.Create(request);

            if (response == null)
            {
                return StatusCode(400);
            }

            return StatusCode(201, response);
        }

        /// <summary>
        /// Delete media by its id
        /// </summary>
        /// <response code="204">Deleted successfully</response>
        /// <response code="404">Not found</response>
        /// <response code="500">An error occurred while processing request</response>
        [HttpDelete("/api/media/{mediaId}")]
        [Produces("application/json")]
        public async Task<ActionResult<Media>> DeleteMedia(int mediaId)
        {
            var media = await _mediaService.GetById(mediaId);

            if (media == null)
            {
                return NotFound();
            }

            var response = await _mediaService.Delete(mediaId);

            if (response == null)
            {
                return StatusCode(500);
            }

            return StatusCode(204);
        }

        private async Task<bool> MobilePhoneExists(int id)
        {
            return await _mobilePhoneService.GetById(id) != null;
        }
    }
}
