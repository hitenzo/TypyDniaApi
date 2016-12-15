using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TypyDniaApi.Model.Helpers
{
    public static class Selectors
    {
        public static string GetSelector(string propertName)
        {
            foreach (FieldInfo info in typeof(Selectors).GetFields(BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (info.Name.ToLower().Equals(propertName.ToLower()))
                {
                    return info.GetValue(typeof(Selectors)).ToString();
                }
            }
            return string.Empty;
        }

        private static string PassTypesButton = "#live-chart-stats-options > li:nth-child(2) > a";

        private static string FixturesButton = "#team-fixtures-summary > div:nth-child(1) > h2 > a";

        private static string AttemptTypesButton = "#live-chart-stats-options > li:nth-child(1) > a";

        private static string CardSituationsButton = "#live-chart-stats-options > li:nth-child(3) > a";

        private static string Fixtures = "#team-fixtures > tbody > tr";

        private static string RecentMachesDates = "#team-fixtures-summary > tbody > tr > td.date";
              
        private static string HtScore = "#match-header > table > tbody > tr:nth-child(2) > td:nth-child(2) > div:nth-child(2) > dl > dd:nth-child(2)";
                 
        private static string FtScore = "#match-header > table > tbody > tr:nth-child(2) > td:nth-child(2) > div:nth-child(2) > dl > dd:nth-child(4)";
               
        private static string HomeTeam = "#match-header > table > tbody > tr:nth-child(1) > td:nth-child(1) > a";
               
        private static string AwayTeam = "#match-header > table > tbody > tr:nth-child(1) > td:nth-child(3) > a";
              
        private static string Date = "#match-header > table > tbody > tr:nth-child(2) > td:nth-child(2) > div:nth-child(3) > dl > dd:nth-child(4)";

        private static string ShotsHome = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(1) > span:nth-child(1) > span";
                
        private static string ShotsAway = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(1) > span:nth-child(3) > span";
              
        private static string ShotsOnTargetHome = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(2) > span:nth-child(1) > span";
              
        private static string ShotsOnTargetAway = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(2) > span:nth-child(3) > span";
               
        private static string PassSuccessHome = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(3) > span:nth-child(1) > span";
              
        private static string PassSuccessAway = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(3) > span:nth-child(3) > span";
               
        private static string AerialDuelSuccessHome = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(4) > span:nth-child(1) > span";
                
        private static string AerialDuelSuccessAway = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(4) > span:nth-child(3) > span";
                
        private static string DribblesWonHome = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(5) > span:nth-child(1) > span";
              
        private static string DribblesWonAway = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(5) > span:nth-child(3) > span";
              
        private static string TacklesHome = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(6) > span:nth-child(1) > span";
               
        private static string TacklesAway = "#match-report-team-statistics > div.stat-group.no-top-margin > div:nth-child(6) > span:nth-child(3) > span";
               
        private static string PossessionHome = "#match-report-team-statistics > div:nth-child(2) > div.stat > span > span:nth-child(2) > span";
               
        private static string PossessionAway = "#match-report-team-statistics > div:nth-child(2) > div.stat > span > span:nth-child(3) > span";
               
        private static string MatchSummaryList = "#content-2col-left > table > tbody";
               
        private static string TotalAttemptsHome = "#live-goals-content-comparision > div > div.stat.selected > span:nth-child(1) > span";
              
        private static string TotalAttemptsAway = "#live-goals-content-comparision > div > div.stat.selected > span:nth-child(3) > span";
                 
        private static string OpenPlayHome = "#live-goals-content-comparision > div > div:nth-child(3) > span:nth-child(1) > span";
               
        private static string OpenPlayAway = "#live-goals-content-comparision > div > div:nth-child(3) > span:nth-child(3) > span";
               
        private static string SetPieceHome = "#live-goals-content-comparision > div > div:nth-child(4) > span:nth-child(1) > span";
               
        private static string SetPieceAway = "#live-goals-content-comparision > div > div:nth-child(4) > span:nth-child(3) > span";
               
        private static string CounterAttackHome = "#live-goals-content-comparision > div > div:nth-child(5) > span:nth-child(1) > span";
               
        private static string CounterAttackAway = "#live-goals-content-comparision > div > div:nth-child(5) > span:nth-child(3) > span";
               
        private static string PenaltyHome = "#live-goals-content-comparision > div > div:nth-child(6) > span:nth-child(1) > span";
               
        private static string PenaltyAway = "#live-goals-content-comparision > div > div:nth-child(6) > span:nth-child(3) > span";
              
        private static string OwnGoalHome = "#live-goals-content-comparision > div > div:nth-child(7) > span:nth-child(1) > span";
               
        private static string OwnGoalAway = "#live-goals-content-comparision > div > div:nth-child(7) > span:nth-child(3) > span";
                
        private static string ConversionRateHome = "#live-goals-info > div > div:nth-child(4) > span > span:nth-child(2) > span";
            
        private static string ConversionRateAway = "#live-goals-info > div > div:nth-child(4) > span > span:nth-child(4) > span";
                
        private static string PassesTotalHome = "#live-passes-content-comparision > div > div.stat.selected > span:nth-child(1) > span";
                                                
        private static string PassesTotalAway = "#live-passes-content-comparision > div > div.stat.selected > span:nth-child(3) > span";
              
        private static string CrossesHome = "#live-passes-content-comparision > div > div:nth-child(3) > span:nth-child(1) > span";
                
        private static string CrossesAway = "#live-passes-content-comparision > div > div:nth-child(3) > span:nth-child(3) > span";
               
        private static string ThroughBallsHome = "#live-passes-content-comparision > div > div:nth-child(4) > span:nth-child(1) > span";
              
        private static string ThroughBallsAway = "#live-passes-content-comparision > div > div:nth-child(4) > span:nth-child(3) > span";
             
        private static string LongBallsHome = "#live-passes-content-comparision > div > div:nth-child(5) > span:nth-child(1) > span";
            
        private static string LongBallsAway = "#live-passes-content-comparision > div > div:nth-child(5) > span:nth-child(3) > span";
                
        private static string ShortPassesHome = "#live-passes-content-comparision > div > div:nth-child(6) > span:nth-child(1) > span";
              
        private static string ShortPassesAway = "#live-passes-content-comparision > div > div:nth-child(6) > span:nth-child(3) > span";
                
        private static string AveragePassStreakHome = "#live-passes-info > div > div:nth-child(3) > span > span:nth-child(2) > span";
                
        private static string AveragePassStreakAway = "#live-passes-info > div > div:nth-child(3) > span > span:nth-child(4) > span";
                 
        private static string CardsTotalHome = "#live-aggression-content-comparision > div > div.stat.selected > span:nth-child(1) > span";
                 
        private static string CardsTotalAway = "#live-aggression-content-comparision > div > div.stat.selected > span:nth-child(3) > span";
               
        private static string CardsFromFaulHome = "#live-aggression-content-comparision > div > div:nth-child(3) > span:nth-child(1) > span";
               
        private static string CardsFromFaulAway = "#live-aggression-content-comparision > div > div:nth-child(3) > span:nth-child(3) > span";
              
        private static string CardsFromUnprofessionalHome = "#live-aggression-content-comparision > div > div:nth-child(4) > span:nth-child(1) > span";
                
        private static string CardsFromUnprofessionalAway = "#live-aggression-content-comparision > div > div:nth-child(4) > span:nth-child(3) > span";
               
        private static string CardsFromDiveHome = "#live-aggression-content-comparision > div > div:nth-child(5) > span:nth-child(1) > span";
               
        private static string CardsFromDiveAway = "#live-aggression-content-comparision > div > div:nth-child(5) > span:nth-child(3) > span";
                
        private static string CardsFromOtherHome = "#live-aggression-content-comparision > div > div:nth-child(6) > span:nth-child(1) > span";
                
        private static string CardsFromOtherAway = "#live-aggression-content-comparision > div > div:nth-child(6) > span:nth-child(3) > span";
                
        private static string RedCardsPerGameHome = "#live-aggression-info > div > div:nth-child(2) > span > span:nth-child(2) > span";
                 
        private static string RedCardsPerGameAway = "#live-aggression-info > div > div:nth-child(2) > span > span:nth-child(4) > span";
                 
        private static string YellowCardsPerGameHome = "#live-aggression-info > div > div:nth-child(3) > span > span:nth-child(2) > span";
                 
        private static string YellowCardsPerGameAway = "#live-aggression-info > div > div:nth-child(3) > span > span:nth-child(4) > span";
                
        private static string CardsPerFaulHome = "#live-aggression-info > div > div:nth-child(4) > span > span:nth-child(2) > span";
                 
        private static string CardsPerFaulAway = "#live-aggression-info > div > div:nth-child(4) > span > span:nth-child(4) > span";
                
        private static string FaulsPerGameHome = "#live-aggression-info > div > div:nth-child(5) > span > span:nth-child(2) > span";
               
        private static string FaulsPerGameAway = "#live-aggression-info > div > div:nth-child(5) > span > span:nth-child(4) > span";

        private static string LeagueList = "#popular-tournaments-list > li > a";

        private static string YearsList = "#seasons";

        private static string MatchReport = "td > a.match-link";

        private static string DatePickerButton = "#date-config-toggle-button";

        private static string FirstYear = "#date-config > div.datepicker-wrapper > div > table > tbody > tr > td:nth-child(1) > div > table > tbody > tr:nth-child(1) > td";

        private static string SecondYear = "#date-config > div.datepicker-wrapper > div > table > tbody > tr > td:nth-child(1) > div > table > tbody > tr:nth-child(2) > td";

        private static string MonthPicker = "#date-config > div.datepicker-wrapper > div > table > tbody > tr > td:nth-child(2) > div > table > tbody";

        private static string ActiveMonths = "#date-config > div.datepicker-wrapper > div > table > tbody > tr > td:nth-child(2) > div > table > tbody > tr > td.selectable";

        private static string ActiveWeeks = "#date-config > div.datepicker-wrapper > div > table > tbody > tr > td:nth-child(3) > div > table > tbody > tr.selectable";

        private static string NextWeekButton = "#date-controller > a.next.button.ui-state-default.rc-r.is-default";

        private static string PreviousWeekButton = "#date-controller > a.previous.button.ui-state-default.rc-l.is-default";

        private static string WeekMatches = "#tournament-fixture > tbody";

        private static string MatchesInDetails = "#tournament-fixture > tbody > tr";

        private static string Browser = "#tournament-nav-popup > div.option-group";

        private static string DetailedTournamentsButton = "#tournament-groups > li:nth-child(3) > a";

        private static string DatePicker = "#date-config > div.datepicker-wrapper";
    }
}