using System;
using System.Collections.Generic;
using Projob1.Classes;
using Projob.Media;

namespace Projob
{
    public class NewsIterator: Iterator
    {
        private readonly List<IReportable> reportableObjects;
        private readonly List<Medias> mediaList;
        private int currentReportableObjectIndex;
        private int currentMediaIndex;
    
        public NewsIterator(List<IReportable> reportableObjects, List<Medias> mediaList)
        {
            this.reportableObjects = reportableObjects;
            this.mediaList = mediaList;
            currentReportableObjectIndex = 0;
            currentMediaIndex = 0;
        }
    
        public string GetCurrent()
        {
            var reportableObject = reportableObjects[currentReportableObjectIndex];
            var media = mediaList[currentMediaIndex];
    
            string news = null;
            if (reportableObject != null && media != null)
            {
                news = reportableObject.Accept(media);
            }
            return news;
        }
    
        public void MoveToNext()
        {
            currentMediaIndex++;
            if (currentMediaIndex >= mediaList.Count)
            {
                currentMediaIndex = 0;
                currentReportableObjectIndex++;
            }
        }
    
        public bool HasNext()
        {
            return currentReportableObjectIndex < reportableObjects.Count;
        }
    }
}

