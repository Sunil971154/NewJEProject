using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewjeProject.Models;
using System.Collections.Concurrent;

namespace NewjeProject.Controllers
{
    [Route("journal")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {

        // Thread-safe dictionary to simulate in-memory data storeAdd commentMore actions
        private static ConcurrentDictionary<long, JournalEntry> journalEntries = new ConcurrentDictionary<long, JournalEntry>();

     
        [HttpGet]
        public ActionResult<IEnumerable<JournalEntry>> GetAll()
        {
            //return Ok(new List<JournalEntry>(journalEntries.Values));  isse bhi same hoga 
            return Ok(journalEntries.Values);
        }

      
        [HttpPost]
        public ActionResult CreateEntry([FromBody] JournalEntry journalEntry)
        {
            journalEntries[journalEntry.Id] = journalEntry;
            return Ok("Journal Entry Created");
        }


        [HttpGet("id/{myId}")]
        public ActionResult<JournalEntry> GetJournalEntryById(long myId)
        {
            //TryGetValue एक method है जो Dictionary या ConcurrentDictionary में मौजूद key के लिए उसकी value को safely निकालने का तरीका है।
            if (journalEntries.TryGetValue(myId, out var entry))
            {
                return Ok(entry);
            }
            return NotFound();
        }


        [HttpPut("id/{id}")]
        public ActionResult<JournalEntry> UpdateJournalEntry(long id, [FromBody] JournalEntry myEntry)
        {
            //ContainsKey method है:Dictionary<TKey, TValue> और ConcurrentDictionary<TKey, TValue> का।
            if (!journalEntries.ContainsKey(id))
            {
                return NotFound();
            }
            journalEntries[id] = myEntry;
            return Ok(myEntry);
        }


        [HttpDelete("id/{myId}")]
        public ActionResult<JournalEntry> DeleteJournalEntry(int myId)
        {
            //TryRemove एक method है जो ConcurrentDictionary में दी गई key को safely हटाता है और उसकी value return करता है—अगर key न मिले तो quietly fail हो जाता है।
            if (journalEntries.TryRemove(myId, out var removedEntry))
            {                
                return Ok($"'{removedEntry}' Deleted Succesfully ");
            }
            return NotFound();
        }

        

    }
}
