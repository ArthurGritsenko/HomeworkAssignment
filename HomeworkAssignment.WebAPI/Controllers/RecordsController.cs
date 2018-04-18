using HomeworkAssignment.Domain.Enums;
using HomeworkAssignment.Domain.Models;
using HomeworkAssignment.Interfaces;
using HomeworkAssignment.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeworkAssignment.WebAPI.Controllers
{
    [RoutePrefix("api/records")]
    public class RecordsController : ApiController
    {
        private readonly IDataStorageService dataStorageService;
        private readonly IDataParserStrategy dataParserStrategy;
        private readonly ISortingStrategy sortingStrategy;

        public RecordsController(IDataStorageService dataStorageService, IDataParserStrategy dataParserStrategy, ISortingStrategy sortingStrategy)
        {
            this.dataStorageService = dataStorageService;
            this.dataParserStrategy = dataParserStrategy;
            this.sortingStrategy = sortingStrategy;
        }

        // GET api/records/{order}
        [Route("{order}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSortedRecords(string order)
        {
            SortStrategyEnum sortStrategy;
            if (!SortStrategyMap.TryGetValue(order, out sortStrategy))
            {
                return NotFound();
            }

            var records = dataStorageService.GetAll();

            if (!records.Any())
            {
                return Ok(records);
            }

            return Ok(sortingStrategy.Sort(sortStrategy, records).Select(x => RecordViewModel.Map(x)));
        }

        // POST api/records
        [HttpPost]
        public async Task<IHttpActionResult> AddRecord([FromBody]string record)
        {
            RecordModel model = new RecordModel();
            try
            {
                var dataParser = dataParserStrategy.GetDataParser(record);
                model = dataParser.Parse(new string[] { record }, skipInvalid: false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (model == null)
            {
                return InternalServerError();
            }

            dataStorageService.Store(model);

            return Created("", model);
        }


        private static Dictionary<string, SortStrategyEnum> SortStrategyMap = new Dictionary<string, SortStrategyEnum>()
        {
            { "gender", SortStrategyEnum.Gender },
            { "birthdate", SortStrategyEnum.BirthDate },
            { "name", SortStrategyEnum.FirstName }
        };
    }
}
