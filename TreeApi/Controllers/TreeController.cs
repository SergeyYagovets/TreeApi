using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreeApi.DAL;
using TreeApi.Models;
using TreeApi.Services.Interfaces;

namespace TreeApi.Controllers
{
    [Route("api/[controller]")]
    public class TreeController : Controller
    {
        private readonly ITreeRepository _treeRepository;
        private readonly IMapper _mapper;

        public TreeController(ITreeRepository treeRepository, IMapper mapper)
        {
            _treeRepository = treeRepository;
            _mapper = mapper;
        }

        [HttpGet("GetTrees")]
        public async Task<ActionResult<IEnumerable<TreeModel>>> GetTreesAsync()
        {
            IEnumerable<Tree> trees = await _treeRepository.GetTreesAsync();
            List<TreeModel> result = new List<TreeModel>();
            foreach (var item in trees)
            {
                result.Add(_mapper.Map<TreeModel>(item));
            }
            return Ok(result);
        }

        [HttpPost("CreateTree")]
        public async Task<ActionResult<IEnumerable<Tree>>> CreateTreeAsync(string name)
        {
            var result = await _treeRepository.CreateTreeAsync(name);
           
            return Ok(result);
           
        }

        [HttpPut("UpdateTree")]
        public async Task<IActionResult> UpdateTreeAsync([FromBody] TreeBaseFields treeBaseFields)
        {
            await _treeRepository.UpdateTreeAsync(treeBaseFields);
            return Ok(treeBaseFields);
        }

        [HttpDelete("DeleteTree/{Id}")]
        public async Task<IActionResult> DeleteTree([FromRoute] int Id)
        {
            await _treeRepository.DeleteTreeAsync(Id);
            return Ok("Success");
        }
    }
}