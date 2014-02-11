Feature: Getting Feedback
  Scenario: Anyone can give feedback about anything

    Given any visitor views any page
    When clicks "Give Feedback" button 
	Then a getting feedback pop-up form should be shown
   
	Given the visitor views getting feedback pop-up form
    When fills the "FeedbackDescription"		
    Then feedback should be saved
		And thank you message should be shown