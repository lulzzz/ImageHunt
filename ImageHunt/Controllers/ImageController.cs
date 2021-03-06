using System;
using System.Net.Mime;
using System.Threading.Tasks;
using ImageHunt.Computation;
using ImageHunt.Model;
using ImageHunt.Services;
using ImageHuntWebServiceClient.Request;
using Microsoft.AspNetCore.Mvc;

namespace ImageHunt.Controllers
{
  [Route("api/[controller]")]
  [DisableRequestSizeLimit]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
      private readonly IImageTransformation _imageTransformation;

      public ImageController(IImageService imageService, IImageTransformation imageTransformation)
      {
        _imageService = imageService;
        _imageTransformation = imageTransformation;
      }
        [HttpGet("{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
          try
          {
            var picture = await _imageService.GetPictureById(imageId);
            return File(picture.Image, "image/jpeg");

          }
          catch (System.Exception e)
          {
            return new NotFoundObjectResult($"Image of id {imageId} not found");
          }
        }
      [HttpGet("Thumbnail")]
      public async Task<IActionResult> GetThumbailById([FromQuery]int pictureId, [FromQuery]int width, [FromQuery]int height)
      {
        var picture = await _imageService.GetPictureById(pictureId);

        var thumbnail = _imageTransformation.Thumbnail(picture.Image, width, height);
        return File(thumbnail, "image/jpeg");
      }
    [HttpGet("SourceAndThumbnail")]
      public async Task<IActionResult> GetImageAndthumbnailById([FromQuery]int pictureId, [FromQuery]int thumbnailWidth, [FromQuery]int thumbnailHeight)
      {
        var picture = await _imageService.GetPictureById(pictureId);

        var thumbnail = _imageTransformation.Thumbnail(picture.Image, thumbnailWidth, thumbnailHeight);
        return Ok(new [] {File(thumbnail, "image/jpeg", "thumnbnail"),
          File(picture.Image, "image/jpeg", "source")});
      }
  }
}
