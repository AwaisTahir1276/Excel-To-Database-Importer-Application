using System.ComponentModel.DataAnnotations;

namespace Read_ExcelFile.Models
{
    public class MetroAreaCity
    {
        [Key]
        public int MetroAreaCitiesID { get; set; }
        public int MetroAreaID { get; set; }
        public int cityID { get; set; }
        public string? url_MLSDotCom { get; set; }
        public bool? active_MLSDotCom { get; set; }
        public string? url_HomeGains { get; set; }
        public bool? active_HomeGains { get; set; }
        public string? url_RealtyTrac { get; set; }
        public bool? active_RealtyTrac { get; set; }
        public string? url_Reply { get; set; }
        public bool? active_Reply { get; set; }
        public string? url_Zillow { get; set; }
        public bool? active_Zillow { get; set; }
        public string? url_Houses { get; set; }
        public bool? active_Houses { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public string url_Foreclosure { get; set; }
        public bool? active_Foreclosure { get; set; }
        public int? budget { get; set; }
        public int? budgetClicks { get; set; }
        public long? replyClicks { get; set; }
        public long? foreclosureClicks { get; set; }
        public long? totalClicks { get; set; }
        public decimal? payout { get; set; }
        public decimal? replyRevenue { get; set; }

        public string active_Url
        {
            get
            {
                var activUrl = string.Empty;

                if (active_HomeGains.HasValue && active_HomeGains.Value)
                {
                    activUrl = url_HomeGains;
                }
                else if (active_Houses.HasValue && active_Houses.Value)
                {
                    activUrl = url_Houses;
                }
                else if (active_Foreclosure.HasValue && active_Foreclosure.Value)
                {
                    activUrl = url_Foreclosure;
                }
                else if (active_MLSDotCom.HasValue && active_MLSDotCom.Value)
                {
                    activUrl = url_MLSDotCom;
                }
                else if (active_RealtyTrac.HasValue && active_RealtyTrac.Value)
                {
                    activUrl = url_RealtyTrac;
                }
                else if (active_Reply.HasValue && active_Reply.Value)
                {
                    activUrl = url_Reply;
                }
                else if (active_Zillow.HasValue && active_Zillow.Value)
                {
                    activUrl = url_Zillow;
                }

                return activUrl;
            }
        }

        public string ActivePartner
        {
            get
            {
                var activePartner = string.Empty;

                if (active_HomeGains.HasValue && active_HomeGains.Value)
                {
                    activePartner = "HomeGain";
                }
                else if (active_Houses.HasValue && active_Houses.Value)
                {
                    activePartner = "Houses";
                }
                else if (active_Foreclosure.HasValue && active_Foreclosure.Value)
                {
                    activePartner = "Foreclosure";
                }
                else if (active_MLSDotCom.HasValue && active_MLSDotCom.Value)
                {
                    activePartner = "MLS.com";
                }
                else if (active_RealtyTrac.HasValue && active_RealtyTrac.Value)
                {
                    activePartner = "RealtyTrac";
                }
                else if (active_Reply.HasValue && active_Reply.Value)
                {
                    activePartner = "Reply";
                }
                else if (active_Zillow.HasValue && active_Zillow.Value)
                {
                    activePartner = "Zillow";
                }

                return activePartner;
            }
        }

        public virtual City City { get; set; }
    }
}
