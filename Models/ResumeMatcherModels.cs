// Models/ResumeMatcherModels.cs

namespace Duffl_career.Models
{
    public class MatchResultViewModel
    {
        public List<CandidateResult> Results { get; set; }
        public int ThresholdUsed { get; set; }
        public SummaryModel Summary { get; set; }
    }
    public class CandidateResult
    {
        public string File { get; set; }
        public string CandidateName { get; set; }
        public string Experience { get; set; }
        public double Score { get; set; }
        public string Status { get; set; }  // "Matched" or "Not Matched"
        public string Issues { get; set; }
    }
    public class SummaryModel
    {
        public int Total { get; set; }
        public int Matched { get; set; }
        public int NotMatched { get; set; }
    }
}