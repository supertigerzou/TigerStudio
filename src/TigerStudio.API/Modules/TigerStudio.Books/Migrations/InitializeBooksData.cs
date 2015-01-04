using TigerStudio.Books.Data;
using TigerStudio.Books.Data.DomainModels;
using TigerStudio.Framework.Data.DomainModels;
using TigerStudio.Framework.Helpers;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TigerStudio.Books.Migrations
{
    public class InitializeBooksData
    {
        public static void Init(BookContext context)
        {
            var webHelper = new WebHelper();

            //  Use the DbSet<T>.AddOrUpdate() helper extension method to avoid creating duplicate seed data.
            var loginUserVictor = new IdentityUser
            {
                UserName = "victor.zou",
                PasswordHash = "ADWj64qPNVxOr988AtL7WKaHKkOYSP9LFWUQniZIRxnXFaNJHELTF4kp+FtTnrYe6Q=="
            };
            var loginUserUnique = new IdentityUser
            {
                UserName = "unique.lin",
                PasswordHash = "ADWj64qPNVxOr988AtL7WKaHKkOYSP9LFWUQniZIRxnXFaNJHELTF4kp+FtTnrYe6Q=="
            };

            context.Users.AddOrUpdate(
                p => p.UserName,
                loginUserVictor,
                loginUserUnique
                );
            context.SaveChanges();

            var authorRhondaByrne = new Author
            {
                FirstName = "Rhonda",
                LastName = "Byrne",
                ShortDescription = "Rhonda Byrne is an Australian television writer and producer, best known " +
                "for her New Thought books The Secret and its sequel The Power.",
                Description = "<p>Rhonda Byrne is an Australian television writer and producer, best known for her New Thought books The Secret " +
                "(based on a film she produced of the same name) and its sequel The Power. She has also written The Magic.</p>" +
                "<p>In 2007 Byrne was featured in Time Magazine's list of 100 people who shape the world. She gained mainstream popularity and " +
                "commercial success after appearing on The Oprah Winfrey Show.[3] Her philosophy is that believing will allow you to achieve your wishes and dreams.</p>"
            };
            var authorJidduKrishnamurti = new Author
            {
                FirstName = "Jiddu",
                LastName = "Krishnamurti",
                ShortDescription = "Jiddu Krishnamurti was an Indian speaker and writer on philosophical and spiritual " + 
                "subjects.",
                Description = "<p>Jiddu Krishnamurti was an Indian speaker and writer on philosophical and spiritual " + 
                "subjects. In his early life he was groomed to be the new World Teacher but later rejected this " +
                "mantle and disbanded the organisation behind it.</p><p>His subject matter included psychological " +
                "revolution, the nature of mind, meditation, inquiry, human relationships, and bringing about " +
                "radical change in society.</p>"
            };
            var authorYuanJieZheng = new Author
            {
                FirstName = "YuanJie",
                LastName = "Zheng",
                ShortDescription = "Zheng Yuanjie (born 1955) is a Chinese fairy tale author, and founder and sole " + 
                "writer of a children's literature magazine known as the King of Fairy Tales.",
                Description = "<p>Zheng Yuanjie (born 1955) is a Chinese fairy tale author, and founder and sole " + 
                "writer of a children's literature magazine known as the King of Fairy Tales. The first issue " + 
                "was published in 1984. His characters (including PiPilu, LuXixi, Shuke, Beita and Luoke) are " +
                "registered trademarks.</p><p>Zheng Yuanjie is critical of conventional methods of education, " +
                "claiming that \"college education tends to make simple things complicated and hard to understand\"." +
                " He chose to home-school his son rather than to send him to middle school."
            };
            var authorScottOdell = new Author
            {
                FirstName = "Scott",
                LastName = "O'Dell",
                ShortDescription = "Scott O'Dell (May 23, 1898 – October 16, 1989) was an American author of 26 novels " +
                "for young people, along with three novels for adults and four nonfiction books.",
                Description = "<p>Scott O'Dell (May 23, 1898 – October 16, 1989) was an American author of 26 novels " +
                "for young people, along with three novels for adults and four nonfiction books. He wrote " +
                "historical fiction, primarily, including several children's novels are about historical " +
                "California and Mexico.</p><p>For his contribution as a children's writer he received the " +
                "biennial, international Hans Christian Andersen Award in 1972, the highest recognition " +
                "available to creators of children's books. He received the The University of Southern " +
                "Mississippi Medallion in 1976 and the Catholic Libraries Association Regina Medal in 1978.</p>"
            };
            //var authorVictorZou = new Author
            //{
            //    FirstName = "Victor",
            //    LastName = "Zou",
            //    LoginUser = loginUserVictor
            //};

            context.Authors.AddOrUpdate(
                p => p.Id
                , authorRhondaByrne
                , authorJidduKrishnamurti
                , authorYuanJieZheng
                , authorScottOdell
                //, authorVictorZou
                );
            context.SaveChanges();

            // http://www.amazon.com/Secret-Rhonda-Byrne/dp/1582701709
            var bookSecret = new Book
            {
                Author = authorRhondaByrne,
                Name = "The Secret"
            };
            // http://www.amazon.com/Freedom-Known-Jiddu-Krishnamurti/dp/0060648082
            var bookFreedomFromTheKnown = new Book
            {
                Author = authorJidduKrishnamurti,
                Name = "Freedom from the Known"
            };
            // http://www.amazon.com/The-Book-Life-Meditations-Krishnamurti/dp/0060648791
            var bookTheBookOfLife = new Book
            {
                Author = authorJidduKrishnamurti,
                Name = "The Book of Life"
            };
            // http://www.amazon.com/Think-These-Things-Jiddu-Krishnamurti/dp/0060916095
            var bookThinkOnTheseThings = new Book
            {
                Author = authorJidduKrishnamurti,
                Name = "Think on These Things"
            };
            // http://www.amazon.com/On-Love-Loneliness-Jiddu-Krishnamurti/dp/0062510134
            var bookOnLoveAndLoneliness = new Book
            {
                Author = authorJidduKrishnamurti,
                Name = "On Love and Loneliness"
            };
            // http://www.amazon.com/Stories-Shuke-Beita-Chinese/dp/7539175745
            var bookTheStoriesOfShukeAndBeita = new Book
            {
                Author = authorYuanJieZheng,
                Name = "The Stories of Shuke and Beita"
            };
            // http://www.amazon.com/Stories-about-Pi-Pilu-Chinese/dp/7539175761
            var bookStoriesAboutPiPilu = new Book
            {
                Author = authorYuanJieZheng,
                Name = "Stories about Pi Pilu"
            };
            // http://www.amazon.com/Stories-about-Lu-Xixi-Chinese/dp/7539175753
            var bookStoriesAboutLuXixi = new Book
            {
                Author = authorYuanJieZheng,
                Name = "Stories about Lu Xixi"
            };
            // http://www.amazon.com/Island-Blue-Dolphins-Scott-ODell/dp/0547328613
            var bookIslandOfTheBlueDolphins = new Book
            {
                Author = authorScottOdell,
                Name = "Island of the Blue Dolphins"
            };
            // http://www.amazon.com/Sing-Down-Moon-Scott-ODell/dp/0547406320
            var bookSingDownTheMoon = new Book
            {
                Author = authorScottOdell,
                Name = "Sing Down the Moon"
            };
            // http://www.amazon.com/Black-Pearl-Scott-ODell/dp/0547334001
            var bookTheBlackPearl = new Book
            {
                Author = authorScottOdell,
                Name = "The Black Pearl"
            };

            context.Books.AddOrUpdate(
                b => b.Id,
                bookSecret,
                bookFreedomFromTheKnown,
                bookTheBookOfLife,
                bookThinkOnTheseThings,
                bookOnLoveAndLoneliness,
                bookTheStoriesOfShukeAndBeita,
                bookStoriesAboutPiPilu,
                bookStoriesAboutLuXixi,
                bookIslandOfTheBlueDolphins,
                bookSingDownTheMoon,
                bookTheBlackPearl
                );
            context.SaveChanges();

            #region init book pictures
            const string sampleImagePathBase = "~/Migrations/Images/Books/";
            var pictureSecret1 = new EntityPicture
            {
                LargePhotoFileName = "secret_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "secret_1_large.jpg")),
                ThumbnailPhotoFileName = "secret_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "secret_1_small.jpg"))
            };
            var pictureSecret2 = new EntityPicture
            {
                LargePhotoFileName = "secret_2_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "secret_2_large.jpg")),
                ThumbnailPhotoFileName = "secret_2_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "secret_2_small.jpg"))
            };
            var pictureSecret3 = new EntityPicture
            {
                LargePhotoFileName = "secret_3_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "secret_3_large.jpg")),
                ThumbnailPhotoFileName = "secret_3_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "secret_3_small.jpg"))
            };
            var pictureFreedomFromTheKnown1 = new EntityPicture
            {
                LargePhotoFileName = "FreedomFromTheKnown_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "FreedomFromTheKnown_1_large.jpg")),
                ThumbnailPhotoFileName = "FreedomFromTheKnown_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "FreedomFromTheKnown_1_small.jpg"))
            };
            var pictureIslandOfTheBlueDolphins1 = new EntityPicture
            {
                LargePhotoFileName = "IslandOfTheBlueDolphins_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "IslandOfTheBlueDolphins_1_large.jpg")),
                ThumbnailPhotoFileName = "IslandOfTheBlueDolphins_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "IslandOfTheBlueDolphins_1_small.jpg"))
            };
            var pictureOnLoveAndLoneliness1 = new EntityPicture
            {
                LargePhotoFileName = "OnLoveAndLoneliness_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "OnLoveAndLoneliness_1_large.jpg")),
                ThumbnailPhotoFileName = "OnLoveAndLoneliness_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "OnLoveAndLoneliness_1_small.jpg"))
            };
            var pictureSingDownTheMoon1 = new EntityPicture
            {
                LargePhotoFileName = "SingDownTheMoon_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "SingDownTheMoon_1_large.jpg")),
                ThumbnailPhotoFileName = "SingDownTheMoon_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "SingDownTheMoon_1_small.jpg"))
            };
            var pictureStoriesAboutLuXixi1 = new EntityPicture
            {
                LargePhotoFileName = "StoriesAboutLuXixi_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "StoriesAboutLuXixi_1_large.jpg")),
                ThumbnailPhotoFileName = "StoriesAboutLuXixi_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "StoriesAboutLuXixi_1_small.jpg"))
            };
            var pictureStoriesAboutPiPilu1 = new EntityPicture
            {
                LargePhotoFileName = "StoriesAboutPiPilu_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "StoriesAboutPiPilu_1_large.jpg")),
                ThumbnailPhotoFileName = "StoriesAboutPiPilu_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "StoriesAboutPiPilu_1_small.jpg"))
            };
            var pictureTheBlackPearl1 = new EntityPicture
            {
                LargePhotoFileName = "TheBlackPearl_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "TheBlackPearl_1_large.jpg")),
                ThumbnailPhotoFileName = "TheBlackPearl_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "TheBlackPearl_1_small.jpg"))
            };
            var pictureTheBookOfLife1 = new EntityPicture
            {
                LargePhotoFileName = "TheBookOfLife_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "TheBookOfLife_1_large.jpg")),
                ThumbnailPhotoFileName = "TheBookOfLife_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "TheBookOfLife_1_small.jpg"))
            };
            var pictureTheStoriesOfShukeAndBeita1 = new EntityPicture
            {
                LargePhotoFileName = "TheStoriesOfShukeAndBeita_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "TheStoriesOfShukeAndBeita_1_large.jpg")),
                ThumbnailPhotoFileName = "TheStoriesOfShukeAndBeita_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "TheStoriesOfShukeAndBeita_1_small.jpg"))
            };
            var pictureThinkOnTheseThings1 = new EntityPicture
            {
                LargePhotoFileName = "ThinkOnTheseThings_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "ThinkOnTheseThings_1_large.jpg")),
                ThumbnailPhotoFileName = "ThinkOnTheseThings_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "ThinkOnTheseThings_1_small.jpg"))
            };
            #endregion

            #region init author pictures
            var pictureRhondaByrne1 = new EntityPicture
            {
                LargePhotoFileName = "RhondaByrne_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "RhondaByrne_1_large.jpg")),
                ThumbnailPhotoFileName = "RhondaByrne_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "RhondaByrne_1_small.jpg"))
            };
            var pictureRhondaByrne2 = new EntityPicture
            {
                LargePhotoFileName = "RhondaByrne_2_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "RhondaByrne_2_large.jpg")),
                ThumbnailPhotoFileName = "RhondaByrne_2_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "RhondaByrne_2_small.jpg"))
            };
            var pictureJidduKrishnamurti1 = new EntityPicture
            {
                LargePhotoFileName = "JidduKrishnamurti_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "JidduKrishnamurti_1_large.jpg")),
                ThumbnailPhotoFileName = "JidduKrishnamurti_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "JidduKrishnamurti_1_small.jpg"))
            };
            var pictureScottODell1 = new EntityPicture
            {
                LargePhotoFileName = "Scott_O_Dell_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "Scott_O_Dell_1_large.jpg")),
                ThumbnailPhotoFileName = "Scott_O_Dell_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "Scott_O_Dell_1_small.jpg"))
            };
            var pictureZhengYuanJie1 = new EntityPicture
            {
                LargePhotoFileName = "ZhengYuanJie_1_large.jpg",
                LargePhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "ZhengYuanJie_1_large.jpg")),
                ThumbnailPhotoFileName = "ZhengYuanJie_1_small.jpg",
                ThumbNailPhoto = File.ReadAllBytes(webHelper.MapPath(sampleImagePathBase + "ZhengYuanJie_1_small.jpg"))
            };
            #endregion

            context.EntityPictures.AddOrUpdate(
                bp => bp.Id,
                pictureSecret1,
                pictureSecret2,
                pictureSecret3,
                pictureFreedomFromTheKnown1,
                pictureIslandOfTheBlueDolphins1,
                pictureOnLoveAndLoneliness1,
                pictureSingDownTheMoon1,
                pictureStoriesAboutLuXixi1,
                pictureStoriesAboutPiPilu1,
                pictureTheBlackPearl1,
                pictureTheBookOfLife1,
                pictureTheStoriesOfShukeAndBeita1,
                pictureThinkOnTheseThings1,
                pictureRhondaByrne1,
                pictureRhondaByrne2
                );
            context.SaveChanges();

            var bookSecretPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookSecret,
                    EntityPicture = pictureSecret1,
                    Primary = true
                },
                new BookEntityPicture
                {
                    Entity = bookSecret,
                    EntityPicture = pictureSecret2,
                    Primary = false
                },
                new BookEntityPicture
                {
                    Entity = bookSecret,
                    EntityPicture = pictureSecret3,
                    Primary = false
                }
            };
            var bookFreedomFromTheKnownPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookFreedomFromTheKnown,
                    EntityPicture = pictureFreedomFromTheKnown1,
                    Primary = true
                }
            };
            var bookTheBookOfLifePictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookTheBookOfLife,
                    EntityPicture = pictureTheBookOfLife1,
                    Primary = true
                }
            };
            var bookThinkOnTheseThingsPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookThinkOnTheseThings,
                    EntityPicture = pictureThinkOnTheseThings1,
                    Primary = true
                }
            };
            var bookOnLoveAndLonelinessPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookOnLoveAndLoneliness,
                    EntityPicture = pictureOnLoveAndLoneliness1,
                    Primary = true
                }
            };
            var bookTheStoriesOfShukeAndBeitaPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookTheStoriesOfShukeAndBeita,
                    EntityPicture = pictureTheStoriesOfShukeAndBeita1,
                    Primary = true
                }
            };
            var bookStoriesAboutPiPiluPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookStoriesAboutPiPilu,
                    EntityPicture = pictureStoriesAboutPiPilu1,
                    Primary = true
                }
            };
            var bookStoriesAboutLuXixiPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookStoriesAboutLuXixi,
                    EntityPicture = pictureStoriesAboutLuXixi1,
                    Primary = true
                }
            };
            var bookIslandOfTheBlueDolphinsPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookIslandOfTheBlueDolphins,
                    EntityPicture = pictureIslandOfTheBlueDolphins1,
                    Primary = true
                }
            };
            var bookSingDownTheMoonPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookSingDownTheMoon,
                    EntityPicture = pictureSingDownTheMoon1,
                    Primary = true
                }
            };
            var bookTheBlackPearlPictures = new[]
            {
                new BookEntityPicture
                {
                    Entity = bookTheBlackPearl,
                    EntityPicture = pictureTheBlackPearl1,
                    Primary = true
                }
            };

            context.BookEntityPictures.AddOrUpdate(
                bbp => new { bbp.EntityId, bbp.EntityPictureId },
                bookSecretPictures
                .Concat(bookFreedomFromTheKnownPictures)
                .Concat(bookTheBookOfLifePictures)
                .Concat(bookThinkOnTheseThingsPictures)
                .Concat(bookOnLoveAndLonelinessPictures)
                .Concat(bookTheStoriesOfShukeAndBeitaPictures)
                .Concat(bookStoriesAboutPiPiluPictures)
                .Concat(bookStoriesAboutLuXixiPictures)
                .Concat(bookIslandOfTheBlueDolphinsPictures)
                .Concat(bookSingDownTheMoonPictures)
                .Concat(bookTheBlackPearlPictures)
                .ToArray());
            context.SaveChanges();

            var authorPictureMapping1 = new AuthorEntityPicture
            {
                Entity = authorRhondaByrne,
                EntityPicture = pictureRhondaByrne1,
                Primary = true
            };
            var authorPictureMapping2 = new AuthorEntityPicture
            {
                Entity = authorRhondaByrne,
                EntityPicture = pictureRhondaByrne2,
                Primary = false
            };
            var authorPictureMapping3 = new AuthorEntityPicture
            {
                Entity = authorJidduKrishnamurti,
                EntityPicture = pictureJidduKrishnamurti1,
                Primary = true
            };
            var authorPictureMapping4 = new AuthorEntityPicture
            {
                Entity = authorYuanJieZheng,
                EntityPicture = pictureZhengYuanJie1,
                Primary = true
            };
            var authorPictureMapping5 = new AuthorEntityPicture
            {
                Entity = authorScottOdell,
                EntityPicture = pictureScottODell1,
                Primary = true
            };

            context.AuthorEntityPictures.AddOrUpdate(
                bbp => new { bbp.EntityId, bbp.EntityPictureId },
                authorPictureMapping1, authorPictureMapping2, authorPictureMapping3,
                authorPictureMapping4, authorPictureMapping5
                );
            context.SaveChanges();

            context.Clients.AddRange(BuildClientsList());
        }

        private static IEnumerable<Client> BuildClientsList()
        {

            var clientList = new List<Client> 
            {
                new Client
                { Id = "ngTigerStudio", 
                    Secret= SecurityHelper.GetHash("abc@123"), 
                    Name="AngularJS front-end Application", 
                    ApplicationType = ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "http://localhost:55854"
                },
                new Client
                { Id = "consoleApp", 
                    Secret=SecurityHelper.GetHash("123@abc"), 
                    Name="Console Application", 
                    ApplicationType = ApplicationTypes.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };

            return clientList;
        }
    }
}
