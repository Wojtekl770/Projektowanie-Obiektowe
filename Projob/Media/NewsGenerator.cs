using System;
using System.Collections.Generic;
using Projob1.Classes;
using Projob.Media;

namespace Projob
{
    public class NewsGenerator
    {
        private readonly List<IReportable> reportableObjects;
        private readonly List<Medias> mediaList;

        public NewsGenerator(List<IReportable> reportableObjects, List<Medias> mediaList)
        {
            this.reportableObjects = reportableObjects;
            this.mediaList = mediaList;
        }

        public IEnumerable<string> GenerateNews()
        {
            var iterator = new NewsIterator(reportableObjects, mediaList);
            while (iterator.HasNext())
            {
                yield return iterator.GetCurrent();
                iterator.MoveToNext();
            }
        }
    }
}
