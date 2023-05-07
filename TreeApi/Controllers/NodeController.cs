using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreeApi.Models;
using TreeApi.Services.Interfaces;

namespace TreeApi.Controllers
{
    [Route("api/[controller]")]
    public class NodeController : Controller
	{
        private readonly INodeRepository _nodeRepository;
        private readonly IMapper _mapper;

        public NodeController(INodeRepository nodeRepository, IMapper mapper)
        {
            _nodeRepository = nodeRepository;
            _mapper = mapper;
        }

        [HttpGet("GetNodes")]
        public async Task<ActionResult<IEnumerable<NodeModel>>> GetAllAsync()
        {

            IEnumerable<Node> nodes = await _nodeRepository.GetAllAsync();
            List<NodeModel> result = new List<NodeModel>();
            foreach (var item in nodes)
            {
                result.Add(_mapper.Map<NodeModel>(item));
            }
            
            return Ok(result);
        }

        [HttpPost("CreateNode")]
        public async Task<ActionResult<Node>> AddAsync([Required]string name, [Required]int treeId, int parentNodeId)
        {
            NodeBaseFields newInst = new NodeBaseFields
            {
                TreeId = treeId,
                Name = name,
            };
            
            if (!parentNodeId.Equals(null) && parentNodeId != 0) 
                newInst.ParentNodeId = parentNodeId;
          
            await _nodeRepository.CreateNodeAsync(newInst);
            return Ok(newInst);
        }

        [HttpPut("UpdateNode")]
        public async Task<ActionResult> UpdateNodeAsync([FromBody] NodeBaseFields nodeBaseFields)
        {
            await _nodeRepository.UpdateNodeAsync(nodeBaseFields);
            return Ok(nodeBaseFields);
        }

        [HttpDelete("DeleteNode{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _nodeRepository.DeleteNodeAsync(id);
            return Ok("Success");
        }
    }
}