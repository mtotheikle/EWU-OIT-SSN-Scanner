#include <stdio.h>
#include <string>
#include <stdexcept>
#include <iostream>
#include <fstream>
#include <regex>

#define DLL_EXPORT __declspec(dllexport)

using namespace std;

namespace SSNValidation
{
	int AreaCheckPre(int area);
	int AreaCheckPost(int area);
	bool isAd(string s);
	int KeyphraseCheck(const char s[]);
}
namespace CCNValidation
{
	int LuhnCheck(string input);
	int KeyphraseCheck(const char s[]);
}

void strip(string *str);
enum {
    NoNumFound = 0,
    Low,
    Medium,
    High
};

enum Pattern {
	// regex pattern types
};

extern "C"
{
	/*
	 * Scan for social security numbers
	 *
	 * s: the character array to be scanned, usually the contents of a file
	 * length: the length of the array
	 * count: the address of the integer variable to be set to the number of ssns found in s
	 */
	DLL_EXPORT int ScanSSN(const char s[], int length, int *count)
	{
		int priority = NoNumFound;
		int type; // for regex
		*count = 0;
		int areaNum; // first three digits of a SSN
		int tempPriority; // to maintain max priority throught the file
		string NineDigit; // this holds each token in the file that matches the regex
		regex rgx("(\\b\\d{3}-\\d{2}-\\d{4}\\b)|(\\b\\d{9}\\b)"); 

		// tokenizes the input string and checks each token for the regex pattern, we can then iterate though the matches
		cregex_token_iterator next(s, s+length-1, rgx); 
		cregex_token_iterator end;

		for(;next != end; next++)
		{
			tempPriority = priority;
			priority = 0;
			NineDigit = next->str();
			strip(&NineDigit); // takes out every nondigit from the string

			areaNum = atoi(NineDigit.substr(0, 3).c_str());
			priority += SSNValidation::AreaCheckPost(areaNum);
			priority += SSNValidation::AreaCheckPre(areaNum);
			if(SSNValidation::isAd(NineDigit) || NineDigit.substr(3, 2).compare("00") == 0 || NineDigit.substr(5, 4).compare("0000") == 0) 
				priority = NoNumFound;

			if(priority) //priority != 0
				++(*count);
			if(priority < tempPriority)
				priority = tempPriority;
		}

		if(priority) //priority != 0
			priority += SSNValidation::KeyphraseCheck(s); // check for keyphrases
		return priority;
	}

	/*
	 * Scan for Credit Card Numbers
	 *
	 * s: the character array to be scanned, usually the contents of a file
	 * length: the length of the array
	 * count: the address of the integer variable to be set to the number found in s
	 */
	DLL_EXPORT int ScanCCN(char s[], int length, int *count)
	{
		int priority = NoNumFound;
		*count = 0;
		int tempPriority; // to maintain max priority throught the file
		string current;
		regex rgx("(\\b(4[0-9]{12}([0-9]{3})?|5[1-5][0-9]{14}|6(011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(0[0-5]|[68][0-9])[0-9]{11}|(2131|1800|35[0-9]{3})[0-9]{11})\\b)"); 
		// tokenizes the input string and checks each token for the regex pattern, we can then iterate though the matches
		cregex_token_iterator next(s, s+length-1, rgx);
		cregex_token_iterator end;

		for(;next != end; next++)
		{
			tempPriority = priority;
			current = next->str();

			priority += CCNValidation::LuhnCheck(current);

			if(priority) //priority != 0
				++(*count);
			if(priority < tempPriority)
				priority = tempPriority;
		}

		if(priority) //priority != 0
			priority += CCNValidation::KeyphraseCheck(s); // check for keyphrases

		return priority;
	}
}

/*
* check if area is in valid ranges for pre 6/25/2011 
*/
int SSNValidation::AreaCheckPre(int area) 
{
	if(area == 0) {
		return NoNumFound;
	}
	if (area == 666)
		return NoNumFound;
	if(area < 734) {
		return Low;
	}
	if(area < 750) {
		return NoNumFound;
	}
	if(area < 773) {
		return Low;
	}
	return NoNumFound;
}
/*
* check if area is in valid ranges for post 6/25/2011 
*/
int SSNValidation::AreaCheckPost(int area)
{
	if(area == 0)
		return NoNumFound;
	if(area == 666)
		return NoNumFound;
	if(area < 900)
		return Low;
	return NoNumFound;
}
/*
* check if this ssn is known to have been used in Ads
*/
bool SSNValidation::isAd(string s)
{
	if(s.compare("078051120") == 0)
		return true;
	if(s.compare("457555462") == 0)
		return true;
	if(s.substr(0,8).compare("98765432") == 0)
		return true;
	return false;
}
/*
* search for keyphrases for context based priority  
*/
int SSNValidation::KeyphraseCheck(const char s[])
{
	// Search for keyphrases, if one found return Low, else NoNumFound
	regex rgx("(\\bsocial security number)|(\\bssn)|(\\bstudent id)", regex_constants::icase);
	if(regex_search(s, rgx))
		return Low;

	return NoNumFound;
}
//---------------------END SSNValidation Functions-------------------------------------------------

//---------------------Start CCNValidation Functions------------------------------------------------
int CCNValidation::LuhnCheck(string input)
{
	int sum = 0;
	int digit = 0;
	int addend = 0;
	bool timesTwo = false;

	for (int i = input.length() - 1; i >= 0; i--) 
	{
		digit = atoi(&input[i]);
		if (timesTwo) 
		{
			addend = digit * 2;
			if (addend > 9) 
			{
				addend -= 9;
			}
		}
		else 
		{
			addend = digit;
		}
		sum += addend;
		timesTwo = !timesTwo;
	}

	int modulus = sum % 10;
	if(modulus == 0)
		return Low;
	return NoNumFound;
}

int CCNValidation::KeyphraseCheck(const char s[])
{
	// Search for keyphrases, if one found return Low, else NoNumFound
	regex rgx("(\\bcredit card( number)?)|(\\bccn)", regex_constants::icase);
	if(regex_search(s, rgx))
		return Low;

	return NoNumFound;
}
//---------------------END CCNValidation Functions------------------------------------------------

// remove any nondigit character from str
void strip(string *str)
{
	for( int i = str->length() - 1; i >= 0; --i)
	{
		if(!isdigit(str->at(i)))
			str->erase(i,1);
	}
}

// For Debugging
int main(void)
{
	ifstream fin("testinput.txt");
	int length, count;
	fin.seekg(0, std::ios::end);    // go to the end
	length = fin.tellg();           // report location (this is the lenght)
	fin.seekg(0, std::ios::beg);
	char *str = new char[length];
	fin.read(str, length);       // read the whole file into the buffer
	fin.close();

	int testPri = ScanSSN(str, length, &count);
	cout << "returned priority: " << testPri << endl;
	cout << "returned count: " << count << endl;
	cin >> *str;
}
