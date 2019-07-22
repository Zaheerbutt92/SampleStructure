using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class MapperUtility
    {
        /// <summary>
        /// Initializes the mapper. mapping all the server and data objects.
        /// </summary>

        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Attachment, AttachmentFileVM>().ReverseMap();
                //cfg.CreateMap<AttachmentFileVM, Consultation_Attachment>().ReverseMap()
                //        .ForMember(src => src.DisplayName, a => a.MapFrom(dest => dest.Attachment.Dislplay_Name))
                //        .ForMember(src => src.Name, a => a.MapFrom(dest => dest.Attachment.Name))
                //        .ForMember(src => src.Path, a => a.MapFrom(dest => dest.Attachment.Path));



                #region Lookup
               //cfg.CreateMap<Rejection_Reason, LookupVM>().ReverseMap();

                #endregion
            });

        }

    }
}
