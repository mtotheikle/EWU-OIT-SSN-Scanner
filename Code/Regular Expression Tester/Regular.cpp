
/* Author: Sijie Xia
 * Description: Simple tester File for the Engine part.
 * Notes: Add supports to read the file, and test at once.
 * date: 4/24/2012
 */

#include "Regular.h"

const string expression = "([\\s^]\\d{3}-\\d{2}-\\d{4}[\\s$])|([\\s^]\\d{9}[\\s$])";

int main()
{
	cout << " *** Welcome to the tester program ***" << endl;
	
	cout << " ***** Menu ***** " << endl;
	cout << "Please choose test type: " << endl;
	cout << "1. Test a single word. " << endl;
	cout << "2. Test all the words in a file. " << endl;
	cout << "Your choice ---> ";
	int choice = -1;
	cin >> choice;
	if (choice == 1)
		test_single();
	else if (choice == 2)
		test_multiple();
	else
		cout << "Invalid Choice. " << endl;

	cout << "Testing finished. " << endl;

	request_pause();

	return 0;
}

void test_single()
{
	cout << "Please enter a word to test if it works: " << endl;
	string temp;
	cin >> temp;
	isValid(temp);
}

void test_multiple()
{
	cout << "Please enter the file name you wish to test: ";
	string fileName = "";
	cin >> fileName;
	ifstream fin (fileName);
	if (fin.is_open())
	{
		while(! fin.eof())
		{
			string temp;
			getline(fin, temp);
			cout << "Testing word: " << temp << "  \t";
			isValid(temp);
		}
		fin.close();
	}
	else
		cout << "Cannot open the file!! " << endl;
}

void isValid(string test)
{
	regex myR(expression);
	if (regex_match(test.begin(), test.end(), myR))
	{
		cout << "Testing Result: Passed. " << endl;
	}
	else
		cout << "Testing Result: Failed. " << endl;
}

void request_pause()
{
	cout << "Press anyKey to continue... ";
	char x;
	cin >> x;
}