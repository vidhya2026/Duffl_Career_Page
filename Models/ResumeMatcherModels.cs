// Models/ResumeMatcherModels.cs

namespace Duffl_career.Models
{
    public class MatchApiResponse
    {
        public List<ResumeResult> Results { get; set; } = new();
        public MatchSummary Summary { get; set; } = new();
    }

    public class ResumeResult
    {
        public string File { get; set; } = "";
        public double Score { get; set; }
        public string Status { get; set; } = "";
        public string Issues { get; set; } = "";
    }

    public class MatchSummary
    {
        public int Total { get; set; }
        public int Matched { get; set; }
        public int NotMatched { get; set; }
    }
}