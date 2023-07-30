Feature: tester feature

Search the tester

@tester1
Scenario: Tester going to search
	Given open the Web browser
	When enter the url
	Then search for the testers

@tester2
Scenario Outline: Tester going to search example
	Given open the Web browser
	When enter the url
	Then search the <searchKey>
	Examples: 
	| searchKey   |
	| hi          |
	| how are you |

@tester3
Scenario: Tester going to search by table
	Given open the Web browser
	When enter the url
	Then search given <searchKey> 
	| searchKey   |
	| hi          |
	| how are you |




