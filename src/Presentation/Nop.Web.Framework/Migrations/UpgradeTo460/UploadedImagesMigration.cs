

using System.Linq;
using FluentMigrator;
using Nop.Core.Domain.Media;
using Nop.Data;
using Nop.Services.Media.RoxyFileman;

namespace Nop.Web.Framework.Migrations.UpgradeTo460
{
    public class UploadedImagesMigration : Migration
    {
        #region Fields

        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRoxyFilemanFileProvider _roxyFilemanFileProvider;

        #endregion

        public UploadedImagesMigration(IRepository<Picture> pictureRepository, IRoxyFilemanFileProvider roxyFilemanFileProvider)
        {
            _pictureRepository = pictureRepository;
            _roxyFilemanFileProvider = roxyFilemanFileProvider;
        }

        #region Utils



        #endregion

        public override void Up()
        {

            try
            {
                var pictures = _pictureRepository.Table
                    .Where(p => p.VirtualPath.StartsWith("~/images/uploaded/"))
                    .ToList();

                if (pictures.Count == 0)
                    return;

                foreach (var picture in pictures)
                {
                    await FlushImagesAsync(picture, roxyConfig.MAX_IMAGE_WIDTH, roxyConfig.MAX_IMAGE_HEIGHT);
                }

                _pictureRepository.DeleteAsync(pictures, false).Wait();
            }
            catch
            {
                // ignored
            }
        }

        public override void Down()
        {
            //add the downgrade logic if necessary 
        }
    }
}