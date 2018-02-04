using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OfficePointBot.Helpers
{
    public static class ReplyHelper
    {
        public static async void ShowNewsResultsAsync(ConnectorClient connector, Activity activity, List<NewsInformation> newsResults)
        {             

            Activity replyMessage = activity.CreateReply("Here's the latest news I found:");
            replyMessage.Recipient = activity.From;
            replyMessage.Type = ActivityTypes.Message;
            replyMessage.AttachmentLayout = "Carousel";
            replyMessage.Attachments = new List<Attachment>();

            foreach(var result in newsResults)
            {
                Attachment attachment = new Attachment();
                attachment.ContentType = "application/vnd.microsoft.card.hero";
                                
                HeroCard heroCard = new HeroCard();
                heroCard.Title = result.name;
                heroCard.Subtitle = result.description;
                heroCard.Images = new List<CardImage>();

                CardImage thumbnailImage = new CardImage();
                thumbnailImage.Url = result.image.thumbnail.contentUrl;
                heroCard.Images.Add(thumbnailImage);
                                
                heroCard.Buttons = new List<CardAction>();
                CardAction articleCard = new CardAction();
                                 
                articleCard.Title = "View article";
                articleCard.Type = "openUrl";
                
                articleCard.Value = result.url;
                heroCard.Buttons.Add(articleCard);

                attachment.Content = heroCard;

                replyMessage.Attachments.Add(attachment);
            }
             
            await connector.Conversations.ReplyToActivityAsync(replyMessage);
        }

        public async static Task<Activity> CreateFactActivityAsync()
        {
            var fact = await Helpers.GeneralHelper.GetRandomFactAsync();

            string message = "DID YOU KNOW: " + fact;

            Activity replyMessage = Common.CommonConversation.CurrentActivity.CreateReply(message);

            return replyMessage;
        }

        public async static Task<Activity> CreateNewsResultsActivity(ConnectorClient connector, Activity activity)
        {
            var newsResults = await Helpers.GeneralHelper.GetTrendingNewsAsync();

            Activity replyMessage = activity.CreateReply("Here's the latest news I found:");
            replyMessage.Recipient = activity.From;
            replyMessage.Type = ActivityTypes.Message;
            replyMessage.AttachmentLayout = "carousel";
            replyMessage.Attachments = new List<Attachment>();

            foreach (var result in newsResults)
            {
                Attachment attachment = new Attachment();
                attachment.ContentType = "application/vnd.microsoft.card.hero";

                HeroCard heroCard = new HeroCard();
                heroCard.Title = result.name;
                heroCard.Subtitle = result.description;
                heroCard.Images = new List<CardImage>();

                CardImage thumbnailImage = new CardImage();
                thumbnailImage.Url = result.image.thumbnail.contentUrl;
                heroCard.Images.Add(thumbnailImage);

                heroCard.Buttons = new List<CardAction>();
                CardAction articleCard = new CardAction();

                articleCard.Title = "View article";
                articleCard.Type = "openUrl";

                articleCard.Value = result.url;
                heroCard.Buttons.Add(articleCard);

                attachment.Content = heroCard;

                replyMessage.Attachments.Add(attachment);
            }

            return replyMessage;
        }

        public static Activity CreateSalesBreakdownActivity(Entity locationEntity)
        {
            Activity replyMessage = Common.CommonConversation.CurrentActivity.CreateReply(null);

            string selectedLocation = (locationEntity == null) ? "Nowhere" : locationEntity.entity;

            replyMessage.Recipient = Common.CommonConversation.CurrentActivity.From;
            replyMessage.Type = "message";
            replyMessage.Attachments = new List<Attachment>();

            replyMessage.Attachments.Add(new Attachment()
            {
                ContentUrl = "[YOUR REMOTE PIE CHART CREATION URL]",
                ContentType = "image/jpeg",
                Name = "Sales Breakdown for " + selectedLocation,

            });

            return replyMessage;

        }

        
    }
}