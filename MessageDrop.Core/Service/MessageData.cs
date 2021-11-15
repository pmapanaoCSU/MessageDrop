using MessageDrop.Core.Interface;
using MessageDrop.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace MessageDrop.Core.Service
{
    public class MessageData : IMessageData
    {
        // MAKE SURE TO EDIT THIS PATH ON YOUR LOCAL MACHINE CONTEXT 
        private string RANDOM_WORDS_JSON = @"C:\Users\emarl\Source\Repos\pmapanaoCSU\MessageDrop\MessageDrop.Core\StaticData\RandomWords.json";
        private string MESSAGES_JSON = @"C:\Users\emarl\Source\Repos\pmapanaoCSU\MessageDrop\MessageDrop.Core\StaticData\MessageGetAll.json";

        readonly List<Message> _messages = new List<Message>();

        public MessageData(bool isInitSeedData, [Optional] int numSeedData)
        {
            _messages = GenerateSeedMessages(numSeedData);
        }

        // Get a list based on the key of JSON file 
        private List<string> getListOfDataFromJson(string pathToJson, string dataToExtract)
        {
            var jsonInMem = String.Empty;


            using (StreamReader r = new StreamReader(pathToJson))
            {
                jsonInMem = r.ReadToEnd();
            }
            Console.WriteLine(jsonInMem);

            JArray json = (JArray)JsonConvert.DeserializeObject(jsonInMem);

            var listObjFromJson = json.Values("MessageString").ToList();
            List<string> stringsFromJson = new List<string>();
            foreach (var obj in listObjFromJson)
            {
                stringsFromJson.Add(obj.ToString());
            }

            return stringsFromJson;

        }
        private List<Message> GenerateSeedMessages(int numSeedData)
        {
            // Get list of Random Words 
            var randomWords = getListOfDataFromJson(MESSAGES_JSON, "MessageString");

            // Empty list of messages
            List<Message> seedData = new List<Message>();


            // Populate messages in seedData
            for (int i = 0; i < randomWords.Count; i++)
            {
                int id = i + 1;
                seedData.Add(new Message(id, randomWords[i]));
            }

            return seedData;
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            IEnumerable<Message> messages = new List<Message>();
            messages = _messages;
            return Task.FromResult(messages);
        }

        public Task<Message> Get(int id)
        {
            var messageById =
                from message in _messages
                where message.Id == id
                select message;

            // accountById returns IEnumerable meaning we have to cast it as a single Account 
            return Task.FromResult<Message>((Message)messageById);
        }

        public Task<bool> Insert(Message message)
        {
            if (message == null)
            {
                return Task.FromResult(false);
            }

            _messages.Add(message);
            return Task.FromResult(true);
        }
    }
}
