using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.UserCenter;
using api.Model;

namespace api.Mappers
{
    public static class UserCenterMapper
    {
        public static CheckChapterDto ToCheckChapterDto(this List<CheckChapter> checkList ){
            if (checkList == null || checkList.Count == 0)
            {
                 throw new ArgumentException("The list is empty or null.");
            }
            var uniqueModuleIds = checkList
            .Select(c => c.Chapitre.ModuleId)
            .Distinct()
            .ToList();
            return new CheckChapterDto
            {
                ModuleIds = uniqueModuleIds
            };
        }
    }
}