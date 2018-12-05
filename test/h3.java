2. Write an input sanitization function called validInput using Java or
C++ language to filter urls containing javascript to prevent from XSS
attacks. The input is a url and the output is the sanitized url as following.
You can use either blacklist-based approach or whitelist-based
approach. (25 pts)

//using whitelist-base for validating input
//assume that I have white-list as an arrayList in constructor called whiteList
//list<String> whiteList = new ArrayList<String>();
//whiteList.add("www.google.com");
//whiteList.add("www.microsoft.com");   ..... more
//....... assume I have whitelist of url...
public String validInput (String url) {
String inputUrl = url;
String validatedInput ="";
Boolean goodInputvalidatedInput= false;
for (int i=0; i<whiteList.size(); i++)
{
 if(inputUrl == whiteList.get(i))
  goodInput = true;
}

if (goodInput)
{
    validatedInput= inputUrl;
}
else
validatedInput= "BAD URL, so It won't be used!!";

return validatedInput;

}
