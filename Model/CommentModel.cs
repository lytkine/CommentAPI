using Microsoft.Security.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CommentAPI.Model
{
    public class CommentModel
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "body")]
        public string BodyText { get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }


        public void Clean()
        {
            CleanName();
            CleanBody();
        }

        private void CleanName()
        {
            if (string.IsNullOrWhiteSpace(Name)) {
                return;
            }

            Name = Encoder.HtmlEncode(Name, false);
        }

        private void CleanBody()
        {
            if (string.IsNullOrWhiteSpace(BodyText))
            {
                return;
            }

            BodyText = Encoder.HtmlEncode(BodyText, false); ;
        }

        public bool IsValid()
        {
            
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(BodyText)) {
                return false;
            }

            return true;
            
        }
    }
}
