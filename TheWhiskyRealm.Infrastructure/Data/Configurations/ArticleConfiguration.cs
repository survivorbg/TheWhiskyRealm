using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Infrastructure.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(GeneratesArticles());
        }

        private Article[] GeneratesArticles()
        {
            ICollection<Article> articles = new HashSet<Article>();


            articles.Add(new Article
            {
                Id = 1,
                Title = "Types of Whiskey",
                Content = "Know your bourbon from your scotch (and much more!) in this beginner's guide to the most popular types of whiskey.\r\n\r\nThe sheer number of types of whiskey in the liquor store might have you stumped. What’s the difference between Irish whiskey and Scotch whisky? Is all bourbon whiskey? What whiskey is best for your favorite mixed drinks?\r\n\r\nYou’ll find everything you need to know in the guide below!\r\n\r\nBy the way, is it whiskey or whisky?\r\nThat depends where it’s made. Yes, whisk(e)y can be spelled both with an “e” and without, which does confuse even the most seasoned drinkers. But, it turns out the letter is very important to the story of the spirit. The Irish use the “e,” a tradition that carried over to American-made whiskeys. The Scots do not use the “e,” and distillers in Canada and Japan follow their lead. Hence, whisky or whiskies.\r\n\r\nSo now, without further ado, here are the types of whiskey you need to know:\r\n\r\nIrish Whiskey\r\nIrish whiskey has a smoother flavor than other types of whiskey. It’s made from a mash of malt, can only be distilled using water and caramel coloring, and must be distilled in wooden casks for at least three years. The result is a whiskey that’s easy to sip neat or on the rocks, though you can use Irish whiskey to make cocktails.\r\n\r\nScotch Whisky\r\nScotch whisky (aka just scotch) is made in Scotland with either malt or grain. The Scots take their whisky-making seriously and have laws in place that distillers must follow. The spirit must age in an oak barrel for at least three years. Plus, each bottle must have an age statement which reflects the youngest aged whisky used to make that blend. This is a whisky to sip neat—it makes an excellent after-dinner drink.\r\n\r\nJapanese Whisky\r\nA little later to the game than Irish and scotch, Japanese whisky has made its mark on the spirits world for its high standards. Japanese whisky was created to taste as close to the scotch style as possible and uses similar distilling methods. It is mostly imbibed in mixed drinks or with a splash of soda.\r\n\r\nCanadian Whisky\r\nLike scotch, Canadian whisky must be barrel-aged for at least three years. It’s lighter and smoother than other types of whiskey because it contains a high percentage of corn. You will find that most Canadian whiskies are made from corn and rye, but other may feature wheat or barley.\r\n\r\nBourbon Whiskey\r\nAn American-style whiskey, bourbon is made from corn. In fact, to be called bourbon whiskey, the spirit needs to be made from at least 51% corn, aged in a new oak barrel and produced in America. It has no minimum aging period and needs to be bottled at 80 proof or more.\r\n\r\nBourbon is the star ingredient in mint juleps—and you don’t have to wait for the Kentucky Derby to learn how to make one.\r\n\r\nTennessee Whiskey\r\nWhile Tennessee whiskey is technically classified as bourbon, some distillers in the state aren’t too keen on that. Instead, they use Tennessee whiskey to define their style. All current Tennessee whiskey producers are required by state law to produce their whiskeys in Tennessee and to use a filtering step known as the Lincoln County Process prior to aging the whiskey.\r\n\r\nRye Whiskey\r\nRye whiskey is made in America with at least 51% rye, while other ingredients include corn and barley. It follows the distilling process of bourbon. Rye that has been aged for two or more years and has not been blended is dubbed “straight rye whiskey.” Rye tends to have a spicier flavor than sweeter, smoother bourbon.\r\n\r\nBlended Whiskey\r\nBlended whiskey is exactly what the name highlights—it’s a mixture of different types of whiskey, as well as colorings, flavors and even other grains. These types of whiskeys are ideal for cocktails, as the process allows for the flavor to come through but keeps the spirit at a lower price point.\r\n\r\nSingle Malt Whisky\r\nSingle malt whisky needs to be made from one batch of scotch at a single distillery. Additionally, it must be aged for three years in oak before being bottled. The term “single malt” comes from the ingredients, as the main ingredient is malted barley. However, these rules did not make their way to U.S. distilleries. For example, in America, single malt is sometimes made from rye and not barley.",
                ImageUrl = "https://www.tasteofhome.com/wp-content/uploads/2019/08/bottles-of-scotch-whiskey-on-shelf-shutterstock_283026071.jpg?fit=1024,640",
                DateCreated = new DateTime(2024, 3, 15),
                Type = ArticleType.General,
                PublisherUserId = "7dfb241e-f8a5-4ba4-a5aa-5becf035c442"
            });

            return articles.ToArray();
        }
    }
}
